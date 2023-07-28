using Cards;
using System.Collections.Generic;
using System.Linq;
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
        if (eventData.pointerDrag.TryGetComponent(out DragOnDropComponent dragOnDropComponent) && dragOnDropComponent.IsDraggable)
        {
            dragOnDropComponent.DefaultParent.GetComponent<SortingComponent>().SortingCard();
            if (dragOnDropComponent.GetComponent<CardSetting>().TypePlayer == _typePlayer)
            {
                var cardSetting = dragOnDropComponent.GetComponent<CardSetting>();
                if (dragOnDropComponent.DefaultParent.TryGetComponent(out Hand hand) && 
                    (cardSetting.IsAbilityUsed || cardSetting.TypeAbility == TypeAbilityIsTarget.None))
                {
                   
                    cardSetting.CheckAbillity();
                    _listCard.Add(cardSetting);
                    hand.ListCard.Remove(cardSetting);
                    hand.Player.SetMana(cardSetting.CardPropertyData.Cost);
                    hand.CheckCardsForAbillity();
                    Managers.GameManager.Instance.UIMana.SetCountMana(hand.Player.ManaCount, hand.Player.MaxManaCount);                   
                    dragOnDropComponent.SetDefaultParent(transform); 

                    if (cardSetting.CardPropertyData.IsCharge)
                        cardSetting.ChangeAttack(true);
                    if (cardSetting.CardPropertyData.IsBattlecry && cardSetting.TypeAbility == TypeAbilityIsTarget.None)
                        cardSetting.Abillity?.Invoke(this, null);
                }
            }
        }



    }

   
}
