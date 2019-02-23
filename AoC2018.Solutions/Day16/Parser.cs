namespace AoC2018.Solutions.Day16
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class Parser
    {
        public static (Sample[] Samples, Instruction[] instructions) Parse(string input)
        {
            // Split into samples and instructions
            int breakPoint = input.IndexOf(string.Concat(Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine));
            string samplesInput = input.Substring(0, breakPoint == -1 ? input.Length : breakPoint);
            string instructionsInput = breakPoint == -1 ? string.Empty : input.Substring(breakPoint + 4);

            // Use a regex to build up the samples
            var sampleRegex = new Regex(@"Before: \[(\d+), (\d+), (\d+), (\d+)]\r\n(\d+) (\d+) (\d+) (\d+)\r\nAfter:  \[(\d+), (\d+), (\d+), (\d+)\]");
            MatchCollection sampleMatches = sampleRegex.Matches(samplesInput);

            var samples = new List<Sample>(sampleMatches.Count);
            foreach (Match match in sampleMatches)
            {
                samples.Add(BuildSampleFromMatch(match));
            }

            // Program is easier...
            IEnumerable<Instruction> instructions = instructionsInput.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(BuildInstructionFromInputLine);

            return (samples.ToArray(), instructions.ToArray());
        }

        private static Instruction BuildInstructionFromInputLine(string input)
        {
            string[] components = input.Split(' ');

            return new Instruction
            {
                OpCode = int.Parse(components[0]),
                A = int.Parse(components[1]),
                B = int.Parse(components[2]),
                C = int.Parse(components[3]),
            };
        }

        private static Sample BuildSampleFromMatch(Match input)
        {
            int[] preOp = new[] { int.Parse(input.Groups[1].Value), int.Parse(input.Groups[2].Value), int.Parse(input.Groups[3].Value), int.Parse(input.Groups[4].Value) };
            var instruction = new Instruction
            {
                OpCode = int.Parse(input.Groups[5].Value),
                A = int.Parse(input.Groups[6].Value),
                B = int.Parse(input.Groups[7].Value),
                C = int.Parse(input.Groups[8].Value),
            };
            int[] postOp = new[] { int.Parse(input.Groups[9].Value), int.Parse(input.Groups[10].Value), int.Parse(input.Groups[11].Value), int.Parse(input.Groups[12].Value) };

            return new Sample
            {
                PreOp = preOp,
                Instruction = instruction,
                PostOp = postOp,
            };
        }
    }
}
