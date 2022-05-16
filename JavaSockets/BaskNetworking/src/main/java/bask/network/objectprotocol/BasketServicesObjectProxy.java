package bask.network.objectprotocol;

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
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;


public class BasketServicesObjectProxy implements IServices {
    private String host;
    private int port;

    private IObserver client;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private Socket connection;

    private List<Response> responses;
    private BlockingQueue<Response> qresponses;
    private volatile boolean finished;
    public BasketServicesObjectProxy(String host, int port) {
        this.host = host;
        this.port = port;
        responses=new ArrayList<Response>();
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

    private void handleUpdate(UpdateResponse update){
        if (update instanceof OrganiserLoggedInResponse){
            OrganiserLoggedInResponse frUpd = (OrganiserLoggedInResponse)update;
            Organiser organiser= frUpd.getOrganiser();
            System.out.println("Organiser logged in " + organiser);
            try {
                client.organiserLoggedIn(organiser);
            } catch (BasketException e) {
                e.printStackTrace();
            }
        }
        if (update instanceof OrganiserLoggedOutResponse){
            OrganiserLoggedOutResponse frOutRes=(OrganiserLoggedOutResponse)update;
            Organiser organiser= frOutRes.getOrganiser();
            System.out.println("Organiser logged out "+organiser);
            try {
                client.organiserLoggedOut(organiser);
            } catch (BasketException e) {
                e.printStackTrace();
            }
        }

        if (update instanceof GetOrganiserCredentialsResponse){
            GetOrganiserCredentialsResponse credentialsResp=(GetOrganiserCredentialsResponse)update;
            Organiser organiser= credentialsResp.getOrganiser();
//            try {
//                client.
//            } catch (BasketException e) {
//                e.printStackTrace();
//            }
        }

        if (update instanceof NewMatchesResponse){
            NewMatchesResponse msgRes=(NewMatchesResponse)update;
            List<Match> matches =  msgRes.getMatchList();
            try {
                client.listUpdated(matches);
            } catch (BasketException e) {
                e.printStackTrace();  
            }
        }
    }

    @Override
    public void login(Organiser organiser, IObserver client) throws BasketException {
        initializeConnection();
        sendRequest(new LoginRequest(organiser));
        Response response=readResponse();
        if (response instanceof OkResponse){
            this.client=client;
            return;
        }
        if (response instanceof ErrorResponse){
            ErrorResponse err=(ErrorResponse)response;
            closeConnection();
            throw new BasketException(err.getMessage());
        }
    }

    @Override
    public Organiser getOrganiserByCredentials(Organiser organiser) throws BasketException {
        sendRequest(new GetOrganiserCredentialsRequest(organiser));
        Response response=readResponse();
        if (response instanceof OkResponse){
            this.client=client;
            return null;
        }
        if (response instanceof ErrorResponse){
            ErrorResponse err=(ErrorResponse)response;
            closeConnection();
            throw new BasketException(err.getMessage());
        }
        return organiser;
    }

    @Override
    public void ticketSold(Ticket ticket) throws BasketException {

    }

    @Override
    public void logout(Organiser organiser, IObserver client) throws BasketException {
        sendRequest(new LogoutRequest(organiser));
        Response response=readResponse();
        closeConnection();
        if (response instanceof ErrorResponse){
            ErrorResponse err=(ErrorResponse)response;
            throw new BasketException(err.getMessage());
        }
    }

    @Override
    public void sendUpdatedList(List<Match> matches) throws BasketException {
        sendRequest(new SendMatchesRequest(matches));
        Response response=readResponse();
        if (response instanceof ErrorResponse){
            ErrorResponse err=(ErrorResponse)response;
            throw new BasketException(err.getMessage());
        }
    }

    @Override
    public List<Match> getMatchesList() throws BasketException {
        return null;
    }

    /*@Override
    public Organiser[] getLoggedOrganisers(Organiser organiser) throws BasketException {
        sendRequest(new GetLoggedOrganisersRequest(organiser));
        Response response=readResponse();
        if (response instanceof ErrorResponse){
            ErrorResponse err=(ErrorResponse)response;
            throw new BasketException(err.getMessage());
        }
        GetLoggedOrganisersResponse resp=(GetLoggedOrganisersResponse)response;
        Organiser[] organisers = resp.getOrganisers();
        return organisers;
    }*/

    private class ReaderThread implements Runnable{
        public void run() {
            while(!finished){
                try {
                    Object response=input.readObject();
                    System.out.println("response received "+response);
                    if (response instanceof UpdateResponse){
                         handleUpdate((UpdateResponse)response);
                    }else{
                        responses.add((Response)response);
                        try {
                            Thread.sleep(1000);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                        synchronized (responses){
                            responses.notify();
                        }
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