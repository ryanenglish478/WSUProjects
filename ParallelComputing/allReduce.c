/*Ryan English
 * 011617228
 * CPTS 411 HW2*/

#include <stdio.h>
#include <stdlib.h>
#include <mpi.h>
#include <time.h>
#include <math.h>
#include <sys/time.h>

void Naive(int input[], int rank, int arraySize, int p, int localSum);
void Hypercubic(int input[], int rank, int arraySize, int p, int localSum);
int Sum(int input[], int arraySize);
int RightPerm(int sum, int rank, int p);

int main(int argc, char*argv[]){
        int rank, p;
        struct timeval start, end;
        MPI_Init(&argc, &argv);
        MPI_Comm_rank(MPI_COMM_WORLD, &rank);
        MPI_Comm_size(MPI_COMM_WORLD, &p);


        int n = 1;
        while(n < 1048577){
                int input[n];
                int singleInput[n+1];
                for(int i = 0; i < p; i++){
                        if(rank==i){
                                srand(time(NULL) * (rank + 1));
                                for(int k = 0; k < n; k++){
                                        singleInput[k] = rand() % 100 + 1;
                                }
                                //Insert random nums into input array
                                for(int j = 0; j < (n/p); j++){
                                        input[j] = rand() % 100 + 1;
                                }
                        }
                }
                int localSum = Sum(input, (n/p));

                gettimeofday(&start, NULL);
                int singlePSum = Sum(singleInput, n+1);

                gettimeofday(&end, NULL);

                Naive(input, rank, (n/p), p, localSum);

                Hypercubic(input, rank, (n/p), p, localSum);
                //printf("My rank: %d, Sum: %d\n", rank, singlePSum);

                int totalSum;
                MPI_Allreduce(&localSum, &totalSum, 1, MPI_INT, MPI_SUM, MPI_COMM_WORLD);

                float diff = (end.tv_usec - start.tv_usec)/1000000.0;
                if(rank == 0){
                        printf("Time for local sum with %d elements: %f\n", n, diff);
                }
                //printf("Using MPI_Allreduce: My rank: %d, sum: %d\n", rank, totalSum);
                n *= 2;
        }
        MPI_Finalize();
}

void Hypercubic(int input[], int rank, int arraySize, int p, int localSum){
        //Note for this to work you need to add -lm flag to mpicc
        //i.e. mpicc -o allReduce allReduce.c -lm
        int timestep = (int)(log2(p));
        MPI_Status status;

        //Sum of local process's input array
        int recvSum;
        //Hypercubic processes communicate to the (rank + 2^t) process, with t being the timestep.
        //If you want me to explain how I came to this algorithm send me an email, I can show you the math I did
        //In the process of discovering this
        for(int i = 0; i < timestep; i++){
                int split = (int)(pow(2, (i+1)));
                while(rank >= split){
                        split += (int)(pow(2,(i+1)));
                }
                if((rank + (int)(pow(2,i))) < split){
                        int destRank = rank + (int)(pow(2,i));
                        MPI_Sendrecv(&localSum, 1, MPI_INT, destRank, 1, &recvSum, 1, MPI_INT, destRank, 1, MPI_COMM_WORLD, &status);
                }
                else{
                        int destRank = rank - (int)(pow(2,i));
                        MPI_Sendrecv(&localSum, 1, MPI_INT, destRank, 1, &recvSum, 1, MPI_INT, destRank, 1, MPI_COMM_WORLD, &status);
                }
                localSum += recvSum;
        }
        //printf("Hypercubic Rank: %d, Sum: %d\f", rank, localSum);
}

void Naive(int input[], int rank, int arraySize, int p, int localSum){
        int recvSum;
        MPI_Status status;
        //Thrn do right shift permutation to reduce sum to rank (p-1)
        int totalSum = RightPerm(localSum, rank, p);
        //Left Perm to pass sum from rank (p-1) to all ranks
        if(rank != (p-1)){
                MPI_Recv(&recvSum, 1, MPI_INT, (rank+1), 0, MPI_COMM_WORLD, &status);
                totalSum = recvSum;
        }
        if(rank != 0){
                MPI_Send(&totalSum, 1, MPI_INT, (rank-1), 0, MPI_COMM_WORLD);
        }
        //printf("Naive My rank: %d, my sum: %d\n", rank, totalSum);
}

int RightPerm(int sum, int rank, int p){
        int recvSum;
        MPI_Status status;
        if(rank != 0){
                MPI_Recv(&recvSum, 1, MPI_INT, (rank-1), 0, MPI_COMM_WORLD, &status);
                sum += recvSum;
        }
        if(rank != (p-1)){
                MPI_Send(&sum, 1, MPI_INT, (rank+1), 0, MPI_COMM_WORLD);
        }
        return sum;
}

int Sum(int input[], int arraySize){
        int sum = 0;
        for(int i = 0; i < arraySize; i++){
                sum += input[i];
        }
        return sum;
}