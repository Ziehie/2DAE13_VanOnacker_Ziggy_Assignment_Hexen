using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace Assets.Scripts.GameSystem.Views
{
    public class AbilityView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private RectTransform _draggingTransform;
        private GameObject _draggable;
        public string Model { get; internal set; }
        public void OnBeginDrag(PointerEventData eventData)
        {
            Canvas canvas = GetCanvas(gameObject);
            if (canvas == null) return;

            _draggingTransform = canvas.transform as RectTransform;

            CreateDraggable();
            UpdateDraggable(eventData);

            SingletonMonobehaviour<GameLoop>.Instance.OnAbilityBeginDrag(Model);
        }

        public void OnDrag(PointerEventData eventData)
        {
            UpdateDraggable(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_draggable == null) return;
            Destroy(_draggable);
        }

        private Canvas GetCanvas(GameObject go)
        {
            Canvas canvas = gameObject.GetComponent<Canvas>();
            if (canvas != null) return canvas;

            for (var parent = go.transform.parent; parent != null && canvas == null; parent = parent.parent)
            {
                canvas = parent.gameObject.GetComponent<Canvas>();
            }
            return canvas;
        }

        private void CreateDraggable()
        {
            _draggable = new GameObject("AbilityIcon");
            _draggable.transform.SetParent(_draggingTransform, false);

            _draggable.transform.SetAsLastSibling();
            Image image = _draggable.AddComponent<Image>();
            image.raycastTarget = false;
            image.sprite = GetComponent<Image>().sprite;
            image.SetNativeSize();
        }

        private void UpdateDraggable(PointerEventData eventData)
        {
            RectTransform component = _draggable.GetComponent<RectTransform>();
            if (!RectTransformUtility.ScreenPointToWorldPointInRectangle(_draggingTransform, eventData.position, eventData.pressEventCamera, out var worldPoint))
                return;

            component.position = worldPoint;
            component.rotation = _draggingTransform.rotation;
        }
    }
}
