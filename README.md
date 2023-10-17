# ClassProjects
The following are the coding projects I worked on in college and their descriptions

AzureConfidentialLedger - The capstone project where we worked with Microsoft to develop a tool to visualize Microsoft's Azure Confidential Ledger.
    The details of the project are outlined in the Microsoft_Azure_Prototype_Report found in the folder.
    The LedgerExplorer blade found currently in the Azure portal was created by our capstone team, and allows the user to see the transactions made on their ledger
    In addition, a blade to manage the users for the ledger such as being able to add, remove, or check user's permissions based off their Microsoft Entra ID or a PEM Certificate file was created.
    As of the time of writing this this feature has not been added to the Azure Portal, but can be found in AzureConfidentialLedger/ACLPortal/src/Default/Extension/Client/ReactViews/ManageUserBlade

JavaSystemsProgramming - A folder containing the coding assignments worked on in my Systems Programming class written in Java
    CmdClientServer - A simple client/server program that communicates over sockets
    shell - A custom shell that executes programs either sequentially or concurrently
    ThreadSchedule - Schedules threads by either Multilevel Feedback Queue scheduling or Round Robin scheduling

ParallelComputing
    allReduce.c - Implements MPI_AllReduce in two different ways, one by a naive approach that just adds up the integers by sending each sum to the next process and another Hypercubic approach that uses an algorithm to make sure each process gets the updated sum at the same time
    pingpong.c - A simple program that sends an array of bytes between two processes

PythonSocketServerClient
    Creates a P2P connection to be able to send messages or files using Python's Socket library

SecureMessenger
    A messenger app that allows the user to create and login with a profile, create contacts with other users and send or delete messages between users
    Interacts with a database via dbClient

Spreadsheet_Ryan_English
    A spreadsheet applicaion that supports math operations between cells. Also allows for the ability to edit cell colors and ctrl-z and ctrl-y changes made to cells.

YelpReviewsDB
    Compiles yelp review data and allows the user to be able to search based off various fields found in the reviews, such as star ratings, locations, and busuiness types