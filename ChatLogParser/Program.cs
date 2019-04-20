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

            // Get a list of files from the folder
            List<string> filePaths = Directory.GetFiles(@"B:\logs\chat").ToList();
            Console.WriteLine($"There are {filePaths.Count} chat logs to parse...");

            // Thread safe bag to store the messages
            ConcurrentBag<ChatMessage> messages = new ConcurrentBag<ChatMessage>();

            // read each file in parallel
            Parallel.ForEach(filePaths,
                _filePath =>
                {
                    using (StreamReader sr = new StreamReader(_filePath))
                    {
                        while (!sr.EndOfStream) // read each line in the file
                        {
                            messages.Add(new ChatMessage(sr.ReadLine())); // Process the log line using the chatmessage constructor
                        }
                    }
                    Console.WriteLine($"Done Reading {_filePath}");
                });
            
            Console.ReadLine();
        }
    }
}
