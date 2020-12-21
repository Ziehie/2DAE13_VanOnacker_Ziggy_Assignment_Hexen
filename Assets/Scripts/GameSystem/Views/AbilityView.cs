using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace GameSystem.Views
{
    public class AbilityView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private RectTransform _draggingTransform;
        private GameObject _draggable;
        public string Model { get; internal set; }
        public void OnBeginDrag(PointerEventData eventData)
        {
            LocateDraggingTransform();
            CreateDraggable();
            UpdateDraggable(eventData);

            GameLoop.Instance.OnAbilityBeginDrag(Model);
        }

        public void OnDrag(PointerEventData eventData)
        {
            UpdateDraggable(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DestroyDraggable();
        }

        private Canvas GetCanvas(GameObject go)
        {
            var canvas = gameObject.GetComponent<Canvas>();
            if (canvas != null) return canvas;

            for (var parent = go.transform.parent; parent != null && canvas == null; parent = parent.parent)
            {
                canvas = parent.gameObject.GetComponent<Canvas>();
            }
            return canvas;
        }

        private void LocateDraggingTransform()
        {
            var canvas = GetCanvas(gameObject);
            if (canvas == null) return;

            _draggingTransform = canvas.transform as RectTransform;
        }

        private void CreateDraggable()
        {
            _draggable = new GameObject("AbilityIcon");
            _draggable.transform.SetParent(_draggingTransform, false);

            _draggable.transform.SetAsLastSibling();

            var image = _draggable.AddComponent<Image>();
            image.raycastTarget = false;
            image.sprite = GetComponent<Image>().sprite;
            image.SetNativeSize();
        }

        private void UpdateDraggable(PointerEventData eventData)
        {
            var component = _draggable.GetComponent<RectTransform>();
            if (!RectTransformUtility.ScreenPointToWorldPointInRectangle(_draggingTransform, eventData.position, eventData.pressEventCamera, out var worldPoint))
                return;

            component.position = worldPoint;
            component.rotation = _draggingTransform.rotation;
        }

        private void DestroyDraggable()
        {
            if (_draggable == null) return;
            Destroy(_draggable);
        }

        public void Destroy()
        {
            DestroyDraggable();
            Destroy(gameObject);
        }
    }
}
