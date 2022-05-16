package bask.network.protobuffprotocol;

import bask.model.Match;
import bask.model.Organiser;
import bask.model.Ticket;

import java.sql.Timestamp;
import java.time.Instant;

public class ProtoUtils {

    public static BasketProtobufs.BasketRequest createLoginRequest(Organiser user){
        BasketProtobufs.Organiser userDTO=BasketProtobufs.Organiser.newBuilder().setId(user.getId()).setName(user.getName()).setPasswd(user.getPassword()).build();
        BasketProtobufs.BasketRequest request= BasketProtobufs.BasketRequest.newBuilder().setType(BasketProtobufs.BasketRequest.Type.Login)
                .setUser(userDTO).build();
        return request;
    }

    public static BasketProtobufs.BasketRequest createLogoutRequest(Organiser user){
        BasketProtobufs.Organiser userDTO=BasketProtobufs.Organiser.newBuilder().setId(user.getId()).setName(user.getName()).setPasswd(user.getPassword()).build();
        BasketProtobufs.BasketRequest request= BasketProtobufs.BasketRequest.newBuilder().setType(BasketProtobufs.BasketRequest.Type.Logout)
                .setUser(userDTO).build();
        return request;
    }

    public static BasketProtobufs.BasketRequest createGetMatchesRequest()
    {
        BasketProtobufs.BasketRequest request= BasketProtobufs.BasketRequest.newBuilder()
                .setType(BasketProtobufs.BasketRequest.Type.GetMatches)
                .build();
        return request;
    }

    public static BasketProtobufs.BasketRequest createUpdateMatchesRequest(Ticket ticket)
    {
        Match match = ticket.getMatch();
//        Instant instant = match.getDate().toInstant();
//        com.google.protobuf.Timestamp date = com.google.protobuf.Timestamp.newBuilder()
//                .setSeconds(instant.getEpochSecond())
//                .setNanos(instant.getNano()).build();

        BasketProtobufs.Match matchDTO = BasketProtobufs.Match.newBuilder().setId(match.getId()).build();

        BasketProtobufs.Ticket ticketDTO=BasketProtobufs.Ticket.newBuilder().setMatch(matchDTO).setQuantity(ticket.getQuantity()).setCustomerName(ticket.getCustomerName()).build();
        BasketProtobufs.BasketRequest request= BasketProtobufs.BasketRequest.newBuilder()
                .setType(BasketProtobufs.BasketRequest.Type.UpdateMatches)
                .setTicket(ticketDTO)
                .build();
        return request;
    }

    public static BasketProtobufs.BasketResponse createOkResponse(){
        BasketProtobufs.BasketResponse response=BasketProtobufs.BasketResponse.newBuilder()
                .setType(BasketProtobufs.BasketResponse.Type.Ok).build();
        return response;
    }

    public static BasketProtobufs.BasketResponse createErrorResponse(String text){
        BasketProtobufs.BasketResponse response=BasketProtobufs.BasketResponse.newBuilder()
                .setType(BasketProtobufs.BasketResponse.Type.Error)
                .setError(text).build();
        return response;
    }

    public static BasketProtobufs.BasketResponse createLoggedInResponse(Organiser user){
        BasketProtobufs.Organiser userDTO=BasketProtobufs.Organiser.newBuilder().setId(user.getId()).setName(user.getName()).setPasswd(user.getPassword()).build();

        BasketProtobufs.BasketResponse response=BasketProtobufs.BasketResponse.newBuilder()
                .setType(BasketProtobufs.BasketResponse.Type.OrgLoggedIn)
                .setUser(userDTO).build();
        return response;
    }

    public static BasketProtobufs.BasketResponse createLoggedOutResponse(Organiser user){
        BasketProtobufs.Organiser userDTO=BasketProtobufs.Organiser.newBuilder().setId(user.getId()).setName(user.getName()).setPasswd(user.getPassword()).build();

        BasketProtobufs.BasketResponse response=BasketProtobufs.BasketResponse.newBuilder()
                .setType(BasketProtobufs.BasketResponse.Type.OrgLoggedOut)
                .setUser(userDTO).build();
        return response;
    }

