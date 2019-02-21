namespace AoC2018.Solutions.Day15
{
    using System;

    public abstract class Unit
    {
        public MapSpace CurrentLocation { get; set; }

        public int AttackStrength { get; set; } = 3;

        public int HitPoints { get; set; } = 200;
    }
}
