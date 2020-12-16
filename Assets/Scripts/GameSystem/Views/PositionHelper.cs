using BoardSystem;
using GameSystem.Models;
using GameSystem.Utils;
using UnityEngine;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultPositionHelper", menuName = "GameSystem/PositionHelper")]
    public class PositionHelper : ScriptableObject
    {
        [SerializeField] private float _tileRadius = 1f;
        public float TileRadius => _tileRadius;

        public Position ToBoardPosition(Vector3 worldPosition) //pixel to point
        {
            var q = (Mathf.Sqrt(3) / 3f * worldPosition.x - 1f/ 3 * worldPosition.z);
            var r = (2f / 3f * worldPosition.z);
            var rounded = HexRound(new Vector3(q, -q - r, r));

            var boardPosition = new Position()
            {
                X = (int)(rounded.x),
                Y = (int)(rounded.y),
                Z = (int)(rounded.z)
            };
            return boardPosition;
        }

        public Vector3 ToLocalPosition(Position boardPosition)
        {
            return Vector3.one;
        }

        public Vector3 ToWorldPosition(Position tileOnBoardPosition)
        {
            var hex = tileOnBoardPosition.AsVector3();

            Vector3 toWorldHex = new Vector3
            {
                x = (Mathf.Sqrt(3) * hex.x + Mathf.Sqrt(3) / 2f * hex.y),
                z = (3f / 2f * hex.y)
            };

            return toWorldHex;
        }

        public Vector3 HexRound(Vector3 hexCubeCoords)
        {
            var rx = Mathf.Round(hexCubeCoords.x);
            var ry = Mathf.Round(hexCubeCoords.y);
            var rz = Mathf.Round(hexCubeCoords.z);

            var xDiff = Mathf.Abs(rx - hexCubeCoords.x);
            var yDiff = Mathf.Abs(ry - hexCubeCoords.y);
            var zDiff = Mathf.Abs(rz - hexCubeCoords.z);

            if (xDiff > yDiff && xDiff > zDiff)
            {
                rx = -ry - rz;
            }
            else if (yDiff > zDiff)
            {
                ry = -rx - rz;
            }

            else
            {
                rz = -rx - ry;
            }
            return new Vector3(rx, ry, rz);
        }
    }
}
