using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards
{
    public class DragOnDropComponent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private Vector3 _offset;
        [SerializeField]
        private float _dY = 2;
        [SerializeField]
        private Transform _defaultParent;
        public Transform DefaultParent => _defaultParent;

        public bool IsDraggable => _isDraggable; 

        [SerializeField]
        private int _dragIndex = -1;

        [SerializeField]
        private bool _isDraggable;
        [SerializeField]
        private CardSetting _cardSetting;
        private void Start()
        {
            _cardSetting = GetComponent<CardSetting>();
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_isDraggable) return;
            transform.SetParent(_defaultParent);
            transform.SetSiblingIndex(_dragIndex);
            _defaultParent.GetComponent<SortingComponent>().SortingCard();
            GetComponent<BoxCollider>().enabled = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDraggable) return;
            var newpos = Camera.main.ScreenToWorldPoint(eventData.position);
            newpos.y = _dY;
            transform.position = newpos;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _defaultParent = transform.parent;
            _isDraggable = Managers.GameManager.Instance.TurnPlayer == _cardSetting.TypePlayer &&
                ((_defaultParent.TryGetComponent(out Hand hand) && hand.Player.ManaCount>= _cardSetting.CardPropertyData.Cost) ||
                (_defaultParent.TryGetComponent(out TableComponent tableComponent)&&_cardSetting.CanAttack));            
            if (!_isDraggable) return;
            _dragIndex = transform.GetSiblingIndex();
            _offset = transform.position - Camera.main.ScreenToWorldPoint(eventData.position);
            transform.SetParent(_defaultParent.parent);
            GetComponent<BoxCollider>().enabled = false;
        }
        public void SetDefaultParent(Transform defaultParent)
        {
            _defaultParent = defaultParent;
        }
    }
}
