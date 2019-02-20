namespace AoC2018.Solutions.Day13
{
    using System.Collections.Generic;
    using System.Linq;

    public static class MinecartExtensions
    {
        public static void SetNextMove(this Minecart cart, int yOffset, char currentRail)
        {
            // Now work out where it'll go next
            // We only care if it's on something that'll change it's direction...
            if (currentRail == '/' && (cart.NextMove == -1 || cart.NextMove == 1))
            {
                // Coming to a left turn
                cart.TurnLeft(yOffset);
            }
            else if (currentRail == '/')
            {
                // Coming to a right turn
                cart.TurnRight(yOffset);
            }
            else if (currentRail == '\\' && (cart.NextMove == -1 || cart.NextMove == 1))
            {
                // Coming to a right turn from the left
                cart.TurnRight(yOffset);
            }
            else if (currentRail == '\\')
            {
                // Coming to a left turn from the bottom
                cart.TurnLeft(yOffset);
            }
            else if (currentRail == '+')
            {
                switch (cart.NextIntersectionBehavior % 3)
                {
                    case 0:
                        cart.TurnLeft(yOffset);
                        break;
                    case 2:
                        cart.TurnRight(yOffset);
                        break;
                }

                cart.NextIntersectionBehavior++;
            }
        }

        public static void TurnLeft(this Minecart cart, int yOffset)
        {
            if (cart.NextMove == -1)
            {
                cart.NextMove = yOffset;
            }
            else if (cart.NextMove == 1)
            {
                cart.NextMove = -yOffset;
            }
            else if (cart.NextMove == yOffset)
            {
                cart.NextMove = 1;
            }
            else
            {
                cart.NextMove = -1;
            }
        }

        public static void TurnRight(this Minecart cart, int yOffset)
        {
            cart.TurnLeft(yOffset);
            cart.NextMove *= -1;
        }

        public static Minecart GetCrashedIntoMinecart(this Minecart cart, IEnumerable<Minecart> otherMinecarts)
        {
            return otherMinecarts.FirstOrDefault(x => x != cart && x.Position == cart.Position);
        }
    }
}
