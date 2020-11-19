using BoardSystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Board))]
public class BoardInspector : Editor
{
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Board board = target as Board;

            if (GUILayout.Button("Generate Hex Board"))
            {
                board.GenerateBoard();
            }
            if (GUILayout.Button("Clear Hex Board"))
            {
                board.ClearBoard();
            }
        }
}
