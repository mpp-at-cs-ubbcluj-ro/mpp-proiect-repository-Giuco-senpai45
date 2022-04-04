package bask.model;

import java.io.Serializable;
import java.sql.Timestamp;
import java.util.Date;

public class Match implements Identifiable<Integer>, Serializable {

    private Integer ID;
    private String team1;
    private String team2;
    private String type;
    private Integer nrOfSeats;
    private Timestamp date;
    private Double price;
    private String state;

    public Match(Integer ID, String team1, String team2, String type, Integer nrOfSeats, Double price,Timestamp date) {
        this.ID = ID;
        this.team1 = team1;
        this.team2 = team2;
        this.type = type;
        this.nrOfSeats = nrOfSeats;
        this.price = price;
        this.date = date;
    }

    public Match(String team1, String team2, String type, Integer nrOfSeats, Double price,Timestamp date) {
        this.team1 = team1;
        this.team2 = team2;
        this.type = type;
        this.nrOfSeats = nrOfSeats;
        this.price = price;
        this.date = date;
    }

    public Match() {

    }

    public Match(Integer mid) {
        this.ID = mid;
    }

    public String getTeam1() {
        return team1;
    }

    public void setTeam1(String team1) {
        this.team1 = team1;
    }

    public String getTeam2() {
        return team2;
    }

    public void setTeam2(String team2) {
        this.team2 = team2;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public Integer getNrOfSeats() {
        return nrOfSeats;
    }

    public void setNrOfSeats(Integer nrOfSeats) {
        this.nrOfSeats = nrOfSeats;
    }

    public Double getPrice() {
        return price;
    }

    public void setPrice(Double price) {
        this.price = price;
    }

    public Timestamp getDate() {
        return date;
    }

    public void setDate(Timestamp date) {
        this.date = date;
    }

    @Override
    public Integer getID() {
        return ID;
    }

    @Override
    public void setId(Integer id) {
        ID = id;
    }

    public String getState() {
        return state;
    }

    public void setState(String state) {
        this.state = state;
    }

    @Override
    public String toString() {
        return new Date(date.getTime()) + "  " + team1 + "  v.s " + team2 + "  " + type + "  " + price + "  " + nrOfSeats;
    }
}
