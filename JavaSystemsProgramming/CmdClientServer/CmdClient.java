import java.net.*;
import java.io.*;
import java.util.Scanner;

public class CmdClient extends Thread{
    int portNum;
    String hostName = "127.0.0.1";
    public CmdClient(){}
    public CmdClient(String args[]){
        portNum = Integer.parseInt(args[0]);
        if(portNum < 5000 || portNum > 5500){
            throw new IllegalArgumentException("Port Number out of range");
        }
        if(args.length == 2){
            hostName = args[1];
        }
    }
    public void run(){
        try{
            
            
            /*make connection to server socket */
            String newPort;
            Socket sock = new Socket(hostName, portNum);

            //Read from Server
            InputStream in = sock.getInputStream();
            BufferedReader bin = new BufferedReader(new InputStreamReader(in));
            
            //Send to Server
            DataOutputStream dos = new DataOutputStream(sock.getOutputStream());

            //Handshake
            String username = System.getProperty("user.name");
            dos.writeBytes(username + "\n");   
            SysLib.cout("Sending UserName"); 
            if((newPort = bin.readLine()) != null){
                SysLib.cout("New port num: " + newPort + "\n");
                sock = new Socket(hostName, Integer.parseInt(newPort));
                in = sock.getInputStream();
                bin = new BufferedReader(new InputStreamReader(in));
                dos = new DataOutputStream(sock.getOutputStream());
            }
            else{
                sock.close();
                SysLib.exit();
            }

            //Prompt user for message
            SysLib.cout("Enter a message: ");
            BufferedReader kb = new BufferedReader(new InputStreamReader(System.in));
            String message = kb.readLine();

            //Send to Server
            
            dos.writeBytes(message + "\n");
            SysLib.cout("Sending Message!\n"); 
            String response = bin.readLine();
            SysLib.cout("Reversed Message: " + response + "\n");

            
           

            Boolean cont = true;
            /*read the data from the socket */
            String serverResponse, cin; 
            SysLib.cout("-");
            while(cont){
                SysLib.cout("-");
                if((cin = kb.readLine()) != null){
                    SysLib.cout("Sending message: " + cin + "\n");
                    dos.writeBytes(cin);
                }
                SysLib.cout("We are in the while loop \n");
                // if((serverResponse = bin.readLine()) != null){
                //     SysLib.cout("Server- " + serverResponse + "\n");
                // }
                
            }
            /*close the socket connection*/
            sock.close();
            SysLib.exit();
        }
        catch(IOException ioe){
            System.err.println(ioe);
        }
    }
}