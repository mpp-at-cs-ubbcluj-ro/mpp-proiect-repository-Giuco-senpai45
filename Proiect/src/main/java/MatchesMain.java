import model.Participant;
import repository.AbstractRepository;

public class MatchesMain {
    public static void main(String[] args) {
        Participant p1 = new Participant("P1","email1@mail.com","1234");
        Participant p2 = new Participant("P2","email2@mail.com","5677");
        Participant p3 = new Participant("P3","email3@mail.com","5933");
        p1.setId(1);
        p2.setId(2);
        p3.setId(3);
        AbstractRepository<Participant, Integer> repoParticipants = new AbstractRepository<>();
        repoParticipants.add(p1);
        repoParticipants.add(p2);
        repoParticipants.add(p3);
        repoParticipants.getAll().forEach(System.out::println);
    }
}
