package chat.client.controllers;

import bask.model.Match;
import bask.model.Organiser;
import bask.model.Ticket;
import bask.services.BasketException;
import bask.services.IObserver;
import bask.services.IServices;
import javafx.application.Platform;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.*;
import javafx.scene.input.MouseEvent;
import javafx.util.Callback;

import java.net.URL;
import java.util.List;
import java.util.ResourceBundle;

public class MatchesController implements Initializable, IObserver {

    @FXML
    private Button sellTicketBtn;

    @FXML
    private Button filterDescendingBtn;

    @FXML
    private ListView<Match> matchesListView;

    @FXML
    private Label loginErrorLabel;

    @FXML
    private Label labelErrorMatch;

    @FXML
    private TextField textMid;

    @FXML
    private TextField textQuantity;

    @FXML
    private TextField textCustomerName;

    ObservableList<Organiser> organisers = FXCollections.observableArrayList();
    private IServices server;
    private Organiser loggedOrganiser;

    public void initializeMatchWindow(){
        try {
            List<Match> matches = server.getMatchesList();
            matchesListView.setItems(FXCollections.observableArrayList(matches));

            matchesListView.setOnMouseClicked(new EventHandler<MouseEvent>() {
                @Override
                public void handle(MouseEvent event) {
                    fillFieldsMatch(event);
                }
            });

            matchesListView.setCellFactory(new Callback<>() {
                @Override
                public ListCell<Match> call(ListView<Match> param) {
                    return new ListCell<>() {
                        @Override
                        protected void updateItem(Match match, boolean empty) {
                            super.updateItem(match, empty);

                            if (match == null || empty) {
                                setText("");
                                setStyle("-fx-text-fill: -fx-text-base-color;");
                            } else {
                                // Logic for setting the right color here:
                                if (match.getNrOfSeats() != 0)
                                {
                                    setStyle("-fx-text-fill: black;");
                                    setText(match.toString());
                                }
                                else{
                                    setText(match.getTeam1() + " v.s " + match.getTeam2() + " SOLD OUT");
                                    setStyle("-fx-text-fill: red;");
                                }
                            }
                        }
                    };
                }
            });
        }
        catch(BasketException e){
            Alert alert = new Alert(Alert.AlertType.INFORMATION);
            alert.setTitle("MPP chat");
            alert.setHeaderText("Authentication failure");
            alert.setContentText("Wrong username or password");
            alert.showAndWait();
        }
    }

    @Override
    public void listUpdated(List<Match> matches) throws BasketException {
        Platform.runLater(()-> matchesListView.setItems(FXCollections.observableArrayList(matches)) );
    }

    @Override
    public void ticketSold(Ticket ticket) throws BasketException {

    }

    @Override
    public void organiserLoggedIn(Organiser organiser) throws BasketException {
    }

    @Override
    public void organiserLoggedOut(Organiser organiser) throws BasketException {
    }

    @Override
    public void initialize(URL location, ResourceBundle resources) {

    }

    public void setServer(IServices s) {
        server=s;
    }

    public void logout() {
        try {
            server.logout(loggedOrganiser, this);
        } catch (BasketException e) {
            System.out.println("Logout error " + e);
        }
    }

    public void setLoggedOrganiser(Organiser o) {
        this.loggedOrganiser = o;
    }


    public void sellTicketHandler(ActionEvent actionEvent) {
        try {
            Integer quantity = Integer.parseInt(textQuantity.getText());
            String customerName = textCustomerName.getText();
            Integer mid = Integer.parseInt(textMid.getText());
            Match match = new Match(mid);
            Ticket ticket = new Ticket(quantity,match,customerName);
            server.ticketSold(ticket);

            List<Match> matches = server.getMatchesList();
            matchesListView.setItems(FXCollections.observableArrayList(matches));
            server.sendUpdatedList(matches);
        }
        catch(BasketException e){
            Alert alert = new Alert(Alert.AlertType.INFORMATION);
            alert.setTitle("MPP chat");
            alert.setHeaderText("Authentication failure");
            alert.setContentText("Wrong username or password");
            alert.showAndWait();
        }
    }

    public void filterMatchesHandler(ActionEvent actionEvent) {
    }

    public void fillFieldsMatch(MouseEvent mouseEvent) {
        Match match = matchesListView.getSelectionModel().getSelectedItem();
        textMid.setText(match.getID().toString());
    }
}
