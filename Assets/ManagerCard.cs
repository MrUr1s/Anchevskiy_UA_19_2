using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards.ScriptableObjects;
using System.Linq;
using Cards;

namespace Managers
{
    public class ManagerCard : MonoBehaviour
    {
        public static ManagerCard Instance { get; private set; }
        public List<CardPropertiesData> Cards => _cards;

        public CardSetting CardSetting => _cardSetting;

        private CardSetting _cardSetting;

        public List<CardSetting> PoolCards => _poolCards;

        [SerializeField]
        private List<CardSetting> _poolCards;

        [SerializeField]
        private float _scaleSize = 1.5f;

        private string _Path = "Cards";
        [SerializeField]
        private List<CardPropertiesData> _cards;
        private void Awake()
        {
            Instance = this;
            _cardSetting = Resources.Load<CardSetting>("CardPrefab");
            foreach (var Conf in Resources.LoadAll<CardPackConfiguration>(_Path))
                _cards = Conf.UnionProperties(Cards).ToList();
        }

        public CardPropertiesData AddCardConf(uint ID)
        {
            return Cards.FirstOrDefault(t => t.Id == ID);
        }

        public CardSetting SetCard(uint id, Vector3 pos, TypePlayer typePlayer)
        {
            CardSetting card;
            if (PoolCards.Any(t => !t.gameObject.activeSelf))
            {
                card = PoolCards.First(t => !t.gameObject.activeSelf);
                card.SetTypePlayer(typePlayer);
                card.ClearEvent();
                card.OnEnterCard += Card_OnEnterCard;
                card.OnExitCard += Card_OnExitCard;
                card.ReDraw(id);
            }
            else
            {
                card = Instantiate(_cardSetting);
                card.gameObject.SetActive(true);
                card.ReDraw(id);
                card.transform.position = pos;
                card.SetTypePlayer(typePlayer);
                card.OnEnterCard += Card_OnEnterCard;
                card.OnExitCard += Card_OnExitCard;
                _poolCards.Add(card);
            }
            if (!card.TryGetComponent(out DragOnDropComponent dragOnDropComponent))
                card.gameObject.AddComponent<DragOnDropComponent>();
            if (!card.TryGetComponent(out AttackedCard attackedCard))
                card.gameObject.AddComponent<AttackedCard>();
            return card;
        }
        private void Card_OnExitCard(CardSetting cardSetting)
        {
            cardSetting.transform.localScale = cardSetting.DefaultScale;
        }

        private void Card_OnEnterCard(CardSetting cardSetting)
        {
            cardSetting.transform.localScale *= _scaleSize;
        }

    }
}
