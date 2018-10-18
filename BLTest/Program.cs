using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandtList;

namespace BLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BrandtList<int> bList = new BrandtList<int>(20);
            BrandtList<char> bListAnother = new BrandtList<char>(10);

            Random r = new Random();

            /*
             * Test 1:
             * BrandtList of initial size 20 of 60 random integers between 0 and 9
             * heavily distributed towards lower numbers (many zeros).
             * Count and remove all '0' and '2' from list and verify they're all gone.
             */

            Console.WriteLine("-----------------TEST 1: 60 integers 0-9, mostly small values------------------\n");
            for (int i = 0; i < 60; i++)
            {
                bList.Add(r.Next(i % 10));
            }

            int[] tmp = bList.ToArray();

            for (int i = 0; i < bList.Count / 10; i++)
            {
                Console.WriteLine(String.Join(" ", tmp.Skip(i * 10).Take(10).ToArray()));
            }

            Console.WriteLine();
            Console.WriteLine("Count................. counted: " + bList.Count);
            Console.WriteLine();
            Console.WriteLine("ContainsHowMany(0)... contains: " + bList.ContainsHowMany(0));
            Console.WriteLine("RemoveAll(0).......... removed: " + bList.RemoveAll(0));
            Console.WriteLine("ContainsHowMany(0)... contains: " + bList.ContainsHowMany(0));
            Console.WriteLine();
            Console.WriteLine("ContainsHowMany(2)... contains: " + bList.ContainsHowMany(2));
            Console.WriteLine("RemoveAll(2).......... removed: " + bList.RemoveAll(2));
            Console.WriteLine("ContainsHowMany(2)... contains: " + bList.ContainsHowMany(2));
            Console.WriteLine();
            Console.WriteLine("Count................. counted: " + bList.Count);
            Console.WriteLine();
            Console.WriteLine(String.Join(" ",bList.ToArray()));

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            /*
             * Test 2:
             * BrandtList of initial size 10 of 100 random chars between 'a' and 'g' evenly distributed.
             * Count and remove all 'g', 'a', and 'e' from list and verify they're all gone.
             */

            Console.WriteLine("-------------------TEST 2: 100 chars 'a'-'g'------------------\n");
            for (int i = 0; i < 100; i++)
            {
                bListAnother.Add((char)(r.Next(7) + 'a'));
            }

            char[] tmpAnother = bListAnother.ToArray();

            for (int i = 0; i < bListAnother.Count / 10; i++)
            {
                Console.WriteLine(String.Join(" ", tmpAnother.Skip(i * 10).Take(10).ToArray()));
            }

            Console.WriteLine();
            Console.WriteLine("Count................... counted: " + bListAnother.Count);
            Console.WriteLine();
            Console.WriteLine("ContainsHowMany('g')... contains: " + bListAnother.ContainsHowMany('g'));
            Console.WriteLine("RemoveAll('g').......... removed: " + bListAnother.RemoveAll('g'));
            Console.WriteLine("ContainsHowMany('g')... contains: " + bListAnother.ContainsHowMany('g'));
            Console.WriteLine();
            Console.WriteLine("ContainsHowMany('a')... contains: " + bListAnother.ContainsHowMany('a'));
            Console.WriteLine("RemoveAll('a').......... removed: " + bListAnother.RemoveAll('a'));
            Console.WriteLine("ContainsHowMany('a')... contains: " + bListAnother.ContainsHowMany('a'));
            Console.WriteLine();
            Console.WriteLine("ContainsHowMany('e')... contains: " + bListAnother.ContainsHowMany('e'));
            Console.WriteLine("RemoveAll('e').......... removed: " + bListAnother.RemoveAll('e'));
            Console.WriteLine("ContainsHowMany('e')... contains: " + bListAnother.ContainsHowMany('e'));
            Console.WriteLine();
            Console.WriteLine("Count................... counted: " + bListAnother.Count);
            Console.WriteLine();
            Console.WriteLine(String.Join(" ", bListAnother.ToArray()));

            Console.ReadLine();
        }
    }
}
