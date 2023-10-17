import java.net.*;
import java.io.*;
import java.util.Scanner;


public class CmdServer extends Thread {
    public CmdServer(){}
    public CmdServer(String[] args){}
    public void run(){
        ServerSocket sock = getSock();
        // String port = Integer.toString(sock.getLocalPort());
        try{
            SysLib.cout(String.format("%s is listening on port %d\n",
                InetAddress.getLocalHost().getHostName(),
                sock.getLocalPort()));
            
                String username = System.getProperty("user.name");

                //Server Input
                BufferedReader kb = new BufferedReader(new InputStreamReader(System.in));

                
        /*now listen for connections */
            while(true){
                Socket client = sock.accept();

                //Send to Client
                PrintStream pout = new PrintStream(client.getOutputStream());

                //Read from Client
                InputStream in = client.getInputStream();
                BufferedReader bin = new BufferedReader(new InputStreamReader(in));

                //Handshake
                String clientUser = bin.readLine();
                SysLib.cout("My Username: " + username + "\n");
                SysLib.cout("Client Username: " + clientUser + "\n");
                if(clientUser.equals(username)){
                    sock = new ServerSocket(0);
                    pout.println(sock.getLocalPort());
                    client = sock.accept();
                    pout = new PrintStream(client.getOutputStream());
                    in = client.getInputStream();
                    bin = new BufferedReader(new InputStreamReader(in));
                }
                else{
                    SysLib.cout("EXITING");
                    sock.close();
                    SysLib.exit();
                    SysLib.cout("Exited?!");
                }


                //reverse the message
                String line = bin.readLine();
                String rLine = new StringBuilder(line).reverse().toString(); 
                pout.println(rLine);
                
                String serverOut;
                //Get message from Client
                boolean cont = true;

                while(true){
                        SysLib.cout("We in the looop\n");
                        String clientIn = bin.readLine();
                        SysLib.cout("Got past the reading\n");
                        SysLib.cout("Client- " + clientIn);
                        if(clientIn.equals("bye") || clientIn.equals("Bye")){
                            SysLib.cout("Closing connection with client!\n");
                            client.close();
                            break;
                        }
                        else if(clientIn.equals("die") || clientIn.equals("Die")){
                            SysLib.cout("Killing server!\n");
                            sock.close();
                            SysLib.exit();
                        }
                        else{
                            SysLib.cout("NORM\n");
                        }
                    }

                //     serverOut = kb.readLine();
                //     while(serverOut.isEmpty() && cont){
                //         clientIn = bin.readLine();
                //         if(clientIn == "bye"){
                //             cont = false;
                //         }
                //         if(!(clientIn.isEmpty())){
                //             SysLib.cout("Client- " + clientIn + "\n");
                //         }
                //         serverOut = kb.readLine();
                //     }
                //     if(cont){
                //         pout.println(serverOut);
                //     }
                //     clientIn = bin.readLine();
                // //Send Message to Client
                //     while(clientIn.isEmpty() && cont){
                //         serverOut = kb.readLine();
                //         if(serverOut == "exit"){
                //             cont = false;
                //         }
                //         if(!(serverOut.isEmpty())){
                //             pout.println(serverOut);  
                //         }        
                //     }
                //     if(cont){
                //         SysLib.cout("Client- " + clientIn + "\n");
                //     }
            }
              
        }
        catch(IOException ioe){
            System.err.println(ioe);
        }
    }
    public ServerSocket getSock(){
        for(int i = 5000; i < 5500; i++){
            try{
                ServerSocket sock = new ServerSocket(i);
                return sock;
            }
            catch(IOException e){
            }
        }
        return null;  
    }
}

