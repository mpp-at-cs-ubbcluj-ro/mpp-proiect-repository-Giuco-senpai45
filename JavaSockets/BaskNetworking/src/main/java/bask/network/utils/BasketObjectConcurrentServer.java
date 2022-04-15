package bask.network.utils;

import bask.network.objectprotocol.BasketClientObjectWorker;
import bask.services.IServices;

import java.net.Socket;

public class BasketObjectConcurrentServer extends AbsConcurrentServer {
    private IServices baskServer;
    public BasketObjectConcurrentServer(int port, IServices chatServer) {
        super(port);
        this.baskServer = chatServer;
        System.out.println("Chat- ChatObjectConcurrentServer");
    }

    @Override
    protected Thread createWorker(Socket client) {
        BasketClientObjectWorker worker=new BasketClientObjectWorker(baskServer, client);
        Thread tw=new Thread(worker);
        return tw;
    }
}
