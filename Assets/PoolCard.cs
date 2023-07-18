using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.Game
{
    public class PoolCard : MonoBehaviour
    {
        public static PoolCard Instance;
        [SerializeField]
        private List<CardSetting> _cards = new();
        public List<CardSetting> Cards=> _cards;
        private void Awake()
        {
            Instance = this; 
            for (int i = 0; i < 20; i++)
            {
                var card = Instantiate(Managers.ManagerCard.Instance.CardSetting, this.transform);
                card.gameObject.SetActive(false);

                _cards.Add(card);
            }
        }

        private void Start()
        {
          
        }
       
    }
}
