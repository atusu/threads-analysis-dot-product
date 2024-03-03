// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

class Program{
    static void Main(string[] args)
    {
        var array = GenerateArray(Int32.Parse(args[0]));
        var threadsNumber = Int32.Parse(args[1]);

        var watch1 = new Stopwatch();
        var watch2 = new Stopwatch();



        //No threads 
        watch1.Start();
        var sumNoThreads = new ComputeNoThreads{ WeirdArray = array }.Compute();
        watch1.Stop();
        Console.WriteLine("[NoThreads] Suma este: " + sumNoThreads + "\n" + "Duration: " + Math.Round(watch1.ElapsedMilliseconds/1000f, 2) + "s");

        //With threads - v1
        watch2.Start();
        var sumWithThreads = new ComputeWithThreads{ WeirdArray = array, ThreadsNumber = threadsNumber }.Compute();
        watch2.Stop();
        Console.WriteLine("[WithThreads] Suma este: " + sumWithThreads + "\n" + "Duration: " + Math.Round(watch2.ElapsedMilliseconds/1000f, 2) + "s");

        //n threads care calculeaza produse pe chunks din list<tuple>
        //fiecare thread scrie chunk de produse in list arr1 pe care il creezi in Compute()
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




