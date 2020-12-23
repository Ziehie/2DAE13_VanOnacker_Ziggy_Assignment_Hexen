using BoardSystem;

namespace GameSystem.BoardCalculations
{
    public struct CubeOffset
    {
        public Position OffsetPosition;

        public CubeOffset(int x, int y, int z)
        {
            OffsetPosition.X = x;
            OffsetPosition.Y = y;
            OffsetPosition.Z = z;
        }
    }
}
