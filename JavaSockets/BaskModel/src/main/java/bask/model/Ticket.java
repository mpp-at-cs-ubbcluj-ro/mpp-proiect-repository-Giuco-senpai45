package bask.model;

import java.io.Serializable;

public class Ticket implements Identifiable<Integer>, Serializable {
    private Integer ID;
    private Integer quantity;
    private Match match;
    private String customerName;

    public Ticket() {

    }

    public Integer getQuantity() {
        return quantity;
    }

    public void setQuantity(Integer quantity) {
        this.quantity = quantity;
    }

    public Match getMatch() {
        return match;
    }

    public void setMatch(Match match) {
        this.match = match;
    }

    public Ticket(Integer quantity, Match match, String customerName) {
        this.quantity = quantity;
        this.match = match;
        this.customerName = customerName;
    }

    public Ticket(Integer ID, Integer quantity, Match match, String customerName) {
        this.ID = ID;
        this.quantity = quantity;
        this.match = match;
        this.customerName = customerName;
    }

    public Ticket(Integer ID) {
        this.ID = ID;
    }

    public String getCustomerName() {
        return customerName;
    }

    public void setCustomerName(String customerName) {
        this.customerName = customerName;
    }

    @Override
    public Integer getId() {
        return ID;
    }

    @Override
    public void setId(Integer id) {
        ID = id;
    }

    @Override
    public String toString() {
        return ID + " " +  customerName + " " + quantity + " " + match.toString();
    }
}
