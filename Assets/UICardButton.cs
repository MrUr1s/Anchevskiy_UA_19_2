using Cards.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Cards.UIMenu {
    public class UICardButton : UIButton
    {
        [SerializeField]
        private TypePlayer _typePlayer;
        [SerializeField]
        private Image _imagebutton;

        [SerializeField]
        private CardUnitType _cardUnitType;
        [SerializeField]
        private Color _colorDefault=new(255,255,255);
        protected new void Awake()
        {
            base.Awake();
            _imagebutton=GetComponent<Image>();
        }

        public void HeroCard()
        {
            _colorDefault = new Color(0, 255, 255);
            _imagebutton.color = _colorDefault;
        }
        public void Set(uint idCard,TypePlayer typePlayer, int Index = 0)
        {
            _typePlayer = typePlayer;
            Set(idCard, Index);
        }
        public new void Set(uint idCard,int Index=0)
        {
            base.Set(idCard,Index);
            Redraw();
        }
        public override void Redraw()
        {
            var card = Managers.ManagerCard.Instance.Cards.Find(t => t.Id == _idCard);
            _textPlayer.text = card.Name;
            _countCard.text = card.Cost == 0 ? "" : card.Cost.ToString();
            _image.texture = card.Texture;
            _cardUnitType = card.Type;
        }

        protected override void OnClick()
        {
            UIMenu.Instance.CartList.List.ForEach(t => ((UICardButton)t).Selected(false));
            Selected(true);
            SelectCards.Instance.VisibleCards(IsHeroCards: _cardUnitType == CardUnitType.Hero);
            SelectCards.Instance.SetIndexCard(_index);
            SelectCards.Instance.SetPlayerType(_typePlayer);
        }

        public void Selected(bool selected=true)
        {
            if (selected)
                _imagebutton.color = new Color(0, 255, 0);
            else
                _imagebutton.color = _colorDefault;

        }


    }
}
