package model;

import java.io.Serializable;

public class Match implements Identifiable<Integer>, Serializable {

    private Integer ID;
    private String team1;
    private String team2;
    private String type;
    private Integer nrOfSeats;
    private Double price;

    public Match(Integer ID, String team1, String team2, String type, Integer nrOfSeats, Double price) {
        this.ID = ID;
        this.team1 = team1;
        this.team2 = team2;
        this.type = type;
        this.nrOfSeats = nrOfSeats;
        this.price = price;
    }

    public Match(String team1, String team2, String type, Integer nrOfSeats, Double price) {
        this.team1 = team1;
        this.team2 = team2;
        this.type = type;
        this.nrOfSeats = nrOfSeats;
        this.price = price;
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

    @Override
    public Integer getID() {
        return ID;
    }

    @Override
    public void setId(Integer id) {
        ID = id;
    }

    @Override
    public String toString() {
        return ID + " " + team1 + " " + team2 +  " " + type + " " + nrOfSeats + " " + price;
    }
}
