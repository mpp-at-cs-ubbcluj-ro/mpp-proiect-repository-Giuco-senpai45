package repository;

import model.JdbcUtils;
import model.Organiser;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Properties;

public class RepoDBOrganiser implements Repository<Organiser, Integer> {

    private JdbcUtils dbUtils;
    private static final Logger logger= LogManager.getLogger();

    public RepoDBOrganiser(Properties props) {
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public void add(Organiser el) {
        logger.traceEntry("saving organiser {}", el);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = con.prepareStatement("insert into Organiser (Id, Name) values (?, ?)")){

            preparedStatement.setInt(1, el.getID());
            preparedStatement.setString(2, el.getName());
            int result = preparedStatement.executeUpdate();
            logger.trace("Saved {} instances", result);
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
    }

    @Override
    public void delete(Organiser el) {
        logger.traceEntry("deleting organiser {}", el);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = con.prepareStatement("delete from Organiser where Id = ?")){

            preparedStatement.setInt(1, el.getID());
            int result = preparedStatement.executeUpdate();
            logger.trace("Removed {} instances", result);
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
    }

    @Override
    public void update(Organiser el, Integer id) {
        logger.traceEntry("updating organiser {}", el);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = con.prepareStatement("Update Organiser set Name = ? where Id = ?")){

            preparedStatement.setString(1, el.getName());
            preparedStatement.setInt(2, el.getID());
            int result = preparedStatement.executeUpdate();
            logger.trace("Updated {} instances", result);
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
    }

    @Override
    public Organiser findById(Integer id) {
        logger.traceEntry("Finding organiser {}", id);
        Connection con = dbUtils.getConnection();
        Organiser organiser = new Organiser();
        try(PreparedStatement preparedStatement = con.prepareStatement("select * from Organiser where Id = ?")){
            preparedStatement.setInt(1, id);
            try(ResultSet result = preparedStatement.executeQuery()) {

                while(result.next()){
                    int Id = result.getInt("Id");
                    String name = result.getString("Name");
                    organiser.setId(Id);
                    organiser.setName(name);
                logger.trace("Found {} instances", organiser);
                }
            }
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
        return organiser;
    }

    @Override
    public Iterable<Organiser> findAll() {
        logger.traceEntry("find all organisers");
        Connection con = dbUtils.getConnection();
        List<Organiser> organisers = new ArrayList<>();
        try(PreparedStatement preparedStatement = con.prepareStatement("select * from Organiser")){
            try(ResultSet result = preparedStatement.executeQuery()){
                while(result.next()){
                    int id = result.getInt("Id");
                    String name = result.getString("Name");
                    Organiser organiser = new Organiser(id,name);
                    organisers.add(organiser);
                }
            }
        }
        catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB" + e);
        }
        logger.traceExit(organisers);
        return organisers;
    }

    @Override
    public Collection<Organiser> getAll() {
        logger.traceEntry("get all organisers");
        Connection con = dbUtils.getConnection();
        List<Organiser> organisers = new ArrayList<>();
        try(PreparedStatement preparedStatement = con.prepareStatement("select * from Organiser")){
            try(ResultSet result = preparedStatement.executeQuery()){
                while(result.next()){
                    int id = result.getInt("Id");
                    String name = result.getString("Name");
                    Organiser organiser = new Organiser(id,name);
                    organisers.add(organiser);
                }
            }
        }
        catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB" + e);
        }
        logger.traceExit(organisers);
        return organisers;
    }
}
