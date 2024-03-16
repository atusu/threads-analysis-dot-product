// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

class Program{
    static void Main(string[] args)
    {
        var array = GenerateArray(int.Parse(args[0]));
        var threadsNumber = int.Parse(args[1]);
        var threadsMethod = args[2];
        var sum = 0;
        var watch = new Stopwatch();

        watch.Start();
        switch(args[2])
        {
            case "no_threads": 
                threadsNumber = 1;
                sum = new ComputeNoThreads {WeirdArray = array}.Compute();
                break;
            case "threads_v1":
                sum = new ComputeWithThreads {WeirdArray = array, ThreadsNumber = threadsNumber}.Compute();
                break;
            case "threads_v2":
                sum = new ComputeWithThreadsV2 {WeirdArray = array, ThreadsNumber = threadsNumber}.Compute();
                break;
            default:
                Console.WriteLine("Invalid argument provided");
                sum = -1;
                return;
        }
        watch.Stop();

        var result = new ExecutionResult 
        { 
            Method = threadsMethod, 
            Duration = Math.Round(watch.ElapsedMilliseconds / 1000f, 5), 
            ArraySize = array.Count, 
            ThreadsNumber = threadsNumber, 
            TotalSum = sum 
        };
    
        SaveResultsToCSV("results_v2.csv", result);
    }

    static void SaveResultsToCSV(string filePath, ExecutionResult result)
    {
        using (var writer = new StreamWriter(filePath, true))
        {
            if (writer.BaseStream.Length == 0)
                writer.WriteLine("Method,Duration(s),ArraySize,ThreadsNumber,TotalSum");

            writer.WriteLine($"{result.Method},{result.Duration},{result.ArraySize},{result.ThreadsNumber},{result.TotalSum}");
   
        }
    }
    static void PrintList(List<(int, int)> list)
    {
        for (int i = 0; i < list.Count(); i++){
            Console.Write(list[i]);

            if(i != list.Count-1)
                Console.Write(", ");
        }

        Console.Write("\n");
    }
    static List<(int, int)> GenerateArray(int arraySize)
    {
        int Min = -100;
        int Max = 100;

        List<(int, int)> listOfTuples = new List<(int, int)>();

        Random randNum = new Random(100);

        for (int i = 0; i < arraySize; i++)
        {
            int random1 = randNum.Next(Min, Max);
            int random2 = randNum.Next(Min, Max);

            listOfTuples.Add((random1, random2));
        }

        return listOfTuples;
    }
}

class ExecutionResult
{
    public string Method {get; set;}
    public double Duration  {get; set;}
    public int ArraySize {get; set;}
    public int ThreadsNumber {get; set;}
    public int TotalSum {get; set;}
}



