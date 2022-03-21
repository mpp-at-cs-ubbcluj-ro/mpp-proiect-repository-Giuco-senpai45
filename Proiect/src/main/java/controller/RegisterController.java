package controller;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import service.MasterService;

public class RegisterController {

    @FXML
    private TextField textName;

    @FXML
    private Button registerBtn;

    @FXML
    private Label registerErrorLabel;

    private MasterService masterService;
    private Stage stage;


    public void setRegisterController(MasterService masterService, Stage stage) {
        this.masterService = masterService;
        this.stage = stage;
    }

    public void registerOrganiser(ActionEvent actionEvent) {
        if(textName.getText().equals("")){
            registerErrorLabel.setText("Name cannot be empty!");
        }
        else {
            String name = textName.getText();
            try {
                masterService.getOrganiserService().saveOrganiser(name);
                registerErrorLabel.setVisible(false);
                resetTextFields();
            }
            catch(Exception e){
                registerErrorLabel.setText(e.getMessage());
                registerErrorLabel.setVisible(true);
            }
        }
    }

    private void resetTextFields(){
        textName.setText(null);
    }

}
