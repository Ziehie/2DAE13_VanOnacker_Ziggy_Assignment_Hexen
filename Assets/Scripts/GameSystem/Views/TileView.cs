using System;
using BoardSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    [SelectionBase]
    public class TileView : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private PositionHelper _positionHelper = null;
        [SerializeField] private Material _highlightMaterial = null;

        private Material _originalMaterial;
        private MeshRenderer _meshRenderer;
        private Tile _model;
        private float _radius = 1f;

        internal float Radius
        {
            set
            {
                _radius = value;
                SetModelSize();
            }
        }

        private void Start()
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            _originalMaterial = _meshRenderer.sharedMaterial;

            GameLoop.Instance.Initialized += OnGameInitialized;
        }

        private void OnGameInitialized(object sender, EventArgs e)
        {
            _model = GameLoop.Instance.Board.TileAt(_positionHelper.ToBoardPosition(transform.localPosition));
            _model.HighlightStatusChanged += OnModelHighlightStatusChanged;
        }

        private void OnModelHighlightStatusChanged(object sender, EventArgs e)
        {
            _meshRenderer.material = _model.IsHighlighted ? _highlightMaterial : _originalMaterial;
        }

        private void SetModelSize()
        {
            transform.localScale = Vector3.one;

            Vector2 pointySize = GetPointyDimension(_radius);
            Vector3 size = GetComponentInChildren<MeshRenderer>().bounds.size;
            
            transform.localScale = new Vector3(pointySize.x / size.x, 1f, pointySize.y / size.z);
        }

        public void OnPointerEnter(PointerEventData pointerEventData) => GameLoop.Instance.OnEnterTile(_model);

        public void OnPointerExit(PointerEventData pointerEventData) => GameLoop.Instance.OnExitTile(_model);

        public void OnDrop(PointerEventData pointerEventData) => GameLoop.Instance.OnAbilityReleased(_model);

        public Vector2 GetPointyDimension(float radius) => new Vector2(Mathf.Sqrt(3f) * radius, 2f * radius);
    }
}
