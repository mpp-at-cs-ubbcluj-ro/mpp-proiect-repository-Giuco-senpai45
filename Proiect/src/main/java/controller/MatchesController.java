package controller;

import javafx.collections.FXCollections;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.scene.control.*;
import javafx.scene.input.MouseEvent;
import javafx.stage.Stage;
import javafx.util.Callback;
import model.Match;
import model.Organiser;
import service.MasterService;

import java.sql.Timestamp;

public class MatchesController {

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

    @FXML
    private TableView<Match> matchesTableView;
    @FXML private TableColumn<Match, Timestamp> dateColumn;
    @FXML private TableColumn<Match, String> team1Column;
    @FXML private TableColumn<Match, String> team2Column;
    @FXML private TableColumn<Match, String> typeColumn;
    @FXML private TableColumn<Match, Integer> nrseatsColumn;
    @FXML private TableColumn<Match, Double> priceColumn;

    private Stage mainStage;
    private Organiser loggedOrganiser;
    private MasterService masterService;

    public void loadAppLoggedUser(Organiser connectedOrganiser, Stage stage, MasterService masterService) {
        this.mainStage = stage;
        this.masterService = masterService;
        this.loggedOrganiser = connectedOrganiser;
        loadMatches();
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

    public void loadMatches(){
        var matches = masterService.getMatchService().getAllMatches();
        matchesListView.setItems(FXCollections.observableArrayList(matches));
//        dateColumn.setCellFactory(new PropertyValueFactory<Match, Timestamp>("Date"));
    }

    public void sellTicketHandler(ActionEvent actionEvent) {
        Integer quantity = Integer.parseInt(textQuantity.getText());
        String customerName = textCustomerName.getText();
        Integer mid = Integer.parseInt(textMid.getText());
        try {
            masterService.sellTicketForMatch(mid,quantity,customerName);
            loadMatches();
            resetTextFields();
        }
        catch (Exception e){
            e.printStackTrace();
        }
    }

    public void filterMatchesHandler(ActionEvent actionEvent) {
        var matches = masterService.getMatchService().getDescdendingMatchesNoOfSeats();
        matchesListView.setItems(FXCollections.observableArrayList(matches));
    }

    public void fillFieldsMatch(MouseEvent mouseEvent) {
        Match match = matchesListView.getSelectionModel().getSelectedItem();
        textMid.setText(match.getID().toString());
    }

    private void resetTextFields(){
        textMid.setText(null);
        textCustomerName.setText(null);
        textQuantity.setText(null);
    }
}