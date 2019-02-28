namespace AoC2018.Solutions.Day21
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Instruction = AoC2018.Solutions.Day19.Instruction;
    using Operation = AoC2018.Solutions.Day19.Operation;
    using Parser = AoC2018.Solutions.Day19.Parser;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            string[] rows = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int instructionPointerRegister = int.Parse(rows[0].Split(' ')[1]);
            int instructionPointer = 0;

            Instruction[] instructions = rows
                .Skip(1)
                .Select(Parser.BuildInstructionFromInputLine)
                .ToArray();

            var operations = Operation.All.ToDictionary(x => x.Name, x => x.Execute);

            int[] registers = new[] { 0, 0, 0, 0, 0, 0 };
            var seenR2Values = new List<int>(1000000);

            // We can assume from the question that r2 is going to change, and at some point we're going to
            // hit a loop. Therefore we need to watch the comparisons, store the values from r2, and the
            // first time we see a number crop up twice, we know the answer is the previous value we stored.
            while (instructionPointer >= 0 && instructionPointer < instructions.Length)
            {
                ////Console.Write($"{instructionPointer}:{registers[0]}:{registers[1]}:{registers[2]}:{registers[3]}:{registers[4]}:{registers[5]:D4}:");

                if (instructionPointer == 30)
                {
                    if (seenR2Values.Contains(registers[2]))
                    {
                        return seenR2Values.Last().ToString();
                    }

                    seenR2Values.Add(registers[2]);
                }

                registers[instructionPointerRegister] = instructionPointer;

                Instruction targetInstruction = instructions[instructionPointer];
                ////Console.Write($"{targetInstruction.OpName}:{targetInstruction.A}:{targetInstruction.B}:{targetInstruction.C}:");

                operations[targetInstruction.OpName](registers, targetInstruction);

                instructionPointer = registers[instructionPointerRegister];
                instructionPointer++;
                ////Console.WriteLine($"{registers[0]:D4}:{registers[1]:D4}:{registers[2]:D4}:{registers[3]:D4}:{registers[4]:D4}:{registers[5]:D4}");
            }

            return string.Empty;
        }
    }
}
