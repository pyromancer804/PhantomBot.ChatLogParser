using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChatLogParser.model;

namespace ChatLogParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("== Look for chat files ==");

            List<string> filePaths = Directory.GetFiles(@"C:\temp\log temp").ToList();

            Console.WriteLine($"There are {filePaths.Count} chat logs to parse...");

            
            ConcurrentBag<ChatMessage> messages = new ConcurrentBag<ChatMessage>();

            Parallel.ForEach(filePaths,
                _filePath =>
                {
                    using (StreamReader sr = new StreamReader(_filePath))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            Console.WriteLine(line);
                        }
                    }
                    Console.WriteLine($"Done Reading {_filePath}");
                });
            
            Console.ReadLine();
        }
    }
}
