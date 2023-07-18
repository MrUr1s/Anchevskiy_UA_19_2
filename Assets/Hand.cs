using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cards
{

    [RequireComponent(typeof(SortingComponent))]
    public class Hand : MonoBehaviour
    {

        [SerializeField]
        private TypePlayer _typePlayer;
        [SerializeField]
        private Player _player;

        [SerializeField]
        private List<CardSetting> _listCard;

        [SerializeField]
        private float _scaleSize = 1.5f;
        [SerializeField]

        private Transform _pos;

        [SerializeField]
        private float _speed = 0.5f;

        public TypePlayer TypePlayer => _typePlayer;

        public List<CardSetting> ListCard=> _listCard;

        public Player Player  => _player;

        private void Awake()
        {
            _player = Managers.PlayerManager.Instance.Players.First(t => t.PlayerType == _typePlayer);
        }

        public void ShowCardsInfo(bool value) => _listCard.ForEach(t => t.ShowCardInfo(value));

        private void Card_OnExitCard(CardSetting cardSetting)
        {
            cardSetting.transform.localScale = cardSetting.DefaultScale;
        }

        private void Card_OnEnterCard(CardSetting cardSetting)
        {
            cardSetting.transform.localScale *= _scaleSize;
        }

        [ContextMenu("AddCard")]
        public void AddCard()
        {
            if (_player.TakeCard(out var id))
            {
                CardSetting card;
                if (Managers.ManagerCard.Instance.PoolCards.Any(t => !t.gameObject.activeSelf))
                {
                    card = Managers.ManagerCard.Instance.PoolCards.First(t => !t.gameObject.activeSelf);
                    card.gameObject.SetActive(true);
                    card.SetTypePlayer(_typePlayer);
                    card.OnEnterCard += Card_OnEnterCard;
                    card.OnExitCard += Card_OnExitCard;
                    card.ReDraw(id);
                }
                else
                {
                    card = Instantiate(Managers.ManagerCard.Instance.CardSetting);
                    Managers.ManagerCard.Instance.PoolCards.Add(card);
                    card.SetTypePlayer(_typePlayer);
                    card.OnEnterCard += Card_OnEnterCard;
                    card.OnExitCard += Card_OnExitCard;
                    card.ReDraw(id);
                }
                _listCard.Add(card);
                card.transform.position = _pos.position;

                card.transform.localRotation = _pos.localRotation;
                if(!card.TryGetComponent(out DragOnDropComponent dragOnDropComponent))
                    card.gameObject.AddComponent<DragOnDropComponent>();
                if (!card.TryGetComponent(out AttackedCard attackedCard))
                    card.gameObject.AddComponent<AttackedCard>();
                StartCoroutine(MovingCard(card.transform, transform.position));
            }
        }

        private IEnumerator MovingCard(Transform cur, Vector3 target)
        {
            while (Vector3.Distance(cur.position, target) >= 0.1f)
            {
                cur.position = Vector3.MoveTowards(cur.position, target, _speed);

                yield return null;
            }
            cur.SetParent(transform, true);
            GetComponent<SortingComponent>().SortingCard();
        }


    }
} 