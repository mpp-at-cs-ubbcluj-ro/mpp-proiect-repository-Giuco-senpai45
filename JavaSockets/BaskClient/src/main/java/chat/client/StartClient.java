package chat.client;

import java.io.IOException;
import java.util.Properties;

import bask.network.objectprotocol.BasketServicesObjectProxy;
import bask.network.rpcprotocol.BaskServicesRpcProxy;
import bask.services.IServices;
import chat.client.controllers.LoginController;
import chat.client.controllers.MatchesController;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;


public class StartClient extends Application {
    private Stage primaryStage;

    private static int defaultChatPort = 55555;
    private static String defaultServer = "localhost";

    @Override
    public void start(Stage primaryStage) throws Exception {
        System.out.println("In start");
        Properties clientProps = new Properties();
        try {
            clientProps.load(StartClient.class.getResourceAsStream("/chatclient.properties"));
            System.out.println("Client properties set. ");
            clientProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find chatclient.properties " + e);
            return;
        }
        String serverIP = clientProps.getProperty("chat.server.host", defaultServer);
        int serverPort = defaultChatPort;

        try {
            serverPort = Integer.parseInt(clientProps.getProperty("chat.server.port"));
        } catch (NumberFormatException ex) {
            System.err.println("Wrong port number " + ex.getMessage());
            System.out.println("Using default port: " + defaultChatPort);
        }
        System.out.println("Using server IP " + serverIP);
        System.out.println("Using server port " + serverPort);

        IServices server = new BaskServicesRpcProxy(serverIP, serverPort);

        FXMLLoader loader = new FXMLLoader(getClass().getClassLoader().getResource("login-view.fxml"));
        Parent root=loader.load();


        LoginController ctrl = loader.<LoginController>getController();
        ctrl.setServer(server);

        FXMLLoader cloader = new FXMLLoader(getClass().getClassLoader().getResource("matches-main-view.fxml"));
        Parent croot=cloader.load();

        MatchesController chatCtrl = cloader.<MatchesController>getController();
        chatCtrl.setServer(server);
        ctrl.setParent(croot);
        ctrl.setChatController(chatCtrl);

        primaryStage.setTitle("Basket matches");
        primaryStage.setScene(new Scene(root, 500, 300));
        primaryStage.show();
    }

}
