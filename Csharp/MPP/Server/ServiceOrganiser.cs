﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;
using persistance;

namespace Server
{
    public class ServiceOrganiser
    {
        private IRepoOrganiser repoOrganiser;
        public ServiceOrganiser(IRepoOrganiser repoOrganiser)
        {
            this.repoOrganiser = repoOrganiser;
        }

        public void saveOrganiser(string name, string password)
        {
            Organiser organiser = new Organiser(name, password);
            repoOrganiser.add(organiser);
        }

        public void removeOrganiser(int id)
        {
            Organiser organiser = new Organiser(id);
            repoOrganiser.delete(organiser);
        }

        public void updateOrganiser(int id, string name)
        {
            Organiser organiser = new Organiser(id, name);
            repoOrganiser.update(organiser, id);
        }

        public ICollection<Organiser> getAllOrganisers()
        {
            return repoOrganiser.findAll();
        }

        public Organiser findOrganiserByLogin(string name, string password)
        {
            return repoOrganiser.findByNameAndPassword(name, password);
        }
    }
}
