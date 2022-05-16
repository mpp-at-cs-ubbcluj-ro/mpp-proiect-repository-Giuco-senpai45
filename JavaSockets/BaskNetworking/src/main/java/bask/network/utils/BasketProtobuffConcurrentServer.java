package bask.network.utils;

import bask.network.protobuffprotocol.ProtoBaskWorker;
import bask.services.IServices;

import java.net.Socket;

public class BasketProtobuffConcurrentServer extends AbsConcurrentServer {
    private IServices chatServer;

    public BasketProtobuffConcurrentServer(int port, IServices chatServer) {
        super(port);
        this.chatServer = chatServer;
        System.out.println("Chat- ChatProtobuffConcurrentServer");
    }

    @Override
    protected Thread createWorker(Socket client) {
        ProtoBaskWorker worker=new ProtoBaskWorker(chatServer,client);
        Thread tw=new Thread(worker);
        return tw;
    }
}
