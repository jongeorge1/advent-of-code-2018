namespace AoC2018.Solutions.Day19
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class Parser
    {
        public static Instruction[] Parse(string input)
        {
            IEnumerable<Instruction> instructions = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(BuildInstructionFromInputLine);

            return instructions.ToArray();
        }

        public static Instruction BuildInstructionFromInputLine(string input)
        {
            string[] components = input.Split(' ');

            return new Instruction
            {
                OpName = components[0],
                A = int.Parse(components[1]),
                B = int.Parse(components[2]),
                C = int.Parse(components[3]),
            };
        }
    }
}
