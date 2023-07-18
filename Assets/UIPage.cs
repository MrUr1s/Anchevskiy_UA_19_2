using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Cards.Menu;

namespace Cards.UIMenu
{
    public class UIPage : MonoBehaviour
    {
        [SerializeField]
        private Button _nextButton;
        [SerializeField]
        private Button _previosButton;

        private void Awake()
        {
            _nextButton = GetComponentInChildren<UINextButton>().GetComponent<Button>(); ;
            _previosButton = GetComponentInChildren<UIPreviosButton>().GetComponent<Button>(); ;
            _nextButton.onClick.AddListener(Next);
            _previosButton.onClick.AddListener(Previos);
        }

        private void Previos()
        {
            SelectCards.Instance.PreviousCards();
        }

        private void Next()
        {
            SelectCards.Instance.NextCards();
        }
    }
}
