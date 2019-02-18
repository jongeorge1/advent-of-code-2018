namespace AoC2018.Solutions.Day08
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var licenceFile = LicenceFile.Parse(input);

            return licenceFile.RootNodes[0].Value().ToString();
        }
    }
}
