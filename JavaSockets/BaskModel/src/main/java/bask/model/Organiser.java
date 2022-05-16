package bask.model;

import org.hibernate.annotations.GenericGenerator;

import java.io.Serializable;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Table;


//@Entity
//@Table( name = "Organiser")
public class Organiser implements Identifiable<Integer>, Serializable {
    private Integer id;
    private String name;
    private String password;

    public Organiser(Integer id, String name) {
        this.id = id;
        this.name = name;
    }

    public Organiser(String name, String password) {
        this.name = name;
        this.password = password;
    }

    public Organiser() {
    }

    public Organiser(Integer id){
        this.id = id;
    }

    public Organiser(String name) {
        this.name = name;
    }

    public Organiser(int id, String name, String password) {
        this.id = id;
        this.name = name;
        this.password = password;
    }

    @Override
//    @javax.persistence.Id
//    @GeneratedValue(generator="increment")
//    @GenericGenerator(name="increment", strategy = "increment")
    public Integer getId() {
        return id;
    }

    @Override
    public void setId(Integer id) {
        this.id = id;
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
        return id + " " + name + " " + password;
    }
}
