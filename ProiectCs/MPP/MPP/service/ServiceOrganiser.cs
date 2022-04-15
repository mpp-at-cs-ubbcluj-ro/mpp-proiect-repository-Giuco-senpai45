using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP.model;
using MPP.repository;

namespace MPP.service
{
    public class ServiceOrganiser
    {
        private IRepoOrganiser repoOrganiser;
        public ServiceOrganiser(IRepoOrganiser repoOrganiser)
        {
            this.repoOrganiser = repoOrganiser;
        }

        public void saveOrganiser(String name,String password)
        {
            Organiser organiser = new Organiser(name,password);
            repoOrganiser.add(organiser);
        }

        public void removeOrganiser(int id)
        {
            Organiser organiser = new Organiser(id);
            repoOrganiser.delete(organiser);
        }

        public void updateOrganiser(int id, String name)
        {
            Organiser organiser = new Organiser(id, name);
            repoOrganiser.update(organiser, id);
        }

        public ICollection<Organiser> getAllOrganisers()
        {
            return repoOrganiser.findAll();
        }

        public Organiser findOrganiserByLogin(String name, String password)
        {
            return repoOrganiser.findByNameAndPassword(name, password);
        }
    }
}
