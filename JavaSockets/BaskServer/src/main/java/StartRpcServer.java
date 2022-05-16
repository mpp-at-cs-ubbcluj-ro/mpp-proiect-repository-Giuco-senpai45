import java.io.IOException;
import java.util.Properties;

import bask.network.utils.AbstractServer;
import bask.network.utils.BasketRpcConcurrentServer;
import bask.network.utils.ServerException;
import bask.repos.RepoDBMatch;
import bask.repos.RepoDBOrganiser;
import bask.repos.RepoDBTicket;
import bask.services.IServices;
import chat.server.*;

public class StartRpcServer {
    private static int defaultPort=55555;
    public static void main(String[] args) {
        // UserRepository userRepo=new UserRepositoryMock();
        Properties serverProps=new Properties();
        try {
            serverProps.load(StartRpcServer.class.getResourceAsStream("/chatserver.properties"));
            System.out.println("Server properties set. ");
            serverProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find chatserver.properties "+e);
            return;
        }
        RepoDBOrganiser organiserRepo=new RepoDBOrganiser(serverProps);
        RepoDBMatch matchRepo=new RepoDBMatch(serverProps);
        RepoDBTicket ticketRepo = new RepoDBTicket(serverProps);

        ServiceOrganiser serviceOrganiser = new ServiceOrganiser(organiserRepo);
        ServiceMatch serviceMatch = new ServiceMatch(matchRepo);
        ServiceTicket serviceTicket = new ServiceTicket(ticketRepo);
        MasterService masterService = new MasterService(serviceMatch,serviceOrganiser,serviceTicket);

        IServices chatServerImpl=new BasketServicesImpl(masterService);
        int chatServerPort=defaultPort;
        try {
            chatServerPort = Integer.parseInt(serverProps.getProperty("chat.server.port"));
        }catch (NumberFormatException nef){
            System.err.println("Wrong  Port Number"+nef.getMessage());
            System.err.println("Using default port "+defaultPort);
        }
        System.out.println("Starting server on port: "+chatServerPort);
        AbstractServer server = new BasketRpcConcurrentServer(chatServerPort, chatServerImpl);
        try {
            server.start();
        } catch (ServerException e) {
            System.err.println("Error starting the server" + e.getMessage());
        }finally {
            try {
                server.stop();
            }catch(ServerException e){
                System.err.println("Error stopping server "+e.getMessage());
            }
        }
    }
}
