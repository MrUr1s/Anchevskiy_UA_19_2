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
            if (eventData.pointerDrag.TryGetComponent(out Cards.CardSetting selfCard) &&
                selfCard.CanAttack &&
                selfCard.GetComponent<DragOnDropComponent>().DefaultParent.TryGetComponent(out TableComponent tableComponent))
            {
                var enemyCard = GetComponent<Cards.CardSetting>();
                if (tableComponent.TypePlayer != enemyCard.TypePlayer)
                {
                    selfCard.GetDamage(enemyCard.CardPropertyData.Attack);
                    enemyCard.GetDamage(selfCard.CardPropertyData.Attack);
                    selfCard.Refresh();
                    enemyCard.Refresh();
                    selfCard.ChangeAttack(false);
                }
            }

        }


    }
}
