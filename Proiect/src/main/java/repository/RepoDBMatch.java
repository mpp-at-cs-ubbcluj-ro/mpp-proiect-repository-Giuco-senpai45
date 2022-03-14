package repository;

import model.JdbcUtils;
import model.Match;
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

public class RepoDBMatch implements Repository<Match, Integer> {

    private JdbcUtils dbUtils;
    private static final Logger logger= LogManager.getLogger();

    public RepoDBMatch(Properties props) {
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public void add(Match el) {
        logger.traceEntry("saving match {}", el);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = con.prepareStatement("insert into Match (Id, Team1, Team2, Type, NrOfSeats, Price) values (?, ?, ?, ?, ?, ?)")){

            preparedStatement.setInt(1, el.getID());
            preparedStatement.setString(2, el.getTeam1());
            preparedStatement.setString(3, el.getTeam2());
            preparedStatement.setString(4, el.getType());
            preparedStatement.setInt(5, el.getNrOfSeats());
            preparedStatement.setDouble(6, el.getPrice());
            int result = preparedStatement.executeUpdate();
            logger.trace("Saved {} instances", result);
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
    }

    @Override
    public void delete(Match el) {
        logger.traceEntry("deleting match {}", el);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = con.prepareStatement("delete from Match where Id = ?")){

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
    public void update(Match el, Integer id) {
        logger.traceEntry("updating match {}", el);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = con.prepareStatement("Update Match set Team1 = ?,Team2 = ?,Type = ?, NrOfSeats = ?, Price = ?  where Id = ?")){

            preparedStatement.setString(1, el.getTeam1());
            preparedStatement.setString(2, el.getTeam2());
            preparedStatement.setString(3, el.getType());
            preparedStatement.setInt(4, el.getID());
            preparedStatement.setDouble(5, el.getPrice());
            preparedStatement.setInt(6,el.getID());
            int result = preparedStatement.executeUpdate();
            logger.trace("Updated {} instances", result);
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
    }

    @Override
    public Match findById(Integer id) {
        logger.traceEntry("Finding match {}", id);
        Connection con = dbUtils.getConnection();
        Match match = new Match();
        try(PreparedStatement preparedStatement = con.prepareStatement("select * from Match where Id = ?")){
            preparedStatement.setInt(1, id);
            try(ResultSet result = preparedStatement.executeQuery()) {

                while(result.next()){
                    int Id = result.getInt("Id");
                    String team1 = result.getString("Team1");
                    String team2 = result.getString("Team2");
                    String type = result.getString("Type");
                    Integer nrOfSeats = result.getInt("NrOfSeats");
                    Double price = result.getDouble("Price");
                    match.setId(Id);
                    match.setTeam1(team1);
                    match.setTeam1(team2);
                    match.setType(type);
                    match.setNrOfSeats(nrOfSeats);
                    match.setPrice(price);
                    logger.trace("Found {} instances", match);
                }
            }
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
        return match;
    }

    @Override
    public Iterable<Match> findAll() {
        logger.traceEntry("find all matches");
        Connection con = dbUtils.getConnection();
        List<Match> matches = new ArrayList<>();
        try(PreparedStatement preparedStatement = con.prepareStatement("select * from Match")){
            try(ResultSet result = preparedStatement.executeQuery()){
                while(result.next()){
                    int idb = result.getInt("Id");
                    String team1 = result.getString("Team1");
                    String team2 = result.getString("Team2");
                    String type = result.getString("Type");
                    Integer nrOfSeats = result.getInt("NrOfSeats");
                    Double price = result.getDouble("Price");
                    Match match = new Match(idb, team1, team2, type, nrOfSeats ,price);
                    matches.add(match);
                }
            }
        }
        catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB" + e);
        }
        logger.traceExit(matches);
        return matches;
    }

    @Override
    public Collection<Match> getAll() {
        logger.traceEntry("find all matches");
        Connection con = dbUtils.getConnection();
        List<Match> matches = new ArrayList<>();
        try(PreparedStatement preparedStatement = con.prepareStatement("select * from Match")){
            try(ResultSet result = preparedStatement.executeQuery()){
                while(result.next()){
                    int idb = result.getInt("Id");
                    String team1 = result.getString("Team1");
                    String team2 = result.getString("Team2");
                    String type = result.getString("Type");
                    Integer nrOfSeats = result.getInt("NrOfSeats");
                    Double price = result.getDouble("Price");
                    Match match = new Match(idb, team1, team2, type, nrOfSeats ,price);
                    matches.add(match);
                }
            }
        }
        catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB" + e);
        }
        logger.traceExit(matches);
        return matches;
    }
}