    public static BasketProtobufs.BasketResponse createGetMatchesResponse(Match[] matches){
        BasketProtobufs.BasketResponse.Builder response=BasketProtobufs.BasketResponse.newBuilder()
                .setType(BasketProtobufs.BasketResponse.Type.GotMatches);
        for(Match match : matches)
        {
            Instant instant = match.getDate().toInstant();
            com.google.protobuf.Timestamp date = com.google.protobuf.Timestamp.newBuilder()
                    .setSeconds(instant.getEpochSecond())
                    .setNanos(instant.getNano()).build();

            BasketProtobufs.Match matchDTO = BasketProtobufs.Match.newBuilder().setId(match.getId()).setTeam1(match.getTeam1())
                    .setTeam2(match.getTeam2()).setType(match.getType()).setPrice(match.getPrice()).setNrOfSeats(match.getNrOfSeats()).setDate(date).build();
            response.addMatches(matchDTO);
        }
        return response.build();
    }

    public static BasketProtobufs.BasketResponse createUpdateMatchesResponse(Match[] matches)
    {
        BasketProtobufs.BasketResponse.Builder response=BasketProtobufs.BasketResponse.newBuilder()
                .setType(BasketProtobufs.BasketResponse.Type.NewMatchList);
        for(Match match : matches)
        {
            Instant instant = match.getDate().toInstant();
            com.google.protobuf.Timestamp date = com.google.protobuf.Timestamp.newBuilder()
                    .setSeconds(instant.getEpochSecond())
                    .setNanos(instant.getNano()).build();
            BasketProtobufs.Match matchDTO = BasketProtobufs.Match.newBuilder().setId(match.getId()).setTeam1(match.getTeam1())
                    .setTeam2(match.getTeam2()).setType(match.getType()).setPrice(match.getPrice()).setNrOfSeats(match.getNrOfSeats()).setDate(date).build();
            response.addMatches(matchDTO);
        }
        return response.build();
    }

    public static String getError(BasketProtobufs.BasketResponse response){
        String errorMessage=response.getError();
        return errorMessage;
    }

    public static Organiser getUser(BasketProtobufs.BasketRequest request){
        Organiser user=new Organiser();
        user.setId(request.getUser().getId());
        user.setName(request.getUser().getName());
        user.setPassword(request.getUser().getPasswd());
        return user;
    }

    public static Organiser getUser(BasketProtobufs.BasketResponse response){
        Organiser user=new Organiser();
        user.setId(response.getUser().getId());
        user.setName(response.getUser().getName());
        user.setPassword(response.getUser().getPasswd());
        return user;
    }

    public static Match[] getMatches(BasketProtobufs.BasketResponse response){
        Match[] matches=new Match[response.getMatchesCount()];
        for(int i=0;i<response.getMatchesCount();i++){
           BasketProtobufs.Match matchDTO = response.getMatches(i);

            Instant instant =  Instant.ofEpochSecond(matchDTO.getDate().getSeconds(), matchDTO.getDate().getNanos());
            Timestamp tm = Timestamp.from(instant);
            Match match = new Match(matchDTO.getId(),matchDTO.getTeam1(),matchDTO.getTeam2(),matchDTO.getType(),matchDTO.getNrOfSeats(),matchDTO.getPrice(),tm);
            System.out.println(match);
            matches[i] = match;
        }
        return matches;
    }

    public static Ticket getTicket(BasketProtobufs.BasketRequest request){
        Ticket ticket=new Ticket();
        ticket.setId(request.getTicket().getId());

        BasketProtobufs.Match matchPb = request.getTicket().getMatch();

        Instant instant =  Instant.ofEpochSecond(matchPb.getDate().getSeconds(), matchPb.getDate().getNanos());
        Timestamp tm = Timestamp.from(instant);

        Match match = new Match(matchPb.getId(),matchPb.getTeam1(),matchPb.getTeam2(),matchPb.getType(),matchPb.getNrOfSeats(),matchPb.getPrice(),tm);
        ticket.setMatch(match);
        ticket.setQuantity(request.getTicket().getQuantity());
        ticket.setCustomerName(request.getTicket().getCustomerName());
        return ticket;
    }
}
