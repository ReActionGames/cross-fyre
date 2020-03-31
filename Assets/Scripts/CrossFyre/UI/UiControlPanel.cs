using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CrossFyre.UI
{
    public class UiControlPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<PointerEventData> BeginDrag, Drag, EndDrag;

        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDrag?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Drag?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDrag?.Invoke(eventData);
        }
    }
}
