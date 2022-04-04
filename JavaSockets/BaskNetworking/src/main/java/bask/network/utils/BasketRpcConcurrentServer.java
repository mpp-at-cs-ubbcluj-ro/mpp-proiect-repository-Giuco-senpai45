package bask.network.utils;

import bask.services.IServices;
import bask.network.rpcprotocol.BaskClientRpcReflectionWorker;

import java.net.Socket;


public class BasketRpcConcurrentServer extends AbsConcurrentServer {
    private IServices chatServer;
    public BasketRpcConcurrentServer(int port, IServices chatServer) {
        super(port);
        this.chatServer = chatServer;
        System.out.println("Chat- ChatRpcConcurrentServer");
    }

    @Override
    protected Thread createWorker(Socket client) {
       // ChatClientRpcWorker worker=new ChatClientRpcWorker(chatServer, client);
        BaskClientRpcReflectionWorker worker=new BaskClientRpcReflectionWorker(chatServer, client);

        Thread tw=new Thread(worker);
        return tw;
    }

    @Override
    public void stop(){
        System.out.println("Stopping services ...");
    }
}
