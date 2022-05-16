package bask.services.rest;

import bask.model.Match;
import bask.repos.RepoDBMatch;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.sql.Timestamp;
import java.time.ZonedDateTime;
import java.util.Date;
import java.util.List;

@RestController
@RequestMapping("/api/v1")
public class BaskMatchController {

    @Autowired
    private RepoDBMatch matchesRepo;

    @RequestMapping(value = "/matches", method = RequestMethod.GET)
    public List<Match> getAll() {
        System.out.println("Get all matches");
        return matchesRepo.getAll().stream().toList();
    }

    @RequestMapping(path = "/matches", method = RequestMethod.POST)
    public  ResponseEntity<?> saveMatch(@RequestBody Match match) {
        try {
            Match m = matchesRepo.addREST(match);
            return new ResponseEntity<Match>(m, HttpStatus.OK);
        }
        catch(Exception e){
            return new ResponseEntity<String>("Match not saved", HttpStatus.UNPROCESSABLE_ENTITY);
        }
    }

    @RequestMapping(path = "/matches/{id}", method = RequestMethod.PUT)
    public ResponseEntity<?> updateMatch(@RequestBody Match match,@PathVariable String id) {
        try {
            match.setId(Integer.parseInt(id));
            matchesRepo.update(match, Integer.parseInt(id));

            Match m = matchesRepo.findById(Integer.parseInt(id));
            if (m == null)
                return new ResponseEntity<String>("Match not found", HttpStatus.NOT_FOUND);
            else
                return new ResponseEntity<Match>(m, HttpStatus.OK);
        }
        catch(Exception e){
            return new ResponseEntity<String>("Match not updated", HttpStatus.UNPROCESSABLE_ENTITY);
        }
    }

    @RequestMapping(path = "/matches/{id}", method = RequestMethod.DELETE)
    public ResponseEntity<?> deleteMatch(@PathVariable String id) {
        try {
            Match match = matchesRepo.findById(Integer.parseInt(id));
            matchesRepo.delete(match);

            return new ResponseEntity<Match>(match, HttpStatus.OK);
        }
        catch(Exception e){
            return new ResponseEntity<String>("Match not saved", HttpStatus.UNPROCESSABLE_ENTITY);
        }
    }

    @RequestMapping(value = "/matches/{id}", method = RequestMethod.GET)
    public ResponseEntity<?> getById(@PathVariable String id) {
        Match m = matchesRepo.findById(Integer.parseInt(id));
        if (m == null)
            return new ResponseEntity<String>("Match not found", HttpStatus.NOT_FOUND);
        else
            return new ResponseEntity<Match>(m, HttpStatus.OK);
    }
}