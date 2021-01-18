using UnityEngine;
using BoardSystem;
using GameSystem.Utils;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultPositionHelper", menuName = "GameSystem/PositionHelper")]
    public class PositionHelper : ScriptableObject
    {
        [SerializeField] private float _tileRadius = 1f;
        public float TileRadius => _tileRadius;

        public Position ToBoardPosition(Vector3 localPosition) //Pixel to hex
        {
            var q = (Mathf.Sqrt(3) / 3f * localPosition.x - 1f/ 3 * localPosition.z);
            var r = (2f / 3f * localPosition.z);
            var rounded = RoundHexCoords(new Vector3(q, -q - r, r));

            var boardPosition = new Position()
            {
                X = (int)(rounded.x),
                Y = (int)(rounded.y),
                Z = (int)(rounded.z)
            };
            return boardPosition;
        }

        public Position ToBoardPosition(Transform transform, Vector3 worldPosition) => ToBoardPosition((transform.worldToLocalMatrix * worldPosition));

        public Vector3 ToLocalPosition(Position boardPosition) //hex to pixel
        {
            var hex = boardPosition.AsVector3();

            Vector3 toLocalHex = new Vector3
            {
                x = (Mathf.Sqrt(3) * hex.x + Mathf.Sqrt(3) / 2f * hex.z),
                z = (3f / 2f * hex.z)
            };

            return toLocalHex;
        }

        public Vector3 ToWorldPosition(Transform transform, Position boardPosition)
        {
            Vector3 localPos = ToLocalPosition(boardPosition);

            return (transform.localToWorldMatrix * localPos);
        }

        public Vector3 RoundHexCoords(Vector3 hexCubeCoords)
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
