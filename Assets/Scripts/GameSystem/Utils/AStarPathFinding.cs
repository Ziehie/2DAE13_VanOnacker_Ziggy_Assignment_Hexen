using System.Collections.Generic;
using BoardSystem;

namespace GameSystem.Utils
{
    public class AStarPathFinding
    {
        public delegate List<Tile> NeighbourStrategy(Tile from, bool pieceCheck);
        public delegate float DistanceStrategy(Tile from, Tile toNeighbour);
        public delegate float HeuristicsStrategy(Tile from, Tile to);

        private readonly NeighbourStrategy _neighbours;
        private readonly DistanceStrategy _distance;
        private readonly HeuristicsStrategy _heuristic;

        public AStarPathFinding(NeighbourStrategy neighbours, DistanceStrategy distance, HeuristicsStrategy heuristic)
        {
            _neighbours = neighbours;
            _distance = distance;
            _heuristic = heuristic;
        }

        public List<Tile> Path(Tile from, Tile to)
        {
            var openSet = new List<Tile>() { from };
            var cameFrom = new Dictionary<Tile, Tile>();
            var gScores = new Dictionary<Tile, float>() { { from, 0f } };
            var fScores = new Dictionary<Tile, float>() { { from, _heuristic(from, to) } };


            while (openSet.Count > 0)
            {
                Tile current = FindLowestScore(fScores, openSet);

                if (current.Equals(to))
                {
                    return ReconstructPath(cameFrom, current);
                }

                openSet.Remove(current);
                var neighbours = _neighbours(current, true);

                foreach (var neighbour in neighbours)
                {
                    var tentativeGScore = gScores[current] + _distance(current, neighbour);
                    if (tentativeGScore < gScores.GetValueOrDefault(neighbour, float.PositiveInfinity))
                    {
                        cameFrom[neighbour] = current;
                        gScores[neighbour] = tentativeGScore;
                        fScores[neighbour] = gScores[neighbour] + _heuristic(neighbour, to);

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
            return new List<Tile>(0);
        }

        private List<Tile> ReconstructPath(Dictionary<Tile, Tile> cameFrom, Tile current)
        {
            var path = new List<Tile>() { current };

            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Insert(0, current); //Insert instead of Append so we dont need to reverse the list at the end
            }
            return path;
        }

        private Tile FindLowestScore(Dictionary<Tile, float> fScores, List<Tile> openSet)
        {
            Tile currentNode = openSet[0];

            foreach (var node in openSet)
            {
                var currentFScore = fScores.GetValueOrDefault(currentNode, float.PositiveInfinity);
                var fScore = fScores.GetValueOrDefault(node, float.PositiveInfinity);

                if (fScore < currentFScore)
                {
                    currentNode = node;
                }
            }
            return currentNode;
        }
    }
}
