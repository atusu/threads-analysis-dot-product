using System;
using System.Collections.Generic;
using System.Threading;

public class ComputeWithThreadsV2
{
    public List<(int, int)> WeirdArray {get; set;}
    public int ThreadsNumber {get; set;}

    public int Compute()
    {
        int numberOfThreads = ThreadsNumber;
        var threads = new List<Thread>(); 
        var workPerThread = WeirdArray.Count / numberOfThreads;
        
        int totalSum = 0;

        var products = new int[WeirdArray.Count].ToList();

        for(int i = 0; i < numberOfThreads; i++){
            var start = i * workPerThread;
            var end = (i == numberOfThreads - 1) ? WeirdArray.Count : (i + 1) * workPerThread;

            Thread thread = new Thread(new ParameterizedThreadStart(ComputeProducts));
            
            var param = Tuple.Create(WeirdArray, start, end, products);
            thread.Start(param);
            threads.Add(thread);
        }

        foreach (var thread in threads) {
            thread.Join();
        }

        foreach (var product in products)
        {
            totalSum += product;
        }

        return totalSum;
    }

    public void ComputeProducts(object data)
    {
        var parameters = (Tuple<List<(int, int)>, int, int, List<int>>) data;
        var arr = parameters.Item1;
        int start = parameters.Item2;
        int end = parameters.Item3;
        List<int> products = parameters.Item4;

        for (int i = start; i < end; i++)
        {
            products[i] = arr[i].Item1 * arr[i].Item2;
        }       
    }
}