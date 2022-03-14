import model.Match;
import model.Organiser;
import model.JdbcUtils;
import model.Ticket;
import repository.RepoDBMatch;
import repository.RepoDBOrganiser;
import repository.RepoDBTicket;

import java.io.FileReader;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

public class MatchesMain {
    public static void main(String[] args) {

        Properties props=new Properties();
        try {
            props.load(new FileReader("bd.config"));
        } catch (IOException e) {
            System.out.println("Cannot find bd.config "+e);
        }
        testRepoOrganiser(props);
        testRepoMatch(props);
        testRepoTicket(props);

        //TODO interfete la fiecare repo, log3j sa afiseze jurnalizarea
    }

    private static void testRepoOrganiser(Properties props){
        Organiser o1 = new Organiser(1,"Gigi");
        RepoDBOrganiser repoOrganiser = new RepoDBOrganiser(props);
        Organiser found = repoOrganiser.findById(1);
        if(found.getName() == "Gigi"){
            repoOrganiser.update(new Organiser(1,"NewGigi"),1);
            repoOrganiser.findAll().forEach(System.out::println);
            repoOrganiser.delete(new Organiser(1," "));
        }
        else{
            Organiser or = new Organiser(1,"Gigi");
            repoOrganiser.add(or);
        }
    }

    private static void testRepoMatch(Properties props){
        Match o1 = new Match(1,"E1","E1","Normal",80,10.3);
        RepoDBMatch repoDBMatch = new RepoDBMatch(props);
        Match found = repoDBMatch.findById(1);
        if(found.getTeam1() == "T1" && found.getTeam2() == "T2"){
            repoDBMatch.update(new Match(1,"E1","E2","Semifinal",80,10.3),1);
            repoDBMatch.findAll().forEach(System.out::println);
            repoDBMatch.delete(new Match(1));
        }
        else{
            Match mt = new Match(1,"E1","E1","Normal",80,10.3);
            repoDBMatch.add(mt);
        }
    }

    private static void testRepoTicket(Properties props) {
        Match m1 = new Match(1,"E1","E1","Normal",80,10.3);
        Ticket t1 = new Ticket(1,2,m1,"Andrei");
        RepoDBTicket repoDBTicket = new RepoDBTicket(props);
        Ticket found = repoDBTicket.findById(1);
        if(found.getCustomerName() == "Andrei"){
            repoDBTicket.update(new Ticket(1,2,m1,"Andrei updated"),1);
            repoDBTicket.findAll().forEach(System.out::println);
            repoDBTicket.delete(new Ticket(1));
        }
        else{
            Ticket tt = new Ticket(1,2,m1,"Andrei");
            repoDBTicket.add(tt);
        }
    }
}
