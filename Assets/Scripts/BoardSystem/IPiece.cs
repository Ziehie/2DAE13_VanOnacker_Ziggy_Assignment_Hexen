namespace BoardSystem
{
    public interface IPiece
    {
        void Moved(Tile fromTile, Tile toTile);

        void Taken();
    }
}
