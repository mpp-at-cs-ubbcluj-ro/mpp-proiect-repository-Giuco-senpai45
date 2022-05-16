package bask.network.protobuffprotocol;

import bask.model.Match;
import bask.model.Organiser;
import bask.model.Ticket;
import bask.services.BasketException;
import bask.services.IObserver;
import bask.services.IServices;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

import static bask.network.protobuffprotocol.BasketProtobufs.BasketResponse.Type.NewMatchList;

public class ProtoBaskProxy implements IServices {
    private String host;
    private int port;

    private IObserver client;

    private InputStream input;
    private OutputStream output;
    private Socket connection;

    private BlockingQueue<BasketProtobufs.BasketResponse> qresponses;
    private volatile boolean finished;

    public ProtoBaskProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses=new LinkedBlockingQueue<BasketProtobufs.BasketResponse>();
    }

    @Override
    public void login(Organiser user, IObserver client) throws BasketException {
        initializeConnection();
        System.out.println("Login request ...");
        user.setId(2);
        sendRequest(ProtoUtils.createLoginRequest(user));
        BasketProtobufs.BasketResponse response=readResponse();
        if (response.getType()==BasketProtobufs.BasketResponse.Type.Ok){
            this.client=client;
            return;
        }
        if (response.getType()==BasketProtobufs.BasketResponse.Type.Error){
            String errorText=ProtoUtils.getError(response);
            closeConnection();
            throw new BasketException(errorText);
        }
    }

    @Override
    public void logout(Organiser user, IObserver client) throws BasketException {
        sendRequest(ProtoUtils.createLogoutRequest(user));
        System.out.println("Logging out request ...");
        BasketProtobufs.BasketResponse response=readResponse();
        closeConnection();
        if (response.getType()==BasketProtobufs.BasketResponse.Type.Error){
            String errorText=ProtoUtils.getError(response);
            throw new BasketException(errorText);
        }
    }

    @Override
    public Organiser getOrganiserByCredentials(Organiser user) throws BasketException {
        return null;
    }

    @Override
    public List<Match> getMatchesList() throws BasketException {
        System.out.println("Getting matches request ...");
        sendRequest(ProtoUtils.createGetMatchesRequest());
        BasketProtobufs.BasketResponse response=readResponse();
        if (response.getType()==BasketProtobufs.BasketResponse.Type.Error){
            String errorText=ProtoUtils.getError(response);
            throw new BasketException(errorText);
        }
        Match[] matchesArr=ProtoUtils.getMatches(response);
        List<Match> matches = new ArrayList<>();
        Collections.addAll(matches, matchesArr);
        return matches;
    }

    @Override
    public void sendUpdatedList(Ticket ticket) throws BasketException {
        System.out.println("Update list request ...");
        sendRequest(ProtoUtils.createUpdateMatchesRequest(ticket));
        BasketProtobufs.BasketResponse response=readResponse();
        if (response.getType()==BasketProtobufs.BasketResponse.Type.Error){
            String errorText=ProtoUtils.getError(response);
            throw new BasketException(errorText);
        }
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

    private void sendRequest(BasketProtobufs.BasketRequest request)throws BasketException{
        try {
            System.out.println("Sending request ..."+request);
            //request.writeTo(output);
            request.writeDelimitedTo(output);
            output.flush();
            System.out.println("Request sent.");
        } catch (IOException e) {
            throw new BasketException("Error sending object "+e);
        }

    }

    private BasketProtobufs.BasketResponse readResponse() throws BasketException{
        BasketProtobufs.BasketResponse response=null;
        try{
            response=qresponses.take();

        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return response;
    }

    private void initializeConnection() throws BasketException{
        try {
            connection=new Socket(host,port);
            output=connection.getOutputStream();
            //output.flush();
            input=connection.getInputStream();     //new ObjectInputStream(connection.getInputStream());
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

    private void handleUpdate(BasketProtobufs.BasketResponse updateResponse) {
        switch (updateResponse.getType()) {
            case OrgLoggedIn: {
                Organiser friend = ProtoUtils.getUser(updateResponse);
                System.out.println("Friend logged in " + friend);
                try {
                    client.organiserLoggedIn(friend);
                } catch (BasketException e) {
                    e.printStackTrace();
                }
                break;
            }
            case OrgLoggedOut: {
                Organiser friend = ProtoUtils.getUser(updateResponse);
                System.out.println("Friend logged out " + friend);
                try {
                    client.organiserLoggedOut(friend);
                } catch (BasketException e) {
                    e.printStackTrace();
                }

                break;
            }
            case NewMatchList: {
                Match[] matches = ProtoUtils.getMatches(updateResponse);
                List<Match> matchesList = new ArrayList<>();
                matchesList.addAll(Arrays.asList(matches));
                try {
                    client.listUpdated(matchesList);
                } catch (BasketException e) {
                    e.printStackTrace();
                }
                break;
            }
        }
    }

    private class ReaderThread implements Runnable{
        public void run() {
            while(!finished){
                try {
                    BasketProtobufs.BasketResponse response=BasketProtobufs.BasketResponse.parseDelimitedFrom(input);
                    System.out.println("response received "+response);

                    if (isUpdateResponse(response.getType())){
                        handleUpdate(response);
                    }else{
                        try {
                            qresponses.put(response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException e) {
                    System.out.println("Reading error "+e);
                }
            }
        }
    }

    private boolean isUpdateResponse(BasketProtobufs.BasketResponse.Type type){
        switch (type){
            case OrgLoggedIn:  return true;
            case OrgLoggedOut: return true;
            case NewMatchList:return true; //TODO: vezi in worker sa faci la updatematches sa trimit newmatchlist response
        }
        return false;
    }
}
