using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cards
{
    [Serializable]
    public class Player 
    {
        [SerializeField]
        private TypePlayer _playerType;
        [SerializeField]
        private uint _heroID;
        [SerializeField]
        private List<uint> _poolCardsID;
        [SerializeField]
        private int _countCards = 10;

        [SerializeField]
        private int _manaCount = 0, _maxManaCount = 0;
        public Player(TypePlayer playerType, uint heroID, List<uint> poolCardsID, int countCards)
        {
            _playerType = playerType;
            _heroID = heroID;
            _poolCardsID = poolCardsID;
            _countCards = countCards;
        }

        public TypePlayer PlayerType => _playerType;

        public int CountCards => _countCards;

        public List<uint> PoolCardsID => _poolCardsID;

        public uint HeroID  => _heroID;

        public int ManaCount => _manaCount;

        public int MaxManaCount  => _maxManaCount;

        public void ReloadMana()
        {
            if(_maxManaCount < 10)
                _maxManaCount++;
            _manaCount = _maxManaCount;
            
        }
        public void SetMana(int value)
        {
            if (value > 0)
            {
                if (_manaCount >= value)
                    _manaCount -= value;
                if (_manaCount < 0)
                {
                    Debug.LogError("Мана меньше нуля");
                    _manaCount = 0;
                }
            }
        }
        public void AddPoolCard(int index,uint IDCard)
        {
            _poolCardsID[index] = IDCard;
        }
        public void AddHeroCard(uint IDCard)
        {
            _heroID=IDCard;
        }
        public bool TakeCard(out uint id)
        {
            id = 0;
            if (_poolCardsID.Count<=0) return false;
            id = _poolCardsID[0];
            _poolCardsID.RemoveAt(0);
            return true;
        }
       

    }
}
