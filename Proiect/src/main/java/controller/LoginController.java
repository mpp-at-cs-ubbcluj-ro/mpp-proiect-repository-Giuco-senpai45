package controller;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.image.Image;
import javafx.stage.Stage;
import model.Match;
import model.Organiser;
import service.MasterService;
import mains.UIMain;

import java.io.IOException;
import java.sql.Timestamp;

public class LoginController {

    @FXML
    private Button loginBtn;

    @FXML
    private Button registerBtn;

    @FXML
    private Label loginErrorLabel;

    @FXML
    private TextField textPassword;

    @FXML
    private TextField textName;



    private MasterService masterService;

    public void registerOrganiserClick(ActionEvent actionEvent) {
        Stage stage = new Stage();
        FXMLLoader fxmlLoader = new FXMLLoader(UIMain.class.getResource("../register-org-view.fxml"));
        Scene scene = null;
        try {
            scene = new Scene(fxmlLoader.load());
            RegisterController registerController = fxmlLoader.getController();
            registerController.setRegisterController(masterService, stage);
        }
        catch(IOException e) {
            e.printStackTrace();
        }
        stage.setTitle("Registring");
        stage.setScene(scene);
        stage.show();
    }

    public void setServiceLogin(MasterService masterService) {
        this.masterService = masterService;
    }


    public void loginOrganiserHandler(ActionEvent actionEvent) {
        if(textName.getText().equals("")){
            loginErrorLabel.setText("Name cannot be empty!");
        }
        else if (textPassword.getText().equals("")) {
            loginErrorLabel.setText("Password cannot be empty!");
        }
        else {
            String name = textName.getText();
            String password = textPassword.getText();
            try {
                Organiser loggedOrganiser = masterService.getOrganiserService().findOrganiserByLogin(name,password);
                if(loggedOrganiser == null){
                    loginErrorLabel.setText("We couldn't find that username!");
                    resetTextFields();
                    return;
                }
                else {
                    connectUser(loggedOrganiser, actionEvent);
                }
                loginErrorLabel.setVisible(false);
            }
            catch(Exception e){
                loginErrorLabel.setText(e.getMessage());
                loginErrorLabel.setVisible(true);
            }
        }
        resetTextFields();
    }

    public void connectUser(Organiser connectedOrganiser, ActionEvent actionEvent)
    {
        Stage stage = new Stage();
        FXMLLoader fxmlLoader = new FXMLLoader(UIMain.class.getResource("../matches-main-view.fxml"));
        Scene scene = null;
        try {
            scene = new Scene(fxmlLoader.load());
            MatchesController matchesController = fxmlLoader.getController();
            matchesController.loadAppLoggedUser(connectedOrganiser,stage,masterService);
        }
        catch(IOException e) {
            e.printStackTrace();
        }
        stage.sizeToScene();
        stage.setScene(scene);
        stage.setTitle("Matches");
        stage.setResizable(false);
        stage.show();
    }

    private void resetTextFields(){

        textName.setText(null);
        textPassword.setText(null);
    }
}
