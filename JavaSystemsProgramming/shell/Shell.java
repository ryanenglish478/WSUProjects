//Made by Ryan English
import java.util.Arrays;

public class Shell extends Thread{
    public Shell(){}
    public Shell(String args[]){}
    public void run(){
        Integer execCount = new Integer(1);
        Boolean repeat = new Boolean(true);
        Integer id[] = new Integer[]{0,0,0,0,0,0};
        Integer EMPTY_ID[] = new Integer[]{0,0,0,0,0,0};
        Integer id_count = new Integer(0);
        while(repeat){

            //prints the amount of times the shell has been executed
            SysLib.cout("shell[" + execCount + "]%");               
            StringBuffer input = new StringBuffer();
           
            //recieves input from user
            SysLib.cin(input);                      
            String line = new String(input);  
            
            //Check if exit, if so exit
            if(line.compareTo("exit") == 0){            
                SysLib.exit();
                repeat = false;
                break;
            }      
            //Check if line isnt empty
            if(!line.isEmpty()){ 
                             
                //Split the commands by ;
                for(String cmd : line.split(";")){  
                    
                    //Split the commands by & 
                    for(String command : cmd.split("&")){ 
                        
                        //Execute commands
                        String[] arguments = SysLib.stringToArgs(command.toString());    
                        
                        //Put ids of the children into array
                        id[id_count] = SysLib.exec(arguments);
                        id_count++;               
                    }
                    //Wait for child process to die in order to execute next
                    findChild(id);

                    //reset id array
                    System.arraycopy(EMPTY_ID, 0, id, 0, id.length);
                    id_count = 0;                          
                }
                 execCount++;
            }   
        }  
    }
    public static void findChild(Integer[] ids){
        Arrays.sort(ids);
        Integer[] checkIds = new Integer[]{0,0,0,0,0,0};
        Integer index = new Integer(0);
      
        //check to see if the checkIds and ids arrays are equal
        while(!(Arrays.equals(ids, checkIds))){
            Integer id = SysLib.join();
           
            //go through ids array checking if SysLib.join id is in it
            for(int x: ids){
                
                //if so, add value to checkIds
                if(x == id){
                    checkIds[index] = x;
                    index++;
                }                
            }
            Arrays.sort(checkIds);
        }
        return;
    }
}