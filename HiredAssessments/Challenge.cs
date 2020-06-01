
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

public class Challenge
{
    public static long Solution(long[] prices)
    {
        long diff = 0;

        for (int i = 0; i < prices.Length - 1; i++)
        {
            for (int j = i+1; j < prices.Length; j++)
            {
                long currentDiff = Math.Abs(prices[i] - prices[j]);
                if (currentDiff > diff)
                {
                    diff = currentDiff;
                }
            }
        }

        return diff;
    }

    public static long Solution1(long[] prices)
    {
        return prices.Max();
    }

    public static long Solution2(long[] tree)
    {
        if (tree.Length < 1)
        {
            return 0;
        }
        var i = 1;
        var height = 0;
        while (i <= tree.Length)
        {
            for (int j = i - 1; j < tree.Length; j++)
            {
                if (tree[j] > 0)
                {
                    height++;
                    break;
                }
            }
            i *= 2;
        }
        return height;
    }

    public static long[][] Solution3(long[][] matrix)
    {
        var length = matrix.Length;



        long[][] marks = new long[][] { new long[] { 1, 2, 3 }, new long[] { 4, 5, 6 }, new long[] { 7, 8, 9 }};
        return marks;
    }

    //public static bool Solution4(long[][] maze, long n)
    //{
    //    int a = 0;
    //    int b = 0;

    //    if(maze[a][b] == 1)
    //    {
    //        return false;
    //    }

    //    for (a = 0; a < n; a++)
    //    {
    //        if 
    //        for (b = 0; b < n; b++)
    //        {

    //        }
    //    }

    //    if (maze[a][b] == 0)
    //    {
    //        var down = maze[a + 1][b];
    //        var right = maze[a][b + 1];
    //        if (down + right )
    //        {

    //        }

    //    }
    //    else
    //    { 
    //        return false;
    //    }

    //    return true;
    //}

    //private static bool IsNextSquareFree()
    //{
    //    return true;
    //}

    public static long[] Solution5(long n)
    {
        var sequence = new List<long>();
        sequence.Add(1);
        sequence.Add(1);

        if (n < 3)
        { return new long[1]; }

        for (int i = 2; i < n; i++)
        {
            sequence.Add(sequence[i - 1] + sequence[i - 2]);
        }


        var primes = new List<long>();
        foreach (var item in sequence)
        {
            if(IsPrime(item))
            {
                primes.Add(item);
            }
        }

        return primes.ToArray();

    }

    public static bool IsPrime(long number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (long)Math.Floor(Math.Sqrt(number));

        for (int i = 3; i <= boundary; i += 2)
            if (number % i == 0)
                return false;

        return true;
    }

    public static long[] Solution6(long[] numbers)
    {
        if (numbers.Length<1)
        { return new long[0]; }
        long[] distNumbers = numbers.Distinct().ToArray();
        var dict = new Dictionary<long, int>();
        foreach (var item in distNumbers)
        {
            var count = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if(numbers[i] == item)
                {
                    count++;
                }
            }
            dict.Add(item, count);
        }

        var listValues = dict.Values.ToList();

        listValues.Sort();
        var lowestValue = listValues.First();
        var onlyLowest = new List<long>();

        foreach (var item in dict)
        {
            if (lowestValue == item.Value)
            {
                onlyLowest.Add(item.Key);
            }
        }

        onlyLowest.Sort();
        return onlyLowest.ToArray();

    }

    public static string Solution7(string word, string cipher)
    {
        var alphabet = "abcdefghijklmnopqrstuvwxyz";
        List<char> cipherList = new List<char>(cipher);
        List<char> alphabetList = new List<char>(alphabet);
        List<char> wordList = new List<char>(word);

        for (int i = 0; i < wordList.Count; i++)
        {
            if(!Char.IsLetter(wordList[i]))
            {
                return "";
            }
        }

        if (cipherList.Count != alphabetList.Count)
        {
            return "";
        }

        var dict = new Dictionary<char, char>();

        for (int i = 0; i < cipherList.Count; i++)
        {
            dict.Add(alphabetList[i], cipherList[i]);
        }

        
        var output = "";

        for (int j = 0; j < wordList.Count; j++)
        {
            char converted;
            dict.TryGetValue(wordList[j], out converted);
            output += converted;
        }

        
        return output;
    }
}

