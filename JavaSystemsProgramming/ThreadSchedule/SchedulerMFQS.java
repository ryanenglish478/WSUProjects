import java.util.*;
//Made by Ryan English
public class Scheduler extends Thread
{
    private Vector queue0;
	private Vector queue1;
	private Vector queue2;
    private int timeSlice;
    private static final int QUEUE0_TIME_SLICE = 500;


    // New data added to p161 
    private boolean[] tids; // Indicate which  ids have been used
    private static final int DEFAULT_MAX_THREADS = 10000;

    // A new feature added to p161 
    // Allocate an ID array, each element indicating if that id has been used
    private int nextId = 0;
    private void initTid( int maxThreads ) {
	tids = new boolean[maxThreads];
	for ( int i = 0; i < maxThreads; i++ )
	    tids[i] = false;
    }

    // A new feature added to p161 
    // Search an available thread ID and provide a new thread with this ID
    private int getNewTid( ) {
	for ( int i = 0; i < tids.length; i++ ) {
	    int tentative = ( nextId + i ) % tids.length;
	    if ( tids[tentative] == false ) {
		tids[tentative] = true;
		nextId = ( tentative + 1 ) % tids.length;
		return tentative;
	    }
	}
	return -1;
    }

    // A new feature added to p161 
    // Return the thread ID and set the corresponding tids element to be unused
    private boolean returnTid( int tid ) {
	if ( tid >= 0 && tid < tids.length && tids[tid] == true ) {
	    tids[tid] = false;
	    return true;
	}
	return false;
    }

    // A new feature added to p161 
    // Retrieve the current thread's TCB from the queue
    public TCB getMyTcb( ) {
	Thread myThread = Thread.currentThread( ); // Get my thread object
	synchronized( queue0 ) {
	    for ( int i = 0; i < queue0.size( ); i++ ) {	//Check queue0 for TCB
		TCB tcb = ( TCB )queue0.elementAt( i );				
		Thread thread = tcb.getThread( );
		if ( thread == myThread ) // if this is my TCB, return it
		    return tcb;
	    
		}
		for ( int i = 0; i < queue1.size( ); i++ ) {	//Check queue1 for TCB
			TCB tcb = ( TCB )queue1.elementAt( i );
			Thread thread = tcb.getThread( );
			if ( thread == myThread ) // if this is my TCB, return it
				return tcb;
		}
		for ( int i = 0; i < queue2.size( ); i++ ) {	//Check queue2 for TCB
			TCB tcb = ( TCB )queue2.elementAt( i );
			Thread thread = tcb.getThread( );
			if ( thread == myThread ) // if this is my TCB, return it
				return tcb;
			
		}
	
	}
	return null;
    }

    // A new feature added to p161 
    // Return the maximal number of threads to be spawned in the system
    public int getMaxThreads( ) {
	return tids.length;
    }

    public Scheduler( ) {			//Initialize timeSlice = 500ms
	timeSlice = QUEUE0_TIME_SLICE;
	queue0 = new Vector( );		
	queue1 = new Vector();			//Initialize all queues
	queue2 = new Vector();
	initTid( DEFAULT_MAX_THREADS );
    }

    public Scheduler( int quantum ) {
	timeSlice = quantum;
	queue0 = new Vector( );
	queue1 = new Vector();
	queue2 = new Vector();
	initTid( DEFAULT_MAX_THREADS );
    }

    // A new feature added to p161 
    // A constructor to receive the max number of threads to be spawned
    public Scheduler( int quantum, int maxThreads ) {
	timeSlice = quantum;
	queue0 = new Vector();
	queue1 = new Vector();
	queue2 = new Vector();
	initTid( maxThreads );
    }

    private void schedulerSleep( ) {
	try {
	    Thread.sleep( timeSlice );
	} catch ( InterruptedException e ) {
	}
    }

