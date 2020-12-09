using BoardSystem;
using GameSystem.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    [SelectionBase]
    public class TileView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Material _highlightMaterial = null;
        private Material _originalMaterial;
        private MeshRenderer _meshRenderer;

        private Tile _model;
        public Tile Model
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

        internal Vector3 Size
        {
            set
            {
                transform.localScale = Vector3.one;

                var meshRenderer = GetComponentInChildren<MeshRenderer>();
                var meshSize = meshRenderer.bounds.size;
                var ratioX = value.x / meshSize.x;
                var ratioY = value.y / meshSize.y;
                var ratioZ = value.z / meshSize.z;

                //transform.localScale = new Vector3(ratioX, ratioY, ratioZ);
                transform.localScale = new Vector3(1.975f, 1f, 1.975f);
            }
        }

        private void Start()
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            _originalMaterial = _meshRenderer.sharedMaterial;
        }

        private void OnDestroy()
        {
            Model = null;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //GameLoop.Instance.Select(Model);
        }

        private void ModelHighlightStatusChanged(object sender, System.EventArgs e)
        {
            _meshRenderer.material = Model.IsHighlighted ? _highlightMaterial : _originalMaterial;
        }
    }
}
