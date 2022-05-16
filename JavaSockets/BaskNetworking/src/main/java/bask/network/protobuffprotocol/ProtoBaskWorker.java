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
import java.util.List;

import static bask.network.protobuffprotocol.BasketProtobufs.BasketResponse.Type.NewMatchList;

public class ProtoBaskWorker  implements Runnable, IObserver {
    private IServices server;
    private Socket connection;

    private InputStream input;
    private OutputStream output;
    private volatile boolean connected;

    public ProtoBaskWorker(IServices server, Socket connection) {
        this.server = server;
        this.connection = connection;
        try{
            output=connection.getOutputStream() ;//new ObjectOutputStream(connection.getOutputStream());
            input=connection.getInputStream(); //new ObjectInputStream(connection.getInputStream());
            connected=true;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }


    @Override
    public void listUpdated(List<Match> matches) throws BasketException {
        System.out.println("Match list updated");
        Match[] matchesArr = new Match[matches.size()];
        matches.toArray(matchesArr);
        try {
            sendResponse(ProtoUtils.createUpdateMatchesResponse(matchesArr));
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void organiserLoggedIn(Organiser organiser) throws BasketException {
        System.out.println("Organiser logged in "+organiser);
        try {
            sendResponse(ProtoUtils.createLoggedInResponse(organiser));
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void organiserLoggedOut(Organiser organiser) throws BasketException {
        System.out.println("Organiser logged out "+organiser);
        try {
            sendResponse(ProtoUtils.createLoggedOutResponse(organiser));
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void run() {
        while(connected){
            try {
                // Object request=input.readObject();
                System.out.println("Waiting requests ...");
                BasketProtobufs.BasketRequest request=BasketProtobufs.BasketRequest.parseDelimitedFrom(input);
                System.out.println("Request received: "+request);
                BasketProtobufs.BasketResponse response=handleRequest(request);
                if (response!=null){
                    sendResponse(response);
                }
            } catch (IOException e) {
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

    private BasketProtobufs.BasketResponse handleRequest(BasketProtobufs.BasketRequest request) {
        BasketProtobufs.BasketResponse response=null;
        switch (request.getType()){
            case Login:{
                System.out.println("Login request ...");
                Organiser user=ProtoUtils.getUser(request);
                try {
                    server.login(user, this);
                    return ProtoUtils.createOkResponse();
                } catch (BasketException e) {
                    connected=false;
                    return ProtoUtils.createErrorResponse(e.getMessage());
                }
            }
            case Logout:{
                System.out.println("Logout request");
                Organiser user=ProtoUtils.getUser(request);
                try {
                    server.logout(user, this);
                    connected=false;
                    return ProtoUtils.createOkResponse();

                } catch (BasketException e) {
                    return ProtoUtils.createErrorResponse(e.getMessage());
                }
            }
            case GetMatches:{
                System.out.println("Get matches Request ...");
                try {
                    List<Match> matches = server.getMatchesList();
                    Match[] matchesArr = new Match[matches.size()];
                    matches.toArray(matchesArr);
                    return ProtoUtils.createGetMatchesResponse(matchesArr);
                } catch (BasketException e) {
                    return ProtoUtils.createErrorResponse(e.getMessage());
                }
            }
            case UpdateMatches:{
                System.out.println("Match list updated Request ...");
                Ticket ticket=ProtoUtils.getTicket(request);
                try {
                    server.sendUpdatedList(ticket);
                    return ProtoUtils.createOkResponse();
                } catch (BasketException e) {
                    return ProtoUtils.createErrorResponse(e.getMessage());
                }
            }
        }
        return response;
    }

    private void sendResponse(BasketProtobufs.BasketResponse response) throws IOException {
        System.out.println("sending response "+response);
        response.writeDelimitedTo(output);
        //output.writeObject(response);
        output.flush();
    }
}
