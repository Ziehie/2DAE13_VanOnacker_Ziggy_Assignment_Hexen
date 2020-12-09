namespace BoardSystem
{
    public interface IAction<TPiece> where TPiece : class, IAction<TPiece>
    {
        void Moved(Board<TPiece> board, Tile fromTile, Tile toTile);

        void Taken(Board<TPiece> board);
    }
}