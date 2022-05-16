import bask.model.Organiser;
import bask.network.utils.AbstractServer;
import bask.network.utils.BasketProtobuffConcurrentServer;
import bask.network.utils.ServerException;
import bask.repos.ORMRepoOrganiser;
import bask.repos.RepoDBMatch;
import bask.repos.RepoDBOrganiser;
import bask.repos.RepoDBTicket;
import bask.services.IServices;
import chat.server.*;

import java.io.IOException;
import java.util.Properties;

public class StartProtobuffServer {
    private static int defaultPort = 55555;

    public static void main(String[] args) {


        Properties serverProps = new Properties();
        try {
            serverProps.load(StartRpcServer.class.getResourceAsStream("/chatserver.properties"));
            System.out.println("Server properties set. ");
            serverProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find chatserver.properties " + e);
            return;
        }
//        RepoDBOrganiser organiserRepo = new RepoDBOrganiser(serverProps);
        ORMRepoOrganiser organiserRepo = new ORMRepoOrganiser();
        RepoDBMatch matchRepo = new RepoDBMatch(serverProps);
        RepoDBTicket ticketRepo = new RepoDBTicket(serverProps);
        testCrudRepo(organiserRepo);

        ServiceOrganiser serviceOrganiser = new ServiceOrganiser(organiserRepo);
        ServiceMatch serviceMatch = new ServiceMatch(matchRepo);
        ServiceTicket serviceTicket = new ServiceTicket(ticketRepo);
        MasterService masterService = new MasterService(serviceMatch, serviceOrganiser, serviceTicket);

        IServices chatServerImpl = new BasketServicesImpl(masterService);
        int chatServerPort = defaultPort;
        try {
            chatServerPort = Integer.parseInt(serverProps.getProperty("chat.server.port"));
        } catch (NumberFormatException nef) {
            System.err.println("Wrong  Port Number" + nef.getMessage());
            System.err.println("Using default port " + defaultPort);
        }
        System.out.println("Starting server on port: " + chatServerPort);
        AbstractServer server = new BasketProtobuffConcurrentServer(chatServerPort, chatServerImpl);
        try {
            server.start();
        } catch (ServerException e) {
            organiserRepo.close();
            System.err.println("Error starting the server" + e.getMessage());
        }
    }

    private static void testCrudRepo(ORMRepoOrganiser organiserRepo) {
        Organiser orr = new Organiser("o5","p5");
//        organiserRepo.add(orr);
//        Organiser orr = new Organiser();

        organiserRepo.findAll().forEach(System.out::println);
        orr.setName("updatedName");
        orr.setPassword("updatedPassword");
        organiserRepo.update(orr,4);

        organiserRepo.findAll().forEach(System.out::println);
//        organiserRepo.delete(orr);
    }
}
