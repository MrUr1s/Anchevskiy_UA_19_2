using Cards;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [SerializeField]
        private TypePlayer _turnPlayer;
        [SerializeField]
        private Button _turnButton;

        public TypePlayer TurnPlayer  => _turnPlayer;

        public List<Hand> Hands  => _hands;

        public UIMana UIMana => _uIMana; 

        [SerializeField]
        private List<Hand> _hands;

        [SerializeField]
        private List<TableComponent> _tables;
        [SerializeField]
        private int _turnTime = 30;
        [SerializeField]
        private TextMeshProUGUI _timeText;
        private Coroutine _turnCoroutine;
        [SerializeField]
        private UIMana _uIMana;

        private void OnValidate()
        {
            _uIMana = FindObjectOfType<UIMana>();
            _hands = FindObjectsOfType<Hand>().ToList();
            _tables = FindObjectsOfType<TableComponent>().ToList();
        }
        private void Awake()
        {
            Instance = this;
            _turnButton.onClick.AddListener(Turn);
        }
        private void Start()
        {
            _hands.ForEach(hand =>
            {
                hand.AddCard();
                hand.AddCard();
                hand.AddCard();
            });
            _hands.First(t => t.TypePlayer != _turnPlayer).ShowCardsInfo(false);
            _turnCoroutine = StartCoroutine(TurnFunc());
            var player = PlayerManager.Instance.Players.First(t => t.PlayerType == _turnPlayer);
            player.ReloadMana();
            _uIMana.SetCountMana(player.ManaCount, player.MaxManaCount);
        }

        public void Turn()
        {
            StopCoroutine(_turnCoroutine);
            int angleY = 0;
            if (_turnPlayer == TypePlayer.Player1)
            {
                _turnPlayer = TypePlayer.Player2; 
                angleY = 180;                
            }
            else 
            { 
                _turnPlayer = TypePlayer.Player1; 
            }

            Camera.main.transform.eulerAngles = new Vector3(90, angleY, 0);
            _hands.First(t => t.TypePlayer != _turnPlayer).ShowCardsInfo(false);
            _hands.First(t => t.TypePlayer == _turnPlayer).ShowCardsInfo(true);
            _hands.First(t => t.TypePlayer == _turnPlayer).AddCard();
            _tables.ForEach(t => {
                t.ListCard.ForEach(card => card.ChangeAttack(t.TypePlayer == _turnPlayer));
                    t.transform.eulerAngles = new Vector3(0, angleY, 0);

            });
            var player = PlayerManager.Instance.Players.First(t => t.PlayerType == _turnPlayer);
            player.ReloadMana();
            _uIMana.SetCountMana(player.ManaCount, player.MaxManaCount);
            
            _turnCoroutine = StartCoroutine(TurnFunc());
        }

        private IEnumerator TurnFunc()
        {
            var time = _turnTime;
            while (time-- > 0)
            {
                _timeText.text=time.ToString();
                yield return new WaitForSeconds(1f);
            }
            Turn();
        }


        public void DestroyCard()
        {
            _tables.ForEach(t =>
            { 
                
                
                });
        }
    }
}
