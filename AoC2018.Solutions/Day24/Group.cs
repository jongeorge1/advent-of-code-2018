namespace AoC2018.Solutions.Day24
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    [DebuggerDisplay("{GroupType} {Number}: {Units} units each with {UnitHitPoints} with an attack that does {DamageCount} {DamageType} damage at initiative {Initiative}")]
    public class Group
    {
        public Group(int index, string groupType, string definition)
        {
            this.Number = index + 1;
            this.GroupType = groupType;

            // First section: units & hit points
            string[] unitsAndHitPoints = definition.Substring(0, definition.IndexOf("hit points")).Split(' ');
            this.Units = int.Parse(unitsAndHitPoints[0]);
            this.UnitHitPoints = int.Parse(unitsAndHitPoints[4]);

            // Last section - attack and initiative
            int attackStart = definition.IndexOf("with an attack");
            string[] attack = definition.Substring(attackStart, definition.Length - attackStart).Split(' ');

            this.DamageCount = int.Parse(attack[5]);
            this.DamageType = attack[6];
            this.Initiative = int.Parse(attack[10]);

            int specialAttributesStart = definition.IndexOf("(");
            if (specialAttributesStart != -1)
            {
                int specialAttributesEnd = definition.IndexOf(")");
                string[] specialAttributes = definition.Substring(specialAttributesStart + 1, specialAttributesEnd - specialAttributesStart - 1).Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string specialAttribute in specialAttributes)
                {
                    string[] attributes = specialAttribute.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).Skip(2).ToArray();
                    if (specialAttribute.StartsWith("weak"))
                    {
                        this.Weaknesses = attributes.ToArray();
                    }
                    else
                    {
                        this.Immunities = attributes.ToArray();
                    }
                }
            }
        }

        public string[] Weaknesses { get; } = new string[0];

        public string[] Immunities { get; } = new string[0];

        public int Units { get; set; }

        public int UnitHitPoints { get; set; }

        public int DamageCount { get; set; }

        public string DamageType { get; }

        public int Initiative { get; }

        public int EffectivePower => this.Units * this.DamageCount;

        public int Number { get; }

        public string GroupType { get; }

        public int CalculateDamageFrom(Group attacker)
        {
            // Are we immune to the attack?
            if (this.Immunities.Contains(attacker.DamageType))
            {
                return 0;
            }

            int multiplier = this.Weaknesses.Contains(attacker.DamageType) ? 2 : 1;

            return attacker.EffectivePower * multiplier;
        }

        public void Attack(Group defender)
        {
            defender.TakeDamageFrom(this);
        }

        public void TakeDamageFrom(Group attacker)
        {
            int damage = this.CalculateDamageFrom(attacker);
            int unitsLost = damage / this.UnitHitPoints;
            this.Units = Math.Max(this.Units - unitsLost, 0);
        }
    }
}
