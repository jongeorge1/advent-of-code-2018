namespace AoC2018.Solutions.Day19
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    [DebuggerDisplay("{OpName} {A} {B} {C}")]
    public class Instruction
    {
        public string OpName { get; set; }

        public int A { get; set; }

        public int B { get; set; }

        public int C { get; set; }
    }
}
