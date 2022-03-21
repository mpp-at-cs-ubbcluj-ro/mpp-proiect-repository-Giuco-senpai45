package model;

import java.io.Serializable;

public class Organiser implements Identifiable<Integer>, Serializable {
    private Integer Id;
    private String name;
    private String password;

    public Organiser(Integer id, String name) {
        Id = id;
        this.name = name;
    }

    public Organiser(String name, String password) {
        this.name = name;
        this.password = password;
    }

    public Organiser() {
    }

    public Organiser(Integer id){
        this.Id = id;
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

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public void setName(String name) {
        this.name = name;
    }

    @Override
    public String toString() {
        return Id + " " + name;
    }
}
