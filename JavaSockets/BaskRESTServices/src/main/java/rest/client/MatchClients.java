package rest.client;

import bask.model.Match;
import bask.services.rest.ServiceException;
import org.springframework.http.HttpEntity;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.util.LinkedMultiValueMap;
import org.springframework.util.MultiValueMap;
import org.springframework.web.client.HttpClientErrorException;
import org.springframework.web.client.ResourceAccessException;
import org.springframework.web.client.RestTemplate;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.concurrent.Callable;

public class MatchClients {
    public static final String URL = "http://localhost:8080/api/v1/matches";

    private RestTemplate restTemplate = new RestTemplate();

    private <T> T execute(Callable<T> callable) {
        try {
            return callable.call();
        } catch (ResourceAccessException | HttpClientErrorException e) { // server down, resource exception
            throw new ServiceException(e);
        } catch (Exception e) {
            throw new ServiceException(e);
        }
    }

    public List<?> getAll() {
        return execute(() -> restTemplate.getForObject(URL, List.class));
    }

    public Match findById(String id) {
        return execute(() -> restTemplate.getForObject(String.format("%s/%s", URL, id), Match.class));
    }

    public Match save(Match match){
        return execute(() -> restTemplate.postForObject(URL, match, Match.class));
    }

    public void update(Match match){
        execute(() -> {
            restTemplate.put(String.format("%s/%s", URL, match.getId().toString()), match);
            return null;
        });
    }

    public void delete(String id){
        execute(() -> {
            restTemplate.delete(String.format("%s/%s", URL, id));
            return null;
        });
    }
}
