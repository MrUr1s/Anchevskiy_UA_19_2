using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards
{
    public class DragOnDropComponent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
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
        [SerializeField]
        private BoxCollider _boxCollider;


       
       

        private void Start()
        {
            _cardSetting = GetComponent<CardSetting>();
            _boxCollider = GetComponent<BoxCollider>();
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_isDraggable) return;
            transform.SetParent(_defaultParent);
            transform.SetSiblingIndex(_dragIndex);
            _defaultParent.GetComponent<SortingComponent>().SortingCard();
            _boxCollider.enabled = true;
            Managers.GameManager.Instance.Tables.First(t => t.TypePlayer != _cardSetting.TypePlayer).ListCard.ForEach(t => t.VisibleTarget(false));
            Managers.GameManager.Instance.Hands.First(t => t.TypePlayer != _cardSetting.TypePlayer).HeroCard.VisibleTarget(false);

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

            _cardSetting.SetAbility(
            AbillityEffectComponent.Instance.Effect(_cardSetting.CardPropertyData.Id, out TypeAbilityIsTarget value));
            _cardSetting.SetIsAbilitySelectCard(value);

            if (_cardSetting.CanAttack||(_cardSetting.TypeAbility!=TypeAbilityIsTarget.None && !_cardSetting.IsAbilityUsed))
            {
                var enemyCards = Managers.GameManager.Instance.Tables.First(t => t.TypePlayer != _cardSetting.TypePlayer).ListCard;
                if (enemyCards.Any(card => card.IsTaunt))
                    enemyCards.ForEach(card =>
                    {
                        card.VisibleTarget(card.IsTaunt);
                    });
                else
                {
                    enemyCards.ForEach(card =>
                    {
                        card.VisibleTarget(true);
                    });
                    Managers.GameManager.Instance.Hands.First(t => t.TypePlayer != _cardSetting.TypePlayer).HeroCard.VisibleTarget(true);
                }
            }
            _dragIndex = transform.GetSiblingIndex();
            transform.SetParent(_defaultParent.parent);
            _boxCollider.enabled = false;
        }
        public void SetDefaultParent(Transform defaultParent)
        {
            _defaultParent = defaultParent;
        }
    }
}
