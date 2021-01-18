using System.Collections.Generic;
using BoardSystem;

namespace GameSystem.Utils
{
    public class BreadthFirstAreaSearch
    {
        public delegate List<Tile> NeighbourStrategy(Tile from, bool pieceCheck);

        public delegate float DistanceStrategy(Tile from, Tile to);

        private readonly NeighbourStrategy _neighbours;
        private readonly DistanceStrategy _distance; //returns float between 2 points

        public BreadthFirstAreaSearch(NeighbourStrategy neighbours, DistanceStrategy distance)
        {
            _neighbours = neighbours;
            _distance = distance;
        }

        public List<Tile> Area(Tile startTile, int maxDistance)
        {
            var nearbyPositions = new List<Tile>();

            var nodesToVisit = new Queue<Tile>();
            nodesToVisit.Enqueue(startTile);

            while (nodesToVisit.Count > 0)
            {
                var currentNode = nodesToVisit.Dequeue();
                var neighbours = _neighbours(currentNode, true);

                foreach (var neighbour in neighbours)
                {
                    if (nearbyPositions.Contains(neighbour)) continue;

                    if (_distance(startTile, neighbour) < maxDistance)
                    {
                        nearbyPositions.Add(neighbour);
                        nodesToVisit.Enqueue(neighbour);
                    }
                }
            }
            return nearbyPositions;
        }
    }
}