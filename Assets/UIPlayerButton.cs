using UnityEngine;
using System.Linq;

namespace Cards.UIMenu
{
    public class UIPlayerButton : UIButton
    {
       
        [SerializeField]
        private TypePlayer _typePlayer;
     
        public void Set(TypePlayer typePlayer)
        {
            _typePlayer = typePlayer; 
            Redraw();
        }
        public override void Redraw()
        {
            var _player=Managers.PlayerManager.Instance.Players.First(t=>t.PlayerType== _typePlayer);
            Set(_player.HeroID);
            _textPlayer.text = _player.PlayerType.ToString();
            _countCard.text = _player.PoolCardsID.Count + "/" + _player.CountCards;
            _image.texture = Managers.ManagerCard.Instance.Cards.FirstOrDefault(t => t.Id == IdCard).Texture;
        }

        protected override void OnClick()
        {
            UIMenu.Instance.PlayerList.gameObject.SetActive(false);
            UIMenu.Instance.UIPage.gameObject.SetActive(true);
            var cartlist = UIMenu.Instance.CartList;
            cartlist.gameObject.SetActive(true);
            cartlist.Set(_typePlayer);
        }


    }
}
