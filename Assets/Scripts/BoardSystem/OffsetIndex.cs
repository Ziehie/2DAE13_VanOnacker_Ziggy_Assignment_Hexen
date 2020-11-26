namespace BoardSystem
{
    [System.Serializable]
    public struct OffsetIndex
    {
        public int row, col;

        public OffsetIndex(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }
}
