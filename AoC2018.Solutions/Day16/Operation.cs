namespace AoC2018.Solutions.Day16
{
    using System;

    public class Operation
    {
        public Operation(string name, Action<int[], Instruction> instruction)
        {
            this.Name = name;
            this.Execute = instruction;
        }

        public static Operation[] All { get; } = new[]
        {
            new Operation("addr", (registers, instruction) => registers[instruction.C] = registers[instruction.A] + registers[instruction.B]),
            new Operation("addi", (registers, instruction) => registers[instruction.C] = registers[instruction.A] + instruction.B),
            new Operation("mulr", (registers, instruction) => registers[instruction.C] = registers[instruction.A] * registers[instruction.B]),
            new Operation("muli", (registers, instruction) => registers[instruction.C] = registers[instruction.A] * instruction.B),
            new Operation("banr", (registers, instruction) => registers[instruction.C] = registers[instruction.A] & registers[instruction.B]),
            new Operation("bani", (registers, instruction) => registers[instruction.C] = registers[instruction.A] & instruction.B),
            new Operation("borr", (registers, instruction) => registers[instruction.C] = registers[instruction.A] | registers[instruction.B]),
            new Operation("bori", (registers, instruction) => registers[instruction.C] = registers[instruction.A] | instruction.B),
            new Operation("setr", (registers, instruction) => registers[instruction.C] = registers[instruction.A]),
            new Operation("seti", (registers, instruction) => registers[instruction.C] = instruction.A),
            new Operation("gtir", (registers, instruction) => registers[instruction.C] = instruction.A > registers[instruction.B] ? 1 : 0),
            new Operation("gtri", (registers, instruction) => registers[instruction.C] = registers[instruction.A] > instruction.B ? 1 : 0),
            new Operation("gtrr", (registers, instruction) => registers[instruction.C] = registers[instruction.A] > registers[instruction.B] ? 1 : 0),
            new Operation("eqir", (registers, instruction) => registers[instruction.C] = instruction.A == registers[instruction.B] ? 1 : 0),
            new Operation("eqri", (registers, instruction) => registers[instruction.C] = registers[instruction.A] == instruction.B ? 1 : 0),
            new Operation("eqrr", (registers, instruction) => registers[instruction.C] = registers[instruction.A] == registers[instruction.B] ? 1 : 0),
        };

        public int? OpCode { get; set; }

        public string Name { get; set; }

        public Action<int[], Instruction> Execute { get; }
    }
}
