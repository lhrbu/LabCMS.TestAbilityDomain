using LabCMS.TestAbilityDomain.Shared.Models;
using System;
using System.IO;
using System.Text.Json;

namespace LabCMS.TestAbilityDomain.DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using Stream stream = File.OpenRead("seed.json");

            var ab = JsonSerializer.DeserializeAsync<TestAbility[]>(stream).Result;
            Console.WriteLine("Hello World!");
        }
    }
}
