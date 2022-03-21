package mains;

import controller.LoginController;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.stage.Stage;
import repository.RepoDBMatch;
import repository.RepoDBOrganiser;
import repository.RepoDBTicket;
import service.MasterService;
import service.ServiceMatch;
import service.ServiceOrganiser;
import service.ServiceTicket;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;

public class UIMain extends Application {
    public static void main(String[] args) {
        launch();
    }

    @Override
    public void start(Stage stage) throws IOException {
        MasterService masterService = setMasterService();
        FXMLLoader fxmlLoader = new FXMLLoader(UIMain.class.getResource("../login-view.fxml"));
        Scene scene = new Scene(fxmlLoader.load(), 500, 300);
        LoginController loginController = fxmlLoader.getController();
        loginController.setServiceLogin(masterService);
        stage.setTitle("Basket Match");
        stage.setScene(scene);
        stage.show();
    }

    private MasterService setMasterService(){
        Properties props=new Properties();
        try {
            props.load(new FileReader("bd.config"));
        } catch (IOException e) {
            System.out.println("Cannot find bd.config "+e);
        }
        RepoDBOrganiser repoOrganiser = new RepoDBOrganiser(props);
        RepoDBMatch repoMatch = new RepoDBMatch(props);
        RepoDBTicket repoTicket = new RepoDBTicket(props);
        ServiceOrganiser serviceOrganiser = new ServiceOrganiser(repoOrganiser);
        ServiceMatch serviceMatch = new ServiceMatch(repoMatch);
        ServiceTicket serviceTicket = new ServiceTicket(repoTicket);
        return new MasterService(serviceMatch,serviceOrganiser,serviceTicket);
    }
}