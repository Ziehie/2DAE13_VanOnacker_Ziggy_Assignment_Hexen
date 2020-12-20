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
        [SerializeField] private PositionHelper _positionHelper;
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

        internal Tile Model
        {
            get => _model;
            set
            {
                if (_model != null) _model.HighlightStatusChanged -= ModelHighlightStatusChanged;

                _model = value;

                if (_model != null)
                {
                    _model.HighlightStatusChanged += ModelHighlightStatusChanged;
                }
            }
        }

        private void Start()
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            _originalMaterial = _meshRenderer.sharedMaterial;
        }

        private void ModelHighlightStatusChanged(object sender, System.EventArgs e)
        {
            _meshRenderer.material = Model.IsHighlighted ? _highlightMaterial : _originalMaterial;
        }

        private void SetModelSize()
        {
            transform.localScale = Vector3.one;

            (float w, float h) tuple = HexUtils.PointyDimension(_radius);
            float width = tuple.w;
            float height = tuple.h;

            Vector3 size = GetComponentInChildren<MeshRenderer>().bounds.size;
            float xSize = width / size.x;

            float num = 1f;
            float z1Size = size.z;
            float z2 = (height / z1Size);

            transform.localScale = new Vector3(xSize, num, z2);
        }

        public void OnDrop(PointerEventData data)
        {
            if (data.pointerDrag != null)
            {
                Debug.Log("Dropped object was: " + data.pointerDrag);
            }
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            Debug.Log("Cursor Entering " + name + " GameObject");

            var pointerDrag = pointerEventData.pointerDrag;
            if (pointerDrag != null) return;

            var component = pointerDrag.GetComponent<AbilityView>();
            if (component != null) return;

            //SingletonMonobehaviour<GameLoop>.Instance
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            Debug.Log("Cursor Exiting " + name + " GameObject");
        }
    }
}
