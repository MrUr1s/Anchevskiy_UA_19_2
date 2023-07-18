using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Cards.UIMenu
{
    public class UIMenu : MonoBehaviour
    {
        public static UIMenu Instance { get; private set; }
        public UIPlayerList PlayerList => _playerList;

        public UICardList CartList => _cartList;

        public UICard UICard => _uICard;

        public UIPage UIPage => _uIPage;

        public Button UIPlayButton  => _uIPlayButton;

        [SerializeField]
        private UIPlayerList _playerList;
        [SerializeField]
        private UICardList _cartList;

        [SerializeField]
        private UICard _uICard;
        [SerializeField]
        private UIPage _uIPage;
        [SerializeField]
        private Button _uIPlayButton;
        private void Awake()
        {
            Instance = this;
            _playerList = GetComponentInChildren<UIPlayerList>();
            _cartList = GetComponentInChildren<UICardList>();
            _uICard = GetComponentInChildren<UICard>();
            _uIPage= GetComponentInChildren<UIPage>();
            _uIPlayButton = GetComponentInChildren<UIPlayButton>().GetComponent<Button>();
            _uIPlayButton.GetComponent<Button>().onClick.AddListener(PlayButtonOnClick);

        }
        private void PlayButtonOnClick()
        {
            SceneManager.LoadScene("GameScene");
        }

        private void Start()
        {
            _playerList.ResetList();
            _uIPlayButton.gameObject.SetActive(true);
            _playerList.gameObject.SetActive(true);
            _uICard.gameObject.SetActive(false);
            _cartList.gameObject.SetActive(false);
            _uIPage.gameObject.SetActive(false);
        }
    }
}