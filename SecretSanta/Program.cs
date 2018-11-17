using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta
{
    class Program
    {
        static void Main(string[] names)
        {
            if (names.Length < 3)
            {
                Console.WriteLine("Need to provide at least 3 names as arguments for secret santa");
            }
            List<string> buyFor = new List<string>(names);
            buyFor.OrderBy(a => Guid.NewGuid()).ToList(); // randomize
            Random rnd = new Random();
            for (int i =0; i < names.Length; i++)
            {
                //make sure no one is buying for themself
                if (buyFor[i] == names[i])
                {
                    int r;
                    do
                    {
                        r = rnd.Next(0, names.Length);
                    } while (r == i);
                    //swap with someone else, since they were initially buying for themself, the person swapped with is guaranteed not to be after the swap
                    string tmp = buyFor[i];
                    buyFor[i] = buyFor[r];
                    buyFor[r] = tmp;
                }
            }

            //write results
            for (int i =0; i < names.Length; i++)
            {
                File.WriteAllText($"{names[i]}.txt", $"Buy a present for {buyFor[i]}");
            }
        }
    }
}
