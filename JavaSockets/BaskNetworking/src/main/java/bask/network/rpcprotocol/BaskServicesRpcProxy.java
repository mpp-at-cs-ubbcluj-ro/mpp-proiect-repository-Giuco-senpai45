package bask.network.rpcprotocol;

import bask.model.Match;
import bask.model.Organiser;
import bask.model.Ticket;
import bask.services.BasketException;
import bask.services.IObserver;
import bask.services.IServices;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;


public class BaskServicesRpcProxy implements IServices {
    private String host;
    private int port;

    private IObserver client;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private Socket connection;

    private BlockingQueue<Response> qresponses;
    private volatile boolean finished;
    public BaskServicesRpcProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses=new LinkedBlockingQueue<Response>();
    }

    private void closeConnection() {
        finished=true;
        try {
            input.close();
            output.close();
            connection.close();
            client=null;
        } catch (IOException e) {
            e.printStackTrace();
        }

    }

    private void sendRequest(Request request)throws BasketException {
        try {
            output.writeObject(request);
            output.flush();
        } catch (IOException e) {
            throw new BasketException("Error sending object "+e);
        }

    }

    private Response readResponse() throws BasketException {
        Response response=null;
        try{
            response=qresponses.take();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return response;
    }
    private void initializeConnection() throws BasketException {
        try {
            connection=new Socket(host,port);
            output=new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input=new ObjectInputStream(connection.getInputStream());
            finished=false;
            startReader();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    private void startReader(){
        Thread tw=new Thread(new ReaderThread());
        tw.start();
    }


    private void handleUpdate(Response response){
        if (response.type()== ResponseType.ORG_LOGGED_IN){
            Organiser friend= (Organiser) response.data();
            System.out.println("Friend logged in "+friend);
            try {
                client.organiserLoggedIn(friend);
            } catch (BasketException e) {
                e.printStackTrace();
            }
        }
        if (response.type()== ResponseType.ORG_LOGGED_OUT){
            Organiser friend= (Organiser) response.data();
            System.out.println("Friend logged out "+friend);
            try {
                client.organiserLoggedOut(friend);
            } catch (BasketException e) {
                e.printStackTrace();
            }
        }

        if (response.type()== ResponseType.NEW_MATCH_LIST){
            List<Match> matches= (List<Match>)response.data();
            try {
                client.listUpdated(matches);
            } catch (BasketException e) {
                e.printStackTrace();
            }
        }
    }

    private boolean isUpdate(Response response){
        return response.type()== ResponseType.ORG_LOGGED_OUT || response.type()== ResponseType.ORG_LOGGED_IN ||
                response.type()== ResponseType.NEW_MATCH_LIST || response.type()== ResponseType.SOLD_TICKET;
    }

    @Override
    public void login(Organiser user, IObserver client) throws BasketException {
        initializeConnection();
        Request req=new Request.Builder().type(RequestType.LOGIN).data(user).build();
        sendRequest(req);
        Response response=readResponse();
        if (response.type()== ResponseType.OK){
            this.client=client;
            return;
        }
        if (response.type()== ResponseType.ERROR){
            String err=response.data().toString();
            closeConnection();
            throw new BasketException(err);
        }
    }

    @Override
    public Organiser getOrganiserByCredentials(Organiser user) throws BasketException {
        return null;
    }

    @Override
    public void ticketSold(Ticket ticket) throws BasketException {
        Request req=new Request.Builder().type(RequestType.SELL_TICKET).data(ticket).build();
        sendRequest(req);
        Response response=readResponse();
        if (response.type()== ResponseType.ERROR){
            String err=response.data().toString();
            throw new BasketException(err);
        }
    }

    @Override
    public void logout(Organiser user, IObserver client) throws BasketException {
        Request req=new Request.Builder().type(RequestType.LOGOUT).data(user).build();
        sendRequest(req);
        Response response=readResponse();
        closeConnection();
        if (response.type()== ResponseType.ERROR){
            String err=response.data().toString();
            throw new BasketException(err);
        }
    }

    @Override
    public void sendUpdatedList(List<Match> matches) throws BasketException {
        Request req=new Request.Builder().type(RequestType.UPDATE_MATCHES).data(matches).build();
        sendRequest(req);
        Response response=readResponse();
        if (response.type()== ResponseType.ERROR){
            String err=response.data().toString();
            throw new BasketException(err);
        }
    }

    @Override
    public List<Match> getMatchesList() throws BasketException {
        Request req=new Request.Builder().type(RequestType.GET_MATCHES).data(null).build();
        sendRequest(req);
        Response response=readResponse();
        if (response.type()== ResponseType.ERROR){
            String err=response.data().toString();
            throw new BasketException(err);
        }
        return (List<Match>)response.data();
    }

    private class ReaderThread implements Runnable{
        public void run() {
            while(!finished){
                try {
                    Object response=input.readObject();
                    System.out.println("response received "+response);
                    if (isUpdate((Response)response)){
                        handleUpdate((Response)response);
                    }else{
                        try {
                            qresponses.put((Response)response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException e) {
                    System.out.println("Reading error "+e);
                } catch (ClassNotFoundException e) {
                    System.out.println("Reading error "+e);
                }
            }
        }
    }
}
