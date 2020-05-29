using System;
using CrossFyre.Core;
using CrossFyre.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CrossFyre.UI
{
    public class UiControlPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<PointerEventData> BeginDrag, Drag, EndDrag;

        private void OnEnable()
        {
            GameEvents.GameEnded += Deactivate;
        }

        private void OnDisable()
        {
            GameEvents.GameEnded -= Deactivate;
        }

        private void Deactivate()
        {
            GetComponent<Image>().raycastTarget = false;
        }

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