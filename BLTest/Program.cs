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

            Random r = new Random();

            for (int i = 0; i < 60; i++)
            {
                bList.Add(r.Next(i % 10));
            }
            Console.WriteLine("ContainsHowMany(0)... contains: " + bList.ContainsHowMany(0));
            Console.WriteLine("Count................. counted: " + bList.Count);
            Console.WriteLine("RemoveAll(0).......... removed: " + bList.RemoveAll(0));
            Console.WriteLine("ContainsHowMany(0)... contains: " + bList.ContainsHowMany(0));
            Console.WriteLine("Count................. counted: " + bList.Count);
           
            foreach(int i in bList)
                Console.Write(i.ToString() + " ");

            Console.ReadLine();
        }
    }
}
