package model;

import java.io.Serializable;

public class Ticket implements Identifiable<Integer>, Serializable {
    private Integer ID;
    private Integer seat;
    private Long price;
    private Integer matchID;

    public Ticket(Integer seat, Long price, Integer matchID) {
        this.seat = seat;
        this.price = price;
        this.matchID = matchID;
    }

    public Integer getSeat() {
        return seat;
    }

    public void setSeat(Integer seat) {
        this.seat = seat;
    }

    public Long getPrice() {
        return price;
    }

    public void setPrice(Long price) {
        this.price = price;
    }

    public Integer getMatchID() {
        return matchID;
    }

    public void setMatchID(Integer matchID) {
        this.matchID = matchID;
    }

    @Override
    public String toString() {
        return "Ticket{" +
                "ID=" + ID +
                ", seat=" + seat +
                ", price=" + price +
                ", matchID=" + matchID +
                '}';
    }

    @Override
    public Integer getID() {
        return ID;
    }

    @Override
    public void setId(Integer id) {
        ID = id;
    }
}
