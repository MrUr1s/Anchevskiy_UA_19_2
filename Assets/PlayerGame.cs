using Cards.UIGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Cards.Game
{
    public class PlayerGame : MonoBehaviour
    {
        [SerializeField]
        private TypePlayer playerType;
        [SerializeField]
        private GameUI _gameUI;
        [SerializeField]
        private Player player;

        public Player Player { 
            get
            {             
                    player = Managers.PlayerManager.Instance.Players.Find(t => t.PlayerType == playerType);
                return player;
             } 
        }

        public GameUI GameUI => _gameUI; 

        private void Awake()
        {
            _gameUI = GetComponentInChildren<GameUI>();

        }
        private void Start()
        {
        }
    }
}