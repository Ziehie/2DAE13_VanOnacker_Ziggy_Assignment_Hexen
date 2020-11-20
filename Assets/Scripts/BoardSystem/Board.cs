using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BoardSystem
{
    public class Board : MonoBehaviour
    {
        public static Board instance;

        //Map settings
        [SerializeField] private int _mapWidth = 3;
        [SerializeField] private int _mapHeight = 2;

        //Hex Settings
        [SerializeField] private float _hexRadius = 1;
        [SerializeField] private Material _hexMaterial= null;

        //Generation Options
        [SerializeField] private bool _enableColliders = true;
        [SerializeField] private bool _enableOutlines = true;
        [SerializeField] private Material _lineMaterial = null;

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

        public Dictionary<string, Tile> Tiles => _board;

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
                Destroy(tile.Value.gameObject);
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

            for (int i = 0; i < 6; i++)
            {
                var cubeIdx = tile.index + _directions[i];
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
                    {
                        tiles.Add(_board[cubeIdx.ToString()]);
                    }
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
        }

        private void GetMesh()
        {
            _hexMesh = null;
            Tile.GetHexMesh(_hexRadius, ref _hexMesh);
        }

        private void GenerateHexShapedBoard()
        {
            Debug.Log("Generating board...");

            Vector3 pos = Vector3.zero;
            int mapSize = Mathf.Max(_mapWidth, _mapHeight);

            for (int q = -mapSize; q <= mapSize; q++)
            {
                int r1 = Mathf.Max(-mapSize, -q - mapSize);
                int r2 = Mathf.Min(mapSize, -q + mapSize);
                for (int r = r1; r <= r2; r++)
                {
                    pos.x = _hexRadius * 3.0f / 2.0f * q;
                    pos.z = _hexRadius * Mathf.Sqrt(3.0f) * (r + q / 2.0f);

                    var tile = CreateHexTile(pos, ("Hex[" + q + "," + r + "," + (-q - r).ToString() + "]"));
                    tile.index = new CubeIndex(q, r, -q - r);
                    _board.Add(tile.index.ToString(), tile);
                }
            }
        }

        private Tile CreateHexTile(Vector3 position, string name)
        {
            GameObject hexObject = new GameObject(name, typeof(MeshFilter), typeof(MeshRenderer), typeof(Tile));

            if (_enableColliders)
                hexObject.AddComponent<MeshCollider>();

            if (_enableOutlines)
                hexObject.AddComponent<LineRenderer>();

            hexObject.transform.position = position;
            hexObject.transform.parent = this.transform;

            Tile tile = hexObject.GetComponent<Tile>();
            MeshFilter meshFilter = hexObject.GetComponent<MeshFilter>();
            MeshRenderer meshRenderer = hexObject.GetComponent<MeshRenderer>();

            meshFilter.sharedMesh = _hexMesh;

            meshRenderer.material = (_hexMaterial)
                ? _hexMaterial
                : UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat");

            if (_enableColliders)
            {
                MeshCollider col = hexObject.GetComponent<MeshCollider>();
                col.sharedMesh = _hexMesh;
            }

            if (_enableOutlines)
            {
                LineRenderer lines = hexObject.GetComponent<LineRenderer>();
                lines.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
                lines.receiveShadows = false;

                lines.startWidth = 0.1f;
                lines.endWidth = 0.1f;
                lines.startColor = Color.black;
                lines.endColor = Color.black;
                lines.material = _lineMaterial;

                lines.positionCount = 7;

                for (int vertex = 0; vertex <= 6; vertex++)
                {
                    lines.SetPosition(vertex, Tile.GetCorner(tile.transform.position, _hexRadius, vertex));
                }
                    
            }
            return tile;
        }
        #endregion
    }
}

