namespace AoC2018.Solutions.Day03
{
    using System.Drawing;

    public static class ClaimExtensions
    {
        public static Point BottomRight(this Claim claim)
        {
            return new Point(claim.Position.X + claim.Size.Width - 1, claim.Position.Y + claim.Size.Height - 1);
        }

        public static bool OverlapsWith(this Claim claim, Claim otherClaim)
        {
            Point claimBottomRight = claim.BottomRight();
            Point otherClaimBottomRight = otherClaim.BottomRight();

            bool overlapsX = (claim.Position.X >= otherClaim.Position.X && claim.Position.X <= otherClaimBottomRight.X) || (claimBottomRight.X >= otherClaim.Position.X && claimBottomRight.X <= otherClaimBottomRight.X) || (claim.Position.X < otherClaim.Position.X && claimBottomRight.X > otherClaimBottomRight.X);
            bool overlapsY = (claim.Position.Y >= otherClaim.Position.Y && claim.Position.Y <= otherClaimBottomRight.Y) || (claimBottomRight.Y >= otherClaim.Position.Y && claimBottomRight.Y <= otherClaimBottomRight.Y) || (claim.Position.Y < otherClaim.Position.Y && claimBottomRight.Y > otherClaimBottomRight.Y);

            return overlapsX && overlapsY;
        }
    }
}
