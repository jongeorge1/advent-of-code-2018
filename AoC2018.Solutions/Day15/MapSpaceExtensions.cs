namespace AoC2018.Solutions.Day15
{
    public static class MapSpaceExtensions
    {
        public static bool IsEmpty(this MapSpace space)
        {
            return space.Unit == null;
        }
    }
}
