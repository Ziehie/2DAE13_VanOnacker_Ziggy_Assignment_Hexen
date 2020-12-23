using System;
using BoardSystem;
using GameSystem.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace GameSystem.Views
{
    [SelectionBase]
    public class TileView : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private PositionHelper _positionHelper = null;
        [SerializeField] private Material _highlightMaterial = null;
        [SerializeField] private bool _isMeshScale = false;

        private Material _originalMaterial;
        private MeshRenderer _meshRenderer;
        private Tile _model;
        private float _radius = 1f;

        internal float Radius
        {
            set
            {
                _radius = value;
                if (!_isMeshScale) return;

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

            (float w, float h) tuple = PointyDimension(_radius);
            float width = tuple.w;
            float height = tuple.h;

            Vector3 size = GetComponentInChildren<MeshRenderer>().bounds.size;
            float xSize = width / size.x;

            float num = 1f;
            float z1Size = size.z;
            float z2 = (height / z1Size);

            transform.localScale = new Vector3(xSize, num, z2);
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            var pointerDrag = pointerEventData.pointerDrag;
            if (pointerDrag == null) return;

            var component = pointerDrag.GetComponent<AbilityView>();
            if (component == null) return;

           GameLoop.Instance.OnAbilityHoldActivity(_model, component.Model, true);
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            var pointerDrag = pointerEventData.pointerDrag;
            if (pointerDrag == null) return;

            var component = pointerDrag.GetComponent<AbilityView>();
            if (component == null) return;

            GameLoop.Instance.OnAbilityHoldActivity(_model, component.Model, false);
        }

        public void OnDrop(PointerEventData pointerEventData)
        {
            var pointerDrag = pointerEventData.pointerDrag;
            if (pointerDrag == null) return;

            var component = pointerDrag.GetComponent<AbilityView>();
            if (component == null) return;

            GameLoop.Instance.OnAbilityReleased(component.Model, _model);
        }

        public static (float w, float h) PointyDimension(float size) => (Mathf.Sqrt(3f) * size, 2f * size);
    }
}
