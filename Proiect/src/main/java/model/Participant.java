package model;

import java.io.Serializable;

public class Participant implements Identifiable<Integer>, Serializable {
    private String name;
    private String email;
    private String phone;
    private Integer ID;

    public Participant(String name, String email, String phone) {
        this.name = name;
        this.email = email;
        this.phone = phone;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPhone() {
        return phone;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    @Override
    public String toString() {
        return "Participant{" +
                "name='" + name + '\'' +
                ", email='" + email + '\'' +
                ", phone='" + phone + '\'' +
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
