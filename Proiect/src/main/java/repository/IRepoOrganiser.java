package repository;

import model.Organiser;

public interface IRepoOrganiser extends Repository<Organiser,Integer>{

    Organiser findByNameAndPassword(String name, String password);
}
