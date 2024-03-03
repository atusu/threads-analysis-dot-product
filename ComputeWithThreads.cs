using System;
using System.Collections.Generic;
using System.Threading;

public class ComputeWithThreads
{
    public List<(int, int)> WeirdArray {get; set;}
    public int ThreadsNumber {get; set;}

    public int Compute()
    {
        int numberOfThreads = ThreadsNumber;
        var threads = new List<Thread>(); 
        var workPerThread = WeirdArray.Count / numberOfThreads;
        
        int totalSum = 0;

        var partialSums = new List<int>();

        for(int i = 0; i < numberOfThreads; i++){
            var start = i * workPerThread;
            var end = (i == numberOfThreads - 1) ? WeirdArray.Count : (i + 1) * workPerThread;

            Thread thread = new Thread(new ParameterizedThreadStart(ComputePartialSums));
            threads.Add(thread);

            var param = Tuple.Create(WeirdArray, start, end, partialSums);
            thread.Start(param);
            thread.Join();
        }

        foreach (var partialSum in partialSums)
        {
            totalSum += partialSum;
        }
        return totalSum;
    }

    public void ComputePartialSums(object data)
    {
        var parameters = (Tuple<List<(int, int)>, int, int, List<int>>) data;
        var arr = parameters.Item1;
        int start = parameters.Item2;
        int end = parameters.Item3;
        List<int> partialSums = parameters.Item4;

        int partialSum = 0;

        for (int i = start; i < end; i++)
        {
            partialSum += arr[i].Item1 * arr[i].Item2;
        }

        partialSums.Add(partialSum);        
    }
}