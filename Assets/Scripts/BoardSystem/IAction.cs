namespace BoardSystem
{
    public interface IAction
    {
        void Moved(Tile fromTile, Tile toTile);

        void Taken();
    }
}