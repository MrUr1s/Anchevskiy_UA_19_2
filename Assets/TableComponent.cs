using Cards;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SortingComponent))]
public class TableComponent : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private TypePlayer _typePlayer;

    [SerializeField]
    private List<CardSetting> _listCard;

    public List<CardSetting> ListCard => _listCard;

    public TypePlayer TypePlayer  => _typePlayer; 

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out DragOnDropComponent card)&& card.IsDraggable)
        {
            card.DefaultParent.GetComponent<SortingComponent>().SortingCard();
            if (card.GetComponent<CardSetting>().TypePlayer == _typePlayer)
            {
                if (card.DefaultParent.TryGetComponent(out Hand hand))
                {
                    var cardSetting = card.GetComponent<CardSetting>();
                    _listCard.Add(cardSetting);
                    hand.ListCard.Remove(cardSetting);

                    hand.Player.SetMana(cardSetting.CardPropertyData.Cost);
                    Managers.GameManager.Instance.UIMana.SetCountMana(hand.Player.ManaCount, hand.Player.MaxManaCount);
                }
                card.SetDefaultParent(transform);
            }
        }
    }

}
