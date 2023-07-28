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
        private CardSetting _heroCard;

        [SerializeField]
        private Transform _posCardPool, _posCardHero;

        [SerializeField]
        private float _speed = 0.5f;

        public TypePlayer TypePlayer => _typePlayer;

        public List<CardSetting> ListCard=> _listCard;

        public Player Player  => _player;

        public CardSetting HeroCard => _heroCard;

        private void Awake()
        {
            _player = Managers.PlayerManager.Instance.Players.First(t => t.PlayerType == _typePlayer);
        }

        private void Start()
        {
            _heroCard = Managers.ManagerCard.Instance.SetCard(_player.HeroID, _posCardHero.position,_typePlayer);
            _heroCard.name = "Hero" + TypePlayer.ToString();
        }

       
        public void ShowCardsInfo(bool value) => _listCard.ForEach(t => t.ShowCardInfo(value));


        [ContextMenu("AddCard")]
        public bool AddCard()
        {
            if (_player.TakeCard(out var id))
            {
                var card = Managers.ManagerCard.Instance.SetCard(id, _posCardPool.position, TypePlayer);
                _listCard.Add(card);
                card.transform.position = _posCardPool.position;
                card.transform.localRotation = _posCardPool.localRotation;
                StartCoroutine(MovingCard(card.transform, transform.position));
                return true;
            }
            else
                return false;
        }

        public IEnumerator MovingCard(Transform cur, Vector3 target)
        {
            while (Vector3.Distance(cur.position, target) >= 0.1f)
            {
                cur.position = Vector3.MoveTowards(cur.position, target, _speed);
                yield return null;
            }
            cur.SetParent(transform, true);
            GetComponent<SortingComponent>().SortingCard();
        }


        public void CheckCardsForAbillity()
        {
            _listCard.ForEach(card => card.CheckAbillity(Player.ManaCount));
        }

    }
} 