namespace AoC2018.Solutions.Day16
{
    using System.Linq;

    public static class SampleExtensions
    {
        public static int GetMatchingOperationsCount(this Sample sample)
        {
            return sample.GetMatchingOperations().Length;
        }

        public static Operation[] GetMatchingOperations(this Sample sample)
        {
            return Operation.All.Where(op =>
            {
                // We don't want to mess with the sample, so we need to create a clean copy of the input
                // for each test
                int[] registers = (int[])sample.PreOp.Clone();
                op.Execute(registers, sample.Instruction);

                return registers[0] == sample.PostOp[0]
                    && registers[1] == sample.PostOp[1]
                    && registers[2] == sample.PostOp[2]
                    && registers[3] == sample.PostOp[3];
            }).ToArray();
        }
    }
}
