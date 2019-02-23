namespace AoC2018.Solutions.Day15
{
    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            ////Console.WriteLine(currentState.ToString());

            int attackStrength = 4;

            while (true)
            {
                ////Console.WriteLine($"Running with hit points = {attackStrength}");

                var currentState = State.Parse(input);
                Elf[] elves = currentState.GetUnits<Elf>();
                int elfCount = elves.Length;

                // Set their hit count
                foreach (Elf current in elves)
                {
                    current.AttackStrength = attackStrength;
                }

                int rounds = 0;
                bool deadElves = false;

                while (!deadElves)
                {
                    bool roundCompleted = currentState.Round();

                    if (roundCompleted)
                    {
                        rounds++;
                    }

                    deadElves = currentState.CountUnits<Elf>() < elfCount;

                    if (currentState.IsCombatEnded())
                    {
                        break;
                    }
                }

                if (deadElves)
                {
                    ////Console.WriteLine($"Elves died during round {rounds}");
                    attackStrength++;
                }
                else
                {
                    return (rounds * currentState.TotalRemainingHitPoints()).ToString();
                }
            }
        }
    }
}
