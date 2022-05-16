package bask.repos;

import bask.model.Organiser;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import org.hibernate.boot.MetadataSources;
import org.hibernate.boot.registry.StandardServiceRegistry;
import org.hibernate.boot.registry.StandardServiceRegistryBuilder;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

public class ORMRepoOrganiser implements IRepoOrganiser{
    private static SessionFactory sessionFactory;

    public ORMRepoOrganiser() {
        final StandardServiceRegistry registry = new StandardServiceRegistryBuilder()
                .configure()
                .build();
        try {
            sessionFactory = new MetadataSources( registry ).buildMetadata().buildSessionFactory();
        }
        catch (Exception e) {
            System.err.println("Exception "+e);
            StandardServiceRegistryBuilder.destroy( registry );
        }
    }

    public void close(){
        if ( sessionFactory != null ) {
            sessionFactory.close();
        }
    }

    @Override
    public Organiser findByNameAndPassword(String name, String password) {
        Organiser foundOrg = new Organiser();
        try(Session session = sessionFactory.openSession()) {
            Transaction tx = null;
            try {
                tx = session.beginTransaction();

                Organiser crit = session.createQuery("from Organiser as o where o.name = :name and o.password = :password", Organiser.class)
                        .setParameter("name", name)
                        .setParameter("password",password)
                        .setMaxResults(1)
                        .uniqueResult();

                foundOrg.setId(crit.getId());
                foundOrg.setName(crit.getName());
                foundOrg.setPassword(crit.getPassword());

                tx.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la find by name "+ex);
                if (tx != null)
                    tx.rollback();
            }
        }
        return foundOrg;
    }

    @Override
    public void add(Organiser el) {
        try(Session session = sessionFactory.openSession()) {
            Transaction tx = null;
            try {
                tx = session.beginTransaction();
                Organiser organiser = new Organiser(el.getName(),el.getPassword());
                session.save(organiser);
                tx.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la inserare "+ex);
                if (tx != null)
                    tx.rollback();
            }
        }
    }

    @Override
    public void delete(Organiser el) {
        try(Session session = sessionFactory.openSession()) {
            Transaction tx = null;
            try {
                tx = session.beginTransaction();

                Organiser crit = session.createQuery("from Organiser as o where o.name = :name and o.password = :password", Organiser.class)
                        .setParameter("name", el.getName())
                        .setParameter("password", el.getPassword())
                        .setMaxResults(1)
                        .uniqueResult();
                System.out.println(crit.toString());
                session.delete(crit);
                tx.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la stergere "+ex);
                if (tx != null)
                    tx.rollback();
            }
        }
    }

    @Override
    public void update(Organiser el, Integer id) {
        try(Session session = sessionFactory.openSession()) {
            Transaction tx = null;
            try {
                tx = session.beginTransaction();

                session.createQuery("update Organiser o set o.name = :name, o.password = :password where o.id = :id")
                        .setParameter("name", el.getName())
                        .setParameter("password", el.getPassword())
                        .setParameter("id", id)
                        .executeUpdate();
                tx.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la update "+ex);
                if (tx != null)
                    tx.rollback();
            }
        }
    }

    @Override
    public Organiser findById(Integer id) {
        Organiser foundOrg = new Organiser();
        try(Session session = sessionFactory.openSession()) {
            Transaction tx = null;
            try {
                tx = session.beginTransaction();

                Organiser crit = session.createQuery("from Organiser as o where o.ID = :id", Organiser.class)
                        .setParameter("id", id)
                        .setMaxResults(1)
                        .uniqueResult();
                foundOrg.setId(crit.getId());
                foundOrg.setName(crit.getName());
                foundOrg.setPassword(crit.getPassword());

                tx.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la stergere "+ex);
                if (tx != null)
                    tx.rollback();
            }
        }
        return foundOrg;
    }

    @Override
    public Iterable<Organiser> findAll() {
        List<Organiser> organisers = new ArrayList<>();
        try(Session session = sessionFactory.openSession()) {
            Transaction tx = null;
            try {
                tx = session.beginTransaction();

                List<Organiser> res = session.createQuery("from Organiser", Organiser.class).list();
                organisers.addAll(res);
                tx.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la stergere "+ex);
                if (tx != null)
                    tx.rollback();
            }
        }
        return organisers;
    }

    @Override
    public Collection<Organiser> getAll() {
        List<Organiser> organisers = new ArrayList<>();
        try(Session session = sessionFactory.openSession()) {
            Transaction tx = null;
            try {
                tx = session.beginTransaction();

                List<Organiser> res = session.createQuery("from Organiser", Organiser.class).list();
                organisers.addAll(res);
                tx.commit();
            } catch (RuntimeException ex) {
                System.err.println("Eroare la stergere "+ex);
                if (tx != null)
                    tx.rollback();
            }
        }
        return organisers;
    }
}
