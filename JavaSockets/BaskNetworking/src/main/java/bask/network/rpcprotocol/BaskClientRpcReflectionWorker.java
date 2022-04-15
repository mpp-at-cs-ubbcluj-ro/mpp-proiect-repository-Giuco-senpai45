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
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.net.Socket;
import java.util.List;

import bask.network.rpcprotocol.Request;


public class BaskClientRpcReflectionWorker implements Runnable, IObserver {
    private IServices server;
    private Socket connection;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private volatile boolean connected;
    public BaskClientRpcReflectionWorker(IServices server, Socket connection) {
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
                Response response=handleRequest((Request) request);
                if (response!=null){
                    sendResponse(response);
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

    @Override
    public void listUpdated(List<Match> matches) throws BasketException {
        Response resp=new Response.Builder().type(ResponseType.NEW_MATCH_LIST).data(matches).build();
        System.out.println("List updated"+matches);
        try {
            sendResponse(resp);
        } catch (IOException e) {
            throw new BasketException("Sending error: "+e);
        }
    }

    @Override
    public void ticketSold(Ticket ticket) throws BasketException {
        Response resp=new Response.Builder().type(ResponseType.SOLD_TICKET).data(ticket).build();
        System.out.println("Ticket sold"+ticket);
        try {
            sendResponse(resp);
        } catch (IOException e) {
            throw new BasketException("Sending error: "+e);
        }
    }

    @Override
    public void organiserLoggedIn(Organiser organiser) throws BasketException {
        Response resp=new Response.Builder().type(ResponseType.ORG_LOGGED_IN).data(organiser).build();
        System.out.println("Organiser logged in "+organiser);
        try {
            sendResponse(resp);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void organiserLoggedOut(Organiser organiser) throws BasketException {
        Response resp=new Response.Builder().type(ResponseType.ORG_LOGGED_OUT).data(organiser).build();
        System.out.println("Organiser logged out "+organiser);
        try {
            sendResponse(resp);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private static Response okResponse=new Response.Builder().type(ResponseType.OK).build();
    //  private static Response errorResponse=new Response.Builder().type(ResponseType.ERROR).build();
    private Response handleRequest(Request request){
        Response response=null;
        String handlerName="handle"+(request).type();
        System.out.println("HandlerName "+handlerName);
        try {
            Method method=this.getClass().getDeclaredMethod(handlerName, Request.class);
            response=(Response)method.invoke(this,request);
            System.out.println("Method "+handlerName+ " invoked");
        } catch (NoSuchMethodException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }

        return response;
    }

    private Response handleLOGIN(Request request){
        System.out.println("Login request ..."+request.type());
        Organiser organiser=(Organiser) request.data();
        try {
            server.login(organiser, this);
            return okResponse;
        } catch (BasketException e) {
            connected=false;
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleLOGOUT(Request request){
        System.out.println("Logout request...");
        Organiser organiser=(Organiser) request.data();
        try {
            server.logout(organiser, this);
            connected=false;
            return okResponse;

        } catch (BasketException e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleUPDATE_MATCHES(Request request){
            System.out.println("Update matches list request ...");
            List<Match> matches=(List<Match>)request.data();  //TODO not sure
            try {
                server.sendUpdatedList(matches);
                return okResponse;
            } catch (BasketException e) {
                return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
            }
    }

    private Response handleSELL_TICKET(Request request){
        System.out.println("Selling ticket request ..."+request.type());
        Ticket ticket=(Ticket) request.data();
        try {
            server.ticketSold(ticket);
            return okResponse;
        } catch (BasketException e) {
            connected=false;
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }


    private Response handleGET_MATCHES(Request request){
        System.out.println("GetLoggedFriends Request ...");
        List<Match> ml=(List<Match>) request.data();
        try {
            List<Match> matches = server.getMatchesList();
            return new Response.Builder().type(ResponseType.GOT_MATCHES).data(matches).build();
        } catch (BasketException e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private void sendResponse(Response response) throws IOException{
        System.out.println("sending response "+response);
        output.writeObject(response);
        output.flush();
    }
}
