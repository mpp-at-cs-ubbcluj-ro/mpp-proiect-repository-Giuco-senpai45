package bask.repos;

import bask.model.JdbcUtils;
import bask.model.Match;
import bask.model.Ticket;
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

public class RepoDBTicket implements IRepoTicket{

    private JdbcUtils dbUtils;
    private static final Logger logger= LogManager.getLogger();

    public RepoDBTicket(Properties props) {
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public void add(Ticket el) {
        logger.traceEntry("saving ticket {}", el);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = con.prepareStatement("insert into Ticket (Mid, Quantity, CustomerName) values (?, ?, ?)")){

            preparedStatement.setInt(1, el.getMatch().getId());
            preparedStatement.setInt(2,el.getQuantity());
            preparedStatement.setString(3,el.getCustomerName());
            int result = preparedStatement.executeUpdate();
            logger.trace("Saved {} instances", result);
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
    }

    @Override
    public void delete(Ticket el) {
        logger.traceEntry("deleting ticket {}", el);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = con.prepareStatement("delete from Ticket where Id = ?")){

            preparedStatement.setInt(1, el.getId());
            int result = preparedStatement.executeUpdate();
            logger.trace("Removed {} instances", result);
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
    }

    @Override
    public void update(Ticket el, Integer id) {
        logger.traceEntry("updating ticket {}", el);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = con.prepareStatement("Update Ticket set Mid = ?, Quantity = ?, CustomerName = ? where Id = ?")){

            preparedStatement.setInt(1, el.getMatch().getId());
            preparedStatement.setInt(2, el.getQuantity());
            preparedStatement.setString(3, el.getCustomerName());
            preparedStatement.setInt(4, el.getId());
            int result = preparedStatement.executeUpdate();
            logger.trace("Updated {} instances", result);
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
    }

    @Override
    public Ticket findById(Integer id) {
        logger.traceEntry("Finding ticket {}", id);
        Connection con = dbUtils.getConnection();
        Ticket ticket = new Ticket();
        try(PreparedStatement preparedStatement = con.prepareStatement("select * from Ticket where Id = ?")){
            preparedStatement.setInt(1, id);
            try(ResultSet result = preparedStatement.executeQuery()) {

                while(result.next()){
                    int Id = result.getInt("Id");
                    String customerName = result.getString("CustomerName");
                    Integer mid = result.getInt("Mid");
                    Integer quantity = result.getInt("Quantity");
                    Match match = new Match(mid);
                    ticket.setId(Id);
                    ticket.setMatch(match);
                    ticket.setQuantity(quantity);
                    ticket.setCustomerName(customerName);
                    logger.trace("Found {} instances", ticket);
                }
            }
        } catch (SQLException ex) {
            logger.error(ex);
            System.err.println("Error DB" + ex);
        }
        logger.traceExit();
        return ticket;
    }

    @Override
    public Iterable<Ticket> findAll() {
        logger.traceEntry("find all tickets");
        Connection con = dbUtils.getConnection();
        List<Ticket> tickets = new ArrayList<>();
        try(PreparedStatement preparedStatement = con.prepareStatement("select * from Ticket")){
            try(ResultSet result = preparedStatement.executeQuery()){
                while(result.next()){
                    int Id = result.getInt("Id");
                    String customerName = result.getString("CustomerName");
                    Integer mid = result.getInt("Mid");
                    Integer quantity = result.getInt("Quantity");
                    Match match = new Match(mid);
                    Ticket ticket = new Ticket(Id, quantity, match,customerName);
                    ticket.setId(Id);
                    ticket.setMatch(match);
                    ticket.setQuantity(quantity);
                    ticket.setCustomerName(customerName);
                    tickets.add(ticket);
                }
            }
        }
        catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB" + e);
        }
        logger.traceExit(tickets);
        return tickets;
    }

    @Override
    public Collection<Ticket> getAll() {
        logger.traceEntry("find all tickets");
        Connection con = dbUtils.getConnection();
        List<Ticket> tickets = new ArrayList<>();
        try(PreparedStatement preparedStatement = con.prepareStatement("select * from Ticket")){
            try(ResultSet result = preparedStatement.executeQuery()){
                while(result.next()){
                    int Id = result.getInt("Id");
                    String customerName = result.getString("CustomerName");
                    Integer mid = result.getInt("Mid");
                    Integer quantity = result.getInt("Quantity");
                    Match match = new Match(mid);
                    Ticket ticket = new Ticket(Id, quantity, match,customerName);
                    ticket.setId(Id);
                    ticket.setMatch(match);
                    ticket.setQuantity(quantity);
                    ticket.setCustomerName(customerName);
                    tickets.add(ticket);
                }
            }
        }
        catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB" + e);
        }
        logger.traceExit(tickets);
        return tickets;
    }
}
