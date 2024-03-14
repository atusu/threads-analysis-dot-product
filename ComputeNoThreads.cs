public class ComputeNoThreads
{
    public List<(int, int)> WeirdArray {get; set;}

    public int Compute()
    {
        //a[0][0] * a[0][1] + a[1][0] * a[1][1] + ... + a[n][0] * a[n][1]
        var sum = 0;
        for(int i = 0; i < WeirdArray.Count; i ++){       
            sum += WeirdArray[i].Item1 * WeirdArray[i].Item2;          
        }

        return sum;
    }
}