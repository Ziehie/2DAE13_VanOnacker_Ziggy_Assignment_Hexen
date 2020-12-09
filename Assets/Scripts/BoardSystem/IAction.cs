namespace BoardSystem
{
    public interface IAction<TPiece> where TPiece : class, IAction<TPiece>
    {
        void Moved(Tile fromTile, Tile toTile);

        void Taken();
    }
}