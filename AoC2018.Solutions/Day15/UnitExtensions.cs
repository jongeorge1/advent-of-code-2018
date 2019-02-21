namespace AoC2018.Solutions.Day15
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class UnitExtensions
    {
        public static void TakeTurn(this Unit unit, State state)
        {
            if (unit.IsKilled())
            {
                throw new InvalidOperationException("A killed unit cannot take a turn");
            }

            unit.Move(state);
            unit.Attack(state);
        }

        public static Unit InRangeEnemy(this Unit unit, State currentState)
        {
            Type targetFoeType = unit.EnemyType();

            return currentState.GetAdjacentSpaces(unit.CurrentLocation).Where(x => x.Unit?.GetType() == targetFoeType).Select(x => x.Unit).OrderBy(x => x.HitPoints).ThenBy(x => x.CurrentLocation.Location).FirstOrDefault();
        }

        public static void Move(this Unit unit, State state)
        {
            // Are we already in range?
            if (unit.InRangeEnemy(state) != null)
            {
                // We're going nowhere.
                return;
            }

            MapSpace[] inRangeSpaces = unit.FindSpacesInRangeOfAnEnemy(state);

            // Find the closest in range space
            MapSpace[] pathToClosestSpace = unit.GetPathToClosest(inRangeSpaces, state);

            // If we found a path, take a step towards it (otherwise stay here - this unit
            // is blocked from moving for some reason)
            if (pathToClosestSpace != null)
            {
                MapSpace targetSpace = pathToClosestSpace[1];

                unit.MoveTo(targetSpace);
            }
        }

        public static void MoveTo(this Unit unit, MapSpace targetSpace)
        {
            if (targetSpace.Unit != null)
            {
                throw new InvalidOperationException("Target space is already occupied");
            }

            unit.CurrentLocation.Unit = null;
            unit.CurrentLocation = targetSpace;
            unit.CurrentLocation.Unit = unit;
        }

        public static Type EnemyType(this Unit unit)
        {
            // Who's the enemy?
            return unit is Elf ? typeof(Goblin) : typeof(Elf);
        }

        public static MapSpace[] FindSpacesInRangeOfAnEnemy(this Unit unit, State currentState)
        {
            // Who's the enemy?
            Type targetFoeType = unit.EnemyType();

            // Where's the enemy?
            IEnumerable<MapSpace> enemyLocations = currentState.Map.Where(x => x?.Unit != null && x.Unit.GetType() == targetFoeType);

            // Find in-range spaces
            return enemyLocations.SelectMany(currentState.GetEmptyAdjacentSpaces).OrderBy(x => x.Location).Distinct().ToArray();
        }

        public static MapSpace[] GetPathToClosest(this Unit unit, MapSpace[] targetSpaces, State currentState)
        {
            // We're doing a basic breadth first search where we'll track all the possible moves
            // until we hit a target space.
            var pathFindingQueue = new Queue<List<MapSpace>>();
            pathFindingQueue.Enqueue(new List<MapSpace> { unit.CurrentLocation });
            var visitedSpaces = new List<MapSpace>(currentState.YOffset * currentState.YOffset);

            while (pathFindingQueue.Count > 0)
            {
                List<MapSpace> currentPath = pathFindingQueue.Dequeue();
                MapSpace currentSpace = currentPath[currentPath.Count - 1];

                if (visitedSpaces.Contains(currentSpace))
                {
                    // We've been here already, and took less time to do it. So abandon this path
                    continue;
                }
                else
                {
                    visitedSpaces.Add(currentSpace);
                }

                // Get the adjacent spaces that haven't already been visited by any path before
                MapSpace[] adjacentSpaces = currentState.GetEmptyAdjacentSpaces(currentSpace).Where(x => !visitedSpaces.Contains(x)).ToArray();

                if (adjacentSpaces.Length == 0)
                {
                    // Dead end.
                    continue;
                }

                // See if any of the adjacent spaces are in the list of target spaces
                MapSpace target = Array.Find(adjacentSpaces, x => targetSpaces.Contains(x));

                if (target != null)
                {
                    var resultingPath = new List<MapSpace>(currentPath) { target };
                    return resultingPath.ToArray();
                }

                // None of the adjacent spaces are in the list, so we'll continue searching
                foreach (MapSpace space in adjacentSpaces)
                {
                    var newPath = new List<MapSpace>(currentPath) { space };
                    pathFindingQueue.Enqueue(newPath);
                }
            }

            // If we're here, we've exhausted our search. There's nothing in range
            return null;
        }

        public static void Attack(this Unit unit, State currentState)
        {
            Unit inRangeEnemy = unit.InRangeEnemy(currentState);

            if (inRangeEnemy != null)
            {
                inRangeEnemy.HitPoints -= unit.AttackStrength;

                if (inRangeEnemy.IsKilled())
                {
                    inRangeEnemy.CurrentLocation.Unit = null;
                    inRangeEnemy.CurrentLocation = null;
                }
            }
        }

        public static bool IsKilled(this Unit unit)
        {
            return unit.HitPoints <= 0;
        }
    }
}
