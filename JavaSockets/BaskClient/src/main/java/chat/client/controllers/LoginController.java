package chat.client.controllers;

import bask.model.Organiser;
import bask.services.BasketException;
import bask.services.IServices;
import chat.client.StartClient;
import javafx.application.Platform;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import javafx.stage.WindowEvent;

import java.io.IOException;

public class LoginController {
    private IServices server;
    private MatchesController matchCtrl;
    private Organiser crtUser;
    @FXML
    TextField textName;
    @FXML
    TextField textPassword;
    Parent mainChatParent;

    public void setServer(IServices s){
        server=s;
    }

    public void setParent(Parent p){
        mainChatParent=p;
    }


    public void loginOrganiserHandler(ActionEvent actionEvent) {
        String name = textName.getText();
        String passwd = textPassword.getText();
        crtUser = new Organiser(name, passwd);
        System.out.println("User's password and name " + crtUser);
        try{
            server.login(crtUser, matchCtrl);
            matchCtrl.setLoggedOrganiser(crtUser);
            matchCtrl.initializeMatchWindow();
            //Organiser gotOrg = server.getOrganiserByCredentials(crtUser);
            //System.out.println("This is the user with credentials " + gotOrg.getID()+"  "+ gotOrg.getName()+ gotOrg.getPassword());
            // Util.writeLog("User succesfully logged in "+crtUser.getId());
            //matchCtrl.setLoggedOrganisers();

            Stage stage=new Stage();
            stage.setTitle("Basket Window for " + name);
            stage.setScene(new Scene(mainChatParent, 800, 600));
            stage.setOnCloseRequest(new EventHandler<WindowEvent>() {
                @Override
                public void handle(WindowEvent event) {
                    matchCtrl.logout();
                    System.exit(0);
                }
            });

            stage.show();
            ((Node)(actionEvent.getSource())).getScene().getWindow().hide();

        }   catch (BasketException e) {
            Alert alert = new Alert(Alert.AlertType.INFORMATION);
            alert.setTitle("MPP chat");
            alert.setHeaderText("Authentication failure");
            alert.setContentText("Wrong username or password");
            alert.showAndWait();
        }
    }

    public void registerOrganiserClick(ActionEvent actionEvent) {
    }

    public void  setChatController(MatchesController matchCtrl) {
        this.matchCtrl = matchCtrl;
    }
}
