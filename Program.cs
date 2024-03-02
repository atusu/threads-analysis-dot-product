// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

class Program{
    static void Main(string[] args)
    {
        var array = GenerateArray(Int32.Parse(args[0]));
        //PrintList(array);
        var watch = new Stopwatch();
        watch.Start();
        var sum = new ComputeNoThreads{ WeirdArray = array }.Compute();
        watch.Stop();
        Console.WriteLine("Suma este: " + sum + "\n" + "Duration: " + Math.Round(watch.ElapsedMilliseconds/1000f, 2) + "s");
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




