using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cards
{
    public class AbillityEffectComponent : MonoBehaviour
    {
        public static AbillityEffectComponent Instance;


        [SerializeField]
        private uint
            _idMurlocScout = 109,
            _idBoar = 107,
            _idMechanicalDragonling = 108;

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> 
        /// TableComponent - Стол
        /// CardSetting - Карта
        /// bool - Есть ли выбор цели
        /// </returns>
        public Action<TableComponent, CardSetting> Effect(uint id,out TypeAbilityIsTarget isSelectCard)
        {
            isSelectCard = TypeAbilityIsTarget.None;
            switch (id)
            {
                case 101:
                    return (TableComponent tableComponent, CardSetting Card) =>
                    MurlocHavePlusOneAttack(tableComponent, null);
                case 105:
                    isSelectCard = TypeAbilityIsTarget.AbilityOnSelf;
                    return (TableComponent tableComponent, CardSetting Card) =>
                    RestoreHealth(Card, 1);
                case 201:
                    Debug.Log("Destroy opponent's weapon");
                    break;
                case 102:
                case 205:
                case 301:
                case 302:
                case 405:
                case 601:
                    isSelectCard = TypeAbilityIsTarget.AbilityOnEnemy;
                    return (TableComponent tableComponent, CardSetting Card) =>
                        DamageEffect(Card, 1);
                case 206:
                    return (TableComponent tableComponent, CardSetting Card) =>
                        AddCard(_idMurlocScout, tableComponent);
                case 207:
                case 403:
                    Debug.Log("Draw a card");
                    break;
                case 305:
                    Debug.Log("Your other minions have + 1 Attack");
                    break;
                case 306:
                    return (TableComponent tableComponent, CardSetting Card) =>
                        AddCard(_idBoar, tableComponent);
                case 307:
                    Debug.Log(" Give a friendly miniom +1/+1");
                    break;
                case 402:
                    return (TableComponent tableComponent, CardSetting Card) =>
                        AddCard(_idMechanicalDragonling, tableComponent);
                case 502:
                    return (TableComponent tableComponent, CardSetting Card) =>
                        RestoreHealthAll(tableComponent, 1);
                case 503:
                    Debug.Log("Gain +1/+1 for each other friendly minion on the battlefield");
                    break;
                case 504:
                    Debug.Log("Whenever this minion takes damage, gain +3 Attack");
                    break;
                case 505:
                    var HeroCard = Managers.GameManager.Instance.Hands.First(t => t.TypePlayer != Managers.GameManager.Instance.TurnPlayer).HeroCard;
                    HeroCard.SetCardPropertyData(Health: HeroCard.CardPropertyData.Health - 3);
                    break;
                case 506:
                    isSelectCard = TypeAbilityIsTarget.AbilityOnEnemy;
                    return (TableComponent tableComponent, CardSetting Card) =>
                    DamageEffect(Card, 2);
                case 702:
                    Debug.Log("Your other minions have +1/+1");
                    break;
            }
            return null;
        }

        private void RestoreHealthAll(TableComponent tableComponent, int health)
        {
            tableComponent.ListCard.ForEach(card => DamageEffect(card, health));
        }

        private void RestoreHealth(CardSetting card, int health)
        {
            DamageEffect(card, -health);
        }

        private void AddCard(uint id, TableComponent tableComponent)
        {
            var card = Managers.ManagerCard.Instance.SetCard(id, tableComponent.transform.position, tableComponent.TypePlayer);
            card.gameObject.GetComponent<DragOnDropComponent>().SetDefaultParent(tableComponent.transform);
            card.transform.SetParent(tableComponent.transform);
            Managers.ManagerCard.Instance.PoolCards.Add(card);
            tableComponent.ListCard.Add(card);
            tableComponent.GetComponent<SortingComponent>().SortingCard();
        }
        private void MurlocHavePlusOneAttack(TableComponent tableComponent, CardSetting cardSetting)
        {
            tableComponent.ListCard
                .Where(t => t.CardPropertyData.Type == CardUnitType.Murloc)
                .ToList()
                .ForEach(card => card.SetCardPropertyData(Attack: card.CardPropertyData.Attack + 1));
        }

        private void DamageEffect(CardSetting card, int damage)
        {
            card.SetCardPropertyData(Health: card.CardPropertyData.Health - damage);
        }
    }
}
