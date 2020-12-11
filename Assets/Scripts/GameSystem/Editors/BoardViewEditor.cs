using GameSystem.Views;
using UnityEditor;
using UnityEngine;
using Debug = System.Diagnostics.Debug;


namespace BoardSystem.Editor
{
    [CustomEditor(typeof(BoardView))]
    public class BoardViewEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var boardView = target as BoardView;
            var tileViewFactorySp = serializedObject.FindProperty("_tileViewFactory");
            var tileViewFactory = tileViewFactorySp.objectReferenceValue as TileViewFactory;
            var game = GameLoop.Instance;
            var board = game.Board;

            if (GUILayout.Button("Generate Hex Board"))
            {
                Debug.Assert(boardView != null, nameof(boardView) + " != null");

                foreach (var tile in board.Tiles)
                {
                    tileViewFactory?.CreateTileView(board, tile, boardView.transform);
                }
            }
            if (GUILayout.Button("Clear Hex Board"))
            {
                Debug.Assert(boardView != null, nameof(boardView) + " != null");

                foreach (var tile in board.Tiles)
                {
#if UNITY_EDITOR
                    //DestroyImmediate(tile.Value.gameObject, false);
#endif
                    //Destroy(tile.Value.gameObject);
                    //board.Clear();
                }
            }
        }
    }
}
