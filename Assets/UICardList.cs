using Cards.Menu;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Cards.UIMenu
{
    public class UICardList : UIList
    {

        [SerializeField]
        private TypePlayer _typePlayer;
        [SerializeField]
        private Button _btBack;

        protected override void SetPrefab()
        {
            _prefab = Resources.Load<UICardButton>("UICardButton");
        }

        protected override void SetList()
        {
            for (int i = 0; i <= 31; i++)
            {
                var temp = Instantiate(_prefab, _content);
                temp.gameObject.SetActive(false);
                List.Add(temp);
            }
            List.First().gameObject.GetComponent<Image>().color = new Color(0, 1, 1);
        }

        protected new void Awake()
        {
            base.Awake();
            _btBack=GetComponentInChildren<Button>();
            _btBack.onClick.AddListener(btBackOnClick); 
            Clear();
        }

        private void btBackOnClick()
        {
            gameObject.SetActive(false);
            UIMenu.Instance.PlayerList.gameObject.SetActive(true);
            UIMenu.Instance.UIPlayButton.gameObject.SetActive(true);
            UIMenu.Instance.UIPage.gameObject.SetActive(false);
            SelectCards.Instance.ClearCards();
        }

        public void Set(TypePlayer typePlayer)
        {
            _typePlayer = typePlayer;
            ResetList();
        }
        
        public override void ResetList()
        {
            Clear();
            bool firstcard = true;
            List<uint> cardsplayer = new();
            cardsplayer.Add( Managers.PlayerManager.Instance.Players.First(t=>t.PlayerType == _typePlayer).HeroID);
            cardsplayer.AddRange( Managers.PlayerManager.Instance.Players.First(t=>t.PlayerType == _typePlayer).PoolCardsID);
            int index = 0;
            foreach(var item in cardsplayer)
            {
                UICardButton card = new();
                if (List.Any(t => !t.gameObject.activeSelf))
                {
                    card = (UICardButton)List.First(t => !t.gameObject.activeSelf);
                    card.gameObject.SetActive(true);
                    card.Set(item, _typePlayer, index);
                }
                else
                {
                    card = (UICardButton)Instantiate(_prefab, _content);
                    card.gameObject.SetActive(true);
                    card.Set(item, _typePlayer, index);
                    List.Add(card);
                }
                if(firstcard)
                {
                    card.HeroCard();
                    firstcard = false;
                }
                else
                index++;
            }
        }
    }
}