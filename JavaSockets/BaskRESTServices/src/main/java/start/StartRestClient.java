package start;

import bask.model.Match;
import bask.services.rest.ServiceException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.web.client.RestClientException;
import rest.client.MatchClients;

import java.sql.Time;
import java.sql.Timestamp;
import java.time.ZonedDateTime;
import java.util.List;

public class StartRestClient {
    private final static MatchClients matchesClient=new MatchClients();
    private final static ObjectMapper mapper = new ObjectMapper();

    public static void main(String[] args) {
        try {

            show(() -> {
                var res =  matchesClient.getAll();
                for (var m : res) {
                    Match match = mapper.convertValue(m, Match.class);
                    System.out.println(match.toString());
                }
            });
            System.out.println("\n\n");

            ZonedDateTime dateTime = ZonedDateTime.parse("2022-08-12T22:00:00.000+00:00");
            Timestamp dt = Timestamp.valueOf(dateTime.toLocalDateTime());
            Match meci = new Match("PLec","Salut","Semifinal",140,3.2, dt);

            final Match[] respArr = {new Match()};
            show(() -> {
                respArr[0] = matchesClient.save(meci);
            });

            Match savedMatch = respArr[0];
            System.out.println("SAVED MATCH");
            System.out.println(savedMatch.toString());
            System.out.println("\n\n");

            savedMatch.setTeam1("Brrr");
            savedMatch.setTeam2("krrr");

            System.out.println("UPDATED MATCH");
            show(() -> {
                matchesClient.update(savedMatch);
            });

            System.out.println("FIND BY ID UPDATED MATCH");
            final Match[] foundM = {new Match()};
            show(() -> {
                foundM[0] = matchesClient.findById(savedMatch.getId().toString());
            });

            System.out.println(foundM[0].toString());

            System.out.println("DELETED MATCH");
            show(() -> {
                matchesClient.delete(savedMatch.getId().toString());
            });

            System.out.println("\n\n");
            show(() -> {
                var res =  matchesClient.getAll();
                for (var m : res) {
                    Match match = mapper.convertValue(m, Match.class);
                    System.out.println(match.toString());
                }
            });
        }
        catch(RestClientException ex){
            System.out.println("Exception ... "+ex.getMessage());
        }
    }

    private static void show(Runnable task) {
        try {
            task.run();
        } catch (ServiceException e) {
            //  LOG.error("Service exception", e);
            System.out.println("Service exception"+ e);
        }
    }
}
