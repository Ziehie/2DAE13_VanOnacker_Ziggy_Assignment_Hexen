using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BoardSystem
{
    public class Board : MonoBehaviour
    {
        public static Board instance;

        //Map settings
        public int mapWidth;
        public int mapHeight;

        //Hex Settings
        public float hexRadius = 1;
        public Material hexMaterial;

        //Generation Options
        public bool addColliders = true;
        public bool drawOutlines = true;
        public Material lineMaterial;

        //Internal variables
        private Dictionary<string, Tile> _board = new Dictionary<string, Tile>();
        private Mesh _hexMesh = null;

        private CubeIndex[] _directions =
            new CubeIndex[]
            {
                new CubeIndex(1, -1, 0),
                new CubeIndex(1, 0, -1),
                new CubeIndex(0, 1, -1),
                new CubeIndex(-1, 1, 0),
                new CubeIndex(-1, 0, 1),
                new CubeIndex(0, -1, 1)
            };

        #region Getters and Setters

        public Dictionary<string, Tile> Tiles
        {
            get { return _board; }
        }

        #endregion

        #region Public Methods

        public void GenerateBoard()
        {
            ClearBoard();
            GetMesh();
            GenerateHexShapedBoard();
        }

        public void ClearBoard()
        {
            Debug.Log("Clearing Board...");

            foreach (var tile in _board)
            {
                DestroyImmediate(tile.Value.gameObject, false);
            }
            _board.Clear();
        }

        public Tile TileAt(CubeIndex index)
        {
            return _board.ContainsKey(index.ToString()) ? _board[index.ToString()] : null;
        }

        public Tile TileAt(int x, int y, int z)
        {
            return TileAt(new CubeIndex(x, y, z));
        }

        public Tile TileAt(int x, int z)
        {
            return TileAt(new CubeIndex(x, z));
        }

        public List<Tile> GetNeighbors(Tile tile)
        {
            List<Tile> tiles = new List<Tile>();

            if (tile == null) return tiles;

            CubeIndex cubeIdx;

            for (int i = 0; i < 6; i++)
            {
                cubeIdx = tile.index + _directions[i];
                if (_board.ContainsKey(cubeIdx.ToString()))
                {
                    tiles.Add(_board[cubeIdx.ToString()]);
                }
            }
            return tiles;
        }

        public List<Tile> GetNeighbors(CubeIndex index)
        {
            return GetNeighbors(TileAt(index));
        }

        public List<Tile> GetNeighbors(int x, int y, int z)
        {
            return GetNeighbors(TileAt(x, y, z));
        }

        public List<Tile> GetNeighbors(int x, int z)
        {
            return GetNeighbors(TileAt(x, z));
        }

        public List<Tile> TilesInRange(Tile center, int range)
        {
            //Return tiles range steps from center, http://www.redblobgames.com/grids/hexagons/#range
            List<Tile> tiles = new List<Tile>();
            CubeIndex cubeIdx;

            for (int dx = -range; dx <= range; dx++)
            {
                for (int dy = Mathf.Max(-range, -dx - range); dy <= Mathf.Min(range, -dx + range); dy++)
                {
                    cubeIdx = new CubeIndex(dx, dy, -dx - dy) + center.index;
                    if (_board.ContainsKey(cubeIdx.ToString()))
                        tiles.Add(_board[cubeIdx.ToString()]);
                }
            }
            return tiles;
        }

        public List<Tile> TilesInRange(CubeIndex index, int range)
        {
            return TilesInRange(TileAt(index), range);
        }

        public List<Tile> TilesInRange(int x, int y, int z, int range)
        {
            return TilesInRange(TileAt(x, y, z), range);
        }

        public List<Tile> TilesInRange(int x, int z, int range)
        {
            return TilesInRange(TileAt(x, z), range);
        }

        public int Distance(CubeIndex a, CubeIndex b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);
        }

        public int Distance(Tile a, Tile b)
        {
            return Distance(a.index, b.index);
        }

        #endregion

        #region Private Methods

        private void Awake()
        {
            if (!instance) instance = this;
            GenerateBoard();
        }

        private void GetMesh()
        {
            _hexMesh = null;
            Tile.GetHexMesh(hexRadius, ref _hexMesh);
        }

        private void GenerateHexShapedBoard()
        {
            Debug.Log("Generating board...");

            Tile tile;
            Vector3 pos = Vector3.zero;

            int mapSize = Mathf.Max(mapWidth, mapHeight);

            for (int q = -mapSize; q <= mapSize; q++)
            {
                int r1 = Mathf.Max(-mapSize, -q - mapSize);
                int r2 = Mathf.Min(mapSize, -q + mapSize);
                for (int r = r1; r <= r2; r++)
                {
                    pos.x = hexRadius * 3.0f / 2.0f * q;
                    pos.z = hexRadius * Mathf.Sqrt(3.0f) * (r + q / 2.0f);

                    tile = CreateHexTile(pos, ("Hex[" + q + "," + r + "," + (-q - r).ToString() + "]"));
                    tile.index = new CubeIndex(q, r, -q - r);
                    _board.Add(tile.index.ToString(), tile);
                }
            }
        }

        private Tile CreateHexTile(Vector3 postion, string name)
        {
            GameObject go = new GameObject(name, typeof(MeshFilter), typeof(MeshRenderer), typeof(Tile));

            if (addColliders)
                go.AddComponent<MeshCollider>();

            if (drawOutlines)
                go.AddComponent<LineRenderer>();

            go.transform.position = postion;
            go.transform.parent = this.transform;

            Tile tile = go.GetComponent<Tile>();
            MeshFilter fil = go.GetComponent<MeshFilter>();
            MeshRenderer ren = go.GetComponent<MeshRenderer>();

            fil.sharedMesh = _hexMesh;

            ren.material = (hexMaterial)
                ? hexMaterial
                : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");

            if (addColliders)
            {
                MeshCollider col = go.GetComponent<MeshCollider>();
                col.sharedMesh = _hexMesh;
            }

            if (drawOutlines)
            {
                LineRenderer lines = go.GetComponent<LineRenderer>();
                lines.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
                lines.receiveShadows = false;

                lines.startWidth = 0.1f;
                lines.endWidth = 0.1f;
                lines.startColor = Color.black;
                lines.endColor = Color.black;
                lines.material = lineMaterial;

                lines.positionCount = 7;

                for (int vert = 0; vert <= 6; vert++)
                {
                    lines.SetPosition(vert, Tile.GetCorner(tile.transform.position, hexRadius, vert));
                }
                    
            }
            return tile;
        }
        #endregion
    }
}

