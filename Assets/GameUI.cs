using Cards.UIMenu;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cards.UIGame
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField]
        private UIMana _uIMana;
        [SerializeField]
        private UICard _uICard;

        public UICard UICard  => _uICard; 

        private void Awake()
        {
            _uIMana=GetComponentInChildren<UIMana>();
            _uICard = GetComponentInChildren<UICard>();
        }
        private void Start()
        {
            _uICard.gameObject.SetActive(false);
        }

    }
}
