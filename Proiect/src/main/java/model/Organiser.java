package model;

import java.io.Serializable;

public class Organiser implements Identifiable<Integer>, Serializable {
    private Integer Id;
    private String name;

    public Organiser(Integer id, String name) {
        Id = id;
        this.name = name;
    }

    public Organiser() {
    }

    public Organiser(String name) {
        this.name = name;
    }

    @Override
    public Integer getID() {
        return Id;
    }

    @Override
    public void setId(Integer id) {
        this.Id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    @Override
    public String toString() {
        return Id + " " + name;
    }
}
