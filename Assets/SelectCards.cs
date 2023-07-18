using System.Collections.Generic;
using UnityEngine;
using Managers;
using System.Linq;
using Cards.UIMenu;

namespace Cards.Menu
{
    public class SelectCards : MonoBehaviour
    {
        public static SelectCards Instance { get; private set; }
        public TypePlayer PlayerType => _playerType;
        [SerializeField]
        private TypePlayer _playerType;
        [SerializeField]
        private List<uint> _heroCards;
        [SerializeField]
        private List<uint> _cards;
        [SerializeField]
        private bool _isHeroCard = true;
        private int _page = 0;
        private List<CardSetting> _cardsPrefab = new();
        [SerializeField]
        private int _indexCard;
        [SerializeField]
        private Vector3 _cardPosition=new Vector3(-20f , 4.5f, 0);
        [SerializeField]
        private float _scaleSize=1.5f;

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            for(int i=0; i<10; i++)
            {
                var prefab = Instantiate(ManagerCard.Instance.CardSetting, transform);
                prefab.OnClickCard += OnClickCard;
                prefab.OnEnterCard += OnEnterCard;
                prefab.OnExitCard += OnExitCard;
                prefab.transform.position = _cardPosition+new Vector3(7.5f * i,0);
                if (i >= 5)
                    prefab.transform.position = _cardPosition + new Vector3(7.5f * (i-5), -12f);
                prefab.gameObject.SetActive(false);
                _cardsPrefab.Add(prefab);
            }
            ManagerCard.Instance.Cards                
                .ForEach(t =>
                {
                    if (t.Type == CardUnitType.Hero)
                        _heroCards.Add(t.Id);
                    else
                        _cards.Add(t.Id);
                });
        }

        public void OnEnterCard(CardSetting cardSetting)
        {
            Debug.Log("CardSetting OnPointerEnter");
            cardSetting.gameObject.transform.localScale *= _scaleSize;
        }

        public void OnExitCard(CardSetting cardSetting)
        {
            Debug.Log("CardSetting OnPointerExit");
            cardSetting.gameObject.transform.localScale /= _scaleSize;
        }
        private void OnClickCard(uint ID)
        {
            Debug.Log("SelectCards OnPointerClick");
            if (_isHeroCard)
            {
                PlayerManager.Instance.Players.First(t => t.PlayerType == PlayerType).AddHeroCard(ID);
            }
            else
            {
                PlayerManager.Instance.Players.First(t => t.PlayerType == PlayerType).AddPoolCard(_indexCard, ID);
            }
            UIMenu.UIMenu.Instance.CartList.ResetList();
            PlayerManager.Instance.SavePlayer();
        }
        public void SetPlayerType(TypePlayer PlayerType)
        {
            _playerType = PlayerType;
        }

        public void SetIndexCard(int IndexCard)
        {
            _indexCard = IndexCard;
        }

        public void VisibleCards(bool IsHeroCards)
        {
            _page = 0;
            _isHeroCard = IsHeroCards;
            NextCards();
        }

        public void NextCards()
        {
            ClearCards();
            if (_isHeroCard)
            {
                NextCards(_heroCards);
            }
            else
            {
                NextCards(_cards); 
              
            }
        }

        public void ClearCards()
        {
            foreach (var prefab in _cardsPrefab)
            {
                prefab.gameObject.SetActive(false);
            }
        }
        private void NextCards(List<uint> cards)
        {
            if (cards.Count > (_page + 1) * 10)
                _page++;
            int i = 0;
            for (int j = 10 * _page; j < cards.Count; j++)
            {
                var card = cards[j];
                if (i < 10)
                {
                    _cardsPrefab[i].gameObject.SetActive(true);
                    _cardsPrefab[i].ReDraw(card);
                    i++;
                }
                else
                    break;
            }
        }
        public void PreviousCards()
        {
            if (_page > 0)
            {
                ClearCards();
                if (_isHeroCard)
                {
                    PreviousCards(_heroCards);
                }
                else
                {
                    PreviousCards(_cards);
                }
            }
        }

        private void PreviousCards(List<uint> cards)
        {
            if (_page > 0)
                _page--;
            int i = 0;
            for (int j = 10 * _page; j < cards.Count; j++)
            {
                var card = cards[j];
                if (i < 10)
                {
                    _cardsPrefab[i].gameObject.SetActive(true);
                    _cardsPrefab[i].ReDraw(card);
                    i++;
                }
                else
                    break;
            }
        }
    }
}
