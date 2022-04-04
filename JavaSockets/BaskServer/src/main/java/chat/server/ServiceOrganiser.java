package chat.server;

import bask.model.Organiser;
import bask.repos.IRepoOrganiser;

import java.util.Collection;

public class ServiceOrganiser {
    IRepoOrganiser repoOrganiser;

    public ServiceOrganiser(IRepoOrganiser repoOrganiser) {
        this.repoOrganiser = repoOrganiser;
    }

    public void saveOrganiser(String name)
    {
        Organiser organiser = new Organiser(name);
        repoOrganiser.add(organiser);
    }
    
    public void removeOrganiser(Integer id){
        Organiser organiser = new Organiser(id);
        repoOrganiser.delete(organiser);
    }

    public void updateOrganiser(Integer id,String name)
    {
        Organiser organiser = new Organiser(id,name);
        repoOrganiser.update(organiser,id);
    }

    public Collection<Organiser> getAllOrganisers(){
        return repoOrganiser.getAll();
    }

    public Organiser findOrganiserByLogin(String name,String password){
        return repoOrganiser.findByNameAndPassword(name,password);
    }

}
