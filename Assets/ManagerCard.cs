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

    }
}
