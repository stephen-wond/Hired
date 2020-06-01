using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackAJobAssessments
{
    public class Solutions
    {

        static public int Run(string teamKey)
        {
            Task<string> result = GetResponseString();
            var jsonResult = result.Result;

            dynamic jsonObject = JsonConvert.DeserializeObject(jsonResult);

            int goalCount = 0;

            foreach (var round in jsonObject.rounds)
            {
                foreach (var match in round.matches)
                {
                    if (match.team1.key == teamKey)
                    {
                        goalCount += (int)match.score1;
                    }
                    if (match.team2.key == teamKey)
                    {
                        goalCount += (int)match.score2;
                    }
                }
            }
            return goalCount;
        }


        static public async Task<string> GetResponseString()
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://raw.githubusercontent.com/openfootball/football.json/master/2014-15/en.1.json");
            var contents = await response.Content.ReadAsStringAsync();

            return contents;
        }

        public static String run1(String p)
		{
            var vowelCount = SubstringCount(p, "AEIOUaeiou");
            var consonantsCount= SubstringCount(p, "BCDFGHJKLMNPQRSTVWxYzbcdfghjklmnpqrstvwxyz");

            var reversed = ReverseWords(p);
            var dashSpace = SpaceReplace(p);
            var prefixVowel = SubstringReplace(p,"AEIOUaeiou");


			String combined_queries = $"{vowelCount} {consonantsCount}::{reversed}::{dashSpace}::{prefixVowel}";
			return combined_queries;
		}

        public static int SubstringCount(string p, string subP)
        {
            var count = 0;
            foreach(char c in p)
            {
                if(subP.Contains(c))
                {
                    count++;
                }
            }
            return count;
        }

        public static string SubstringReplace(string p, string subP)
        {
            foreach (char c in subP)
            {
                var character = c.ToString();
                p = p.Replace(character, ("pv" + c));
            }
            return p;
        }

        public static string SpaceReplace(string p)
        {
            return p.Replace(" ", "-");
        }

        public static string ReverseWords(string p)
        {
            var outputString = "";

            var caseCharArray = p.ToCharArray();
            var reverseCaseArray = new char[caseCharArray.Length];
            
            for (int i = 0; i < caseCharArray.Length; i++)
            {
                if (!char.IsWhiteSpace(caseCharArray[i]))
                {
                    if (char.IsUpper(caseCharArray[i]) && char.IsLetter(caseCharArray[i]))
                    {
                        reverseCaseArray[i] = char.ToLower(caseCharArray[i]);
                    }
                    if (char.IsLower(caseCharArray[i]) && char.IsLetter(caseCharArray[i]))
                    {
                        reverseCaseArray[i] = char.ToUpper(caseCharArray[i]);
                    }
                }
                
            }

            p = new string(reverseCaseArray);


            var wordArray = p.Split(' ');
            var reverseWordArray = new string[wordArray.Length];

            for (int i = 0, j = wordArray.Length - 1; i < wordArray.Length; i++, j--)
            {
                reverseWordArray[i] = wordArray[j];
            }

            outputString += string.Join(" ", reverseWordArray);

            

            foreach (var word in wordArray)
            {
                var charArray = word.ToCharArray();
                var reverseCharArray = new char[charArray.Length];

                for (int i = 0, j = charArray.Length - 1; i < charArray.Length; i++, j--)
                {
                    if(char.IsUpper(charArray[j]))
                    {
                        reverseCharArray[i] = char.ToLower(charArray[j]);
                    }
                    else if(char.IsLower(charArray[j]))
                    {
                        reverseCharArray[i] = char.ToUpper(charArray[j]);
                    }
                    else 
                    {
                        reverseCharArray[i] = charArray[j];
                    }
                }
                outputString += new string(reverseCharArray) + " ";
            }

            return outputString.Remove(outputString.Length - 1); ;
        }


        static public int Run(int[] a)
        {
            var highest = a.Max();
            for (int k = 0; k < a.Length; k++)
            {
                for (int j = 0; j < a.Length; j++)
                {
                    var sum = 0;
                    for (int i = k; i < j+1; i++)
                    {
                        sum += a[i];
                        Console.WriteLine(a[i]);

                    }
                    if (sum > highest)
                    {
                        highest = sum;
                    }
                    Console.WriteLine("=" + sum);
                    Console.WriteLine("\n");
                    Console.WriteLine("\n");
                }
            }
            
            if(highest == 0)
            {
                return a.Max();

            }
            else
            {
                return highest;
            }
        }

        static public bool Run(int number)
        {
            for (int i = 3; i < number+1; i++)
            {
                if(IsPrime(i))
                {
                    float divisor = (float)number / (float)i;
                    if (divisor % 1 == 0)
                    {
                        if (IsPrime((int)divisor))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        [Test]
        public void RunTest()
        {
            Assert.IsTrue(Solutions.IsPrime(0));
            Assert.IsTrue(Solutions.IsPrime(1));
            Assert.IsTrue(Solutions.IsPrime(2));
            Assert.IsTrue(Solutions.IsPrime(3));
            Assert.IsTrue(Solutions.IsPrime(4));
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
    }
}
//For an array with n elements, a = (a1, a2, a3, … , an), 
//find the maximum possible sum of a contigous subarray.
//If the maximum element is bigger than the sum, return that element.

//CONSTRAINTS 1 <= n <= 10^6 -10^3 <= ai <= 10^3

//EXAMPLE Input [-2,1,-3,4,-1,2,1,-5,4]

//Output 6