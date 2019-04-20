using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChatLogParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("== Look for chat files ==");

            List<string> filePaths = Directory.GetFiles(@"B:\logs\chat").ToList();

            Console.WriteLine($"There are {filePaths.Count} chat logs to parse...");

            







            Console.ReadLine();
        }
    }
}
