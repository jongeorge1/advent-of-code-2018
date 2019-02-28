namespace AoC2018.Solutions.Day21
{
    using System;
    using System.Linq;
    using Instruction = AoC2018.Solutions.Day19.Instruction;
    using Operation = AoC2018.Solutions.Day19.Operation;
    using Parser = AoC2018.Solutions.Day19.Parser;

    public class Part01 : ISolution
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

            // From looking at the program, r0 is only used for one check - in line 30, and if it's equal
            // at that point to the value in r2 then the program will exit. So we need to know what the
            // value of r2 is the first time we execute line 30
            while (instructionPointer >= 0 && instructionPointer < instructions.Length)
            {
                ////Console.Write($"{instructionPointer}:{registers[0]}:{registers[1]}:{registers[2]}:{registers[3]}:{registers[4]}:{registers[5]:D4}:");

                if (instructionPointer == 30)
                {
                    return registers[2].ToString();
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
