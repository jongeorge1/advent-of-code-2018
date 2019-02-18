namespace AoC2018.Runner
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using AoC2018.Solutions;

    public static class Program
    {
        private static void Main(string[] args)
        {
            int day = int.Parse(args[0]);
            int part = int.Parse(args[1]);

            ISolution instance = SolutionFactory.GetSolution(day, part);

            // Load the data
            var locationUri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            string location = Uri.UnescapeDataString(locationUri.Path);
            string locationDirectory = Path.GetDirectoryName(location);
            string inputFileName = Path.Combine(locationDirectory, "Input", $"day{day:D2}.txt");
            string data = File.ReadAllText(inputFileName);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            string result = instance.Solve(data);

            stopwatch.Stop();

            Console.WriteLine(result);
            Console.WriteLine($"Result obtained in {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
