using BoardSystem;
using UnityEditor;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

[CustomEditor(typeof(Board))]
public class BoardInspector : Editor
{
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var board = target as Board;

            if (GUILayout.Button("Generate Hex Board"))
            {
                Debug.Assert(board != null, nameof(board) + " != null");
                board.GenerateBoard();
            }
            if (GUILayout.Button("Clear Hex Board"))
            {
                Debug.Assert(board != null, nameof(board) + " != null");
                board.ClearBoard();
            }
        }
}
