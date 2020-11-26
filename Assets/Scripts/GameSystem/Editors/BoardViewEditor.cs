using GameSystem.Views;
using UnityEditor;
using UnityEngine;
using Debug = System.Diagnostics.Debug;


namespace BoardSystem.Editor
{
    [CustomEditor(typeof(BoardView))]
    public class BoardInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var boardView = target as BoardView;

            if (GUILayout.Button("Generate Hex Board"))
            {
                Debug.Assert(boardView != null, nameof(boardView) + " != null");
                //board.GenerateBoard();
            }
            if (GUILayout.Button("Clear Hex Board"))
            {
                Debug.Assert(boardView != null, nameof(boardView) + " != null");
                //board.ClearBoard();
            }
        }
    }
}
