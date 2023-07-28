using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards
{
    public class AttackedCard : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {           
            if (eventData.pointerDrag.TryGetComponent(out CardSetting selfCard) &&                
                selfCard.TryGetComponent(out DragOnDropComponent dragOnDropComponent)&& 
                (selfCard.CanAttack  &&
                dragOnDropComponent.DefaultParent.TryGetComponent(out TableComponent tableComponent) ||
                (selfCard.TypeAbility!= TypeAbilityIsTarget.None && !selfCard.IsAbilityUsed)))
            {
                var enemyCard = GetComponent<CardSetting>();

               
                if (!selfCard.IsAbilityUsed)
                {
                    switch(selfCard.TypeAbility)
                    {
                        case TypeAbilityIsTarget.AbilityOnEnemy:
                            Ability(eventData, selfCard, enemyCard);
                            return;
                        case TypeAbilityIsTarget.AbilityOnSelf:
                            Ability(eventData, selfCard, selfCard);
                            return;                            
                    }
                }
                if (selfCard.TypePlayer != enemyCard.TypePlayer)
                {                   
                    StartCoroutine(Attack(selfCard, enemyCard));
                }
            }

        }


        void Ability(PointerEventData eventData, CardSetting selfCard, CardSetting enemyCard)
        {
            selfCard.OnAbilityUsed();
            selfCard.Abillity?.Invoke(null, enemyCard);
            Managers.GameManager.Instance.Tables
                .Find(t => t.TypePlayer == selfCard.TypePlayer)
                .OnDrop(eventData);
        }


        IEnumerator Attack(CardSetting selfCard, CardSetting enemyCard)
        {
            selfCard.ChangeAttack(false);         
            selfCard.GetDamage(enemyCard.CardPropertyData.Attack);
            enemyCard.GetDamage(selfCard.CardPropertyData.Attack);
            selfCard.Refresh();
            enemyCard.Refresh();
            yield return null;
        }

    }
}
