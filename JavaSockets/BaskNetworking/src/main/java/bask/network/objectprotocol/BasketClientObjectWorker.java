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
import java.util.List;


public class BasketClientObjectWorker implements Runnable, IObserver {
    private IServices server;
    private Socket connection;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private volatile boolean connected;

    public BasketClientObjectWorker(IServices server, Socket connection) {
        this.server = server;
        this.connection = connection;
        try{
            output=new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input=new ObjectInputStream(connection.getInputStream());
            connected=true;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void run() {
        while(connected){
            try {
                Object request=input.readObject();
                Object response=handleRequest((Request)request);
                if (response!=null){
                    System.out.println("Trimit raspuns la client");
                   sendResponse((Response) response);
                }
            } catch (IOException e) {
                e.printStackTrace();
            } catch (ClassNotFoundException e) {
                e.printStackTrace();
            }
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        try {
            input.close();
            output.close();
            connection.close();
        } catch (IOException e) {
            System.out.println("Error "+e);
        }
    }

    private Response handleRequest(Request request){
        Response response=null;
        if (request instanceof LoginRequest){
            System.out.println("Login request ...");
            LoginRequest logReq = (LoginRequest)request;
            Organiser organiser = logReq.getOrganiser();
            try {
                server.login(organiser, this);
                return new OkResponse();
            } catch (BasketException e) {
                connected=false;
                return new ErrorResponse(e.getMessage());
            }
        }
        if (request instanceof LogoutRequest){
            System.out.println("Logout request");
            LogoutRequest logReq = (LogoutRequest)request;
            Organiser organiser = logReq.getOrganiser();
            try {
                server.logout(organiser, this);
                connected=false;
                return new OkResponse();

            } catch (BasketException e) {
               return new ErrorResponse(e.getMessage());
            }
        }
        if (request instanceof GetOrganiserCredentialsRequest){
            System.out.println("Logout request");
            GetOrganiserCredentialsRequest getCredentialsReq = (GetOrganiserCredentialsRequest)request;
            Organiser organiser = getCredentialsReq.getOrganiser();
            try {
                server.getOrganiserByCredentials(organiser);
                return new OkResponse();

            } catch (BasketException e) {
                return new ErrorResponse(e.getMessage());
            }
        }
        if (request instanceof SendMatchesRequest){
            System.out.println("Update matches list request ...");
            SendMatchesRequest senReq = (SendMatchesRequest)request;
            List<Match> matches = senReq.getMatchList();
            try {
                server.sendUpdatedList(matches);
                 return new OkResponse();
            } catch (BasketException e) {
                return new ErrorResponse(e.getMessage());
            }
        }

        /*if (request instanceof GetLoggedOrganisersRequest){
            System.out.println("GetLoggedFriends Request ...");
            GetLoggedOrganisersRequest getReq = (GetLoggedOrganisersRequest)request;
            Organiser organiser = getReq.getOrganiser();
            System.out.println("Got this organiser" + organiser.getID() + " " +  organiser.getName()+ " " + organiser.getPassword());
            try {
                Organiser[] loggedOrganisers = server.getLoggedOrganisers(organiser);
                return new GetLoggedOrganisersResponse(loggedOrganisers );
            } catch (BasketException e) {
                return new ErrorResponse(e.getMessage());
            }
        }*/
        return response;
    }

    private void sendResponse(Response response) throws IOException{
        System.out.println("sending response "+response);
        synchronized (output) {
            output.writeObject(response);
            output.flush();
        }
    }

    @Override
    public void listUpdated(List<Match> matches) throws BasketException {
        System.out.println("Matches updated");
        try {
            sendResponse(new NewMatchesResponse(matches));
        } catch (IOException e) {
            throw new BasketException("Sending error: "+e);
        }
    }

    @Override
    public void ticketSold(Ticket ticket) throws BasketException {

    }

    @Override
    public void organiserLoggedIn(Organiser organiser) throws BasketException {
        System.out.println("Organiser logged in "+organiser);
        try {
            sendResponse(new OrganiserLoggedInResponse(organiser));
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void organiserLoggedOut(Organiser organiser) throws BasketException {
        System.out.println("Organiser logged out "+organiser);
        try {
            sendResponse(new OrganiserLoggedOutResponse(organiser));
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
