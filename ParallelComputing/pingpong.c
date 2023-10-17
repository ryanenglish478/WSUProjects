/*Ryan English
 * 011617228
 * CPTS 411 HW1*/

//#define __DEBUG__

#include <stdio.h>
#include <mpi.h>
#include <assert.h>
#include <string.h>
#include <sys/time.h>

int main(int argc, char*argv[]){
    int rank, p;
    struct timeval start, end;
    MPI_Init(&argc, &argv);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &p);

    assert(p>=2);
    int numOfBytes = 1;
    while(numOfBytes < 1048577){
        if(rank==0){ //I am sender
            int dest = 1;
            char bytes[numOfBytes];
            memset(bytes, '0', numOfBytes);

            gettimeofday(&start, NULL);

            MPI_Send(&bytes, (numOfBytes+1), MPI_CHAR, dest, 0, MPI_COMM_WORLD);

            gettimeofday(&end, NULL);
            float diff = (end.tv_usec - start.tv_usec)/1000000.0;

            printf("Time taken to send %d bytes: %f\n", numOfBytes, diff)
        }
        else
        if(rank==1){ //I am receiver
            int src = 0;
            MPI_Status status;
            char recv[numOfBytes];
            gettimeofday(&start, NULL);
            MPI_Recv(&recv, (numOfBytes+1), MPI_CHAR, MPI_ANY_SOURCE, MPI_ANY_TAG, MPI_COMM_WORLD, &status);
            gettimeofday(&end, NULL);
            
            float diff = (end.tv_usec - start.tv_usec)/1000000.0;

            printf("Receive time for %d bytes: %f\n", numOfBytes, diff);
        }
        else{}
        numOfBytes *= 2;


    }
    MPI_Finalize();
}