    // A modified addThread of p161 example
    public TCB addThread( Thread t ) {

	TCB parentTcb = getMyTcb( ); // get my TCB and find my TID
	int pid = ( parentTcb != null ) ? parentTcb.getTid( ) : -1;
	int tid = getNewTid( ); // get a new TID
	if ( tid == -1)
	    return null;
	TCB tcb = new TCB( t, tid, pid ); // create a new TCB
	queue0.add( tcb );			//Adds new TCB to queue0
	return tcb;
    }

    // A new feature added to p161
    // Removing the TCB of a terminating thread
    public boolean deleteThread( ) {
	TCB tcb = getMyTcb( ); 
	if ( tcb!= null )
	    return tcb.setTerminated( );
	else
	    return false;
    }

    public void sleepThread( int milliseconds ) {
	try {
	    sleep( milliseconds );
	} catch ( InterruptedException e ) { }
    }
    
    // A modified run of p161
    public void run( ) {
	Thread current = null;
	int repeated = 0;	//This value will check how much time has passed for queue1 and 2
	
	
	while ( true ) {
		
	    try {
		// get the next TCB and its thrad
		if ( queue0.size() != 0 ){				//Check queue0 first
			TCB currentTCB = (TCB)queue0.firstElement( );	//Grab first TCB
			if ( currentTCB.getTerminated( ) == true ) {	//Check if TCB is terminated
		    	queue0.remove( currentTCB );
		    	returnTid( currentTCB.getTid( ) );
		    	continue;
			}
			current = currentTCB.getThread( );
			if ( current != null ) {
		 		if ( current.isAlive( ) ){
					//  current.suspend();
					 queue0.remove(currentTCB);			//If this thread is alive, this means that the
					 queue1.add(currentTCB);			//Thread has outlasted queue0's quantum. Therefore,
				 }										//Move to queue1
		    	else {
			// Spawn must be controlled by Scheduler
			// Scheduler must start a new thread
				current.start( ); 						//If not alive, thread has never started. Therefore start
				}
			}
		}
		else if(queue1.size() != 0){					//Check queue1 next
			TCB currentTCB = getMyTcb();				//Grab active TCB
			if (currentTCB == null) currentTCB = (TCB)queue1.firstElement();	//if no active TCB, grab first TCB in queue
			if(currentTCB.getTerminated() == true){
				queue1.remove(currentTCB);
				returnTid(currentTCB.getTid());
				continue;
			}
			current = currentTCB.getThread();
			if(current!= null){
				if(current.isAlive() && repeated != 2){		//Check to see if Alive + under queue1 quantum (500*2)
					current.resume();
					repeated++;								//Resume the thread, increment repeated to repersent
				}											//500ms passing
				else if(current.isAlive() && repeated == 2){
					repeated = 0;							//If 1000ms (500*2) has passed, thread outlasted 
					current.suspend();						//queue1's quantum. Therefore, suspend, move to 
					queue1.remove(currentTCB);				//queue2
					queue2.add(currentTCB);
				}
				else{
					current.start();
				}
				
			}
		}
		else if(queue2.size() != 0){
			TCB currentTCB = getMyTcb();					//Same as queue1
			if (currentTCB == null) currentTCB = (TCB)queue2.firstElement();
			if(currentTCB.getTerminated() == true){
				queue2.remove(currentTCB);
				returnTid(currentTCB.getTid());
				continue;
			}
			
			current = currentTCB.getThread();
			if(current!= null){
				if(current.isAlive() && repeated != 4){		//Check to see if thread has outlasted 2000ms (500*4)
					current.resume();
					repeated++;
				}
				else if(current.isAlive() && repeated == 4){	//If thread outlasted queue2's quantum, 
					repeated = 0;								//Put into back of queue
					queue2.remove(currentTCB);
					queue2.add(currentTCB);
				}
				else{
					current.start();
				}

			}
		}
		
		schedulerSleep( );
		// System.out.println("* * * Context Switch * * * ");

		synchronized ( queue0 ) {
		    if ( current != null && current.isAlive( ) )
			current.suspend();
		}
	    } catch ( NullPointerException e3 ) { };
	}
    }
}
