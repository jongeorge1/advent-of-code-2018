namespace AoC2018.Solutions.Day16
{
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            (Sample[] Samples, Instruction[] Instructions) data = Parser.Parse(input);

            IEnumerable<(Sample Sample, Operation[] CandidateOperations)> testResults = data.Samples.Select(x => (x, x.GetMatchingOperations()));

            // We need to find the opcode for each operation...
            Operation[] operations = Operation.All;

            while (operations.Any(x => !x.OpCode.HasValue))
            {
                // Find any sample that has only one unallocated opcode
                IEnumerable<(Sample Sample, Operation[] CandidateOperations)> samplesWithOnlyOneUnallocatedOperation = testResults.Where(x => x.CandidateOperations.Count(op => !op.OpCode.HasValue) == 1);
                foreach ((Sample Sample, Operation[] CandidateOperations) current in samplesWithOnlyOneUnallocatedOperation)
                {
                    Operation unallocatedOperation = current.CandidateOperations.First(x => !x.OpCode.HasValue);
                    unallocatedOperation.OpCode = current.Sample.Instruction.OpCode;
                }
            }

            // Now we have allocated all the operations we can run the program
            // For ease of use we'll stick the operations in a dictionary...
            var operationsDictionary = operations.ToDictionary(x => x.OpCode, x => x);
            int[] register = new[] { 0, 0, 0, 0 };

            foreach (Instruction instruction in data.Instructions)
            {
                operationsDictionary[instruction.OpCode].Execute(register, instruction);
            }

            return register[0].ToString();
        }
    }
}
