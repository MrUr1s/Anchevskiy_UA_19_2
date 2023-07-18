using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cards.UIMenu
{
    public abstract class UIButton:MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        protected uint _idCard;
        public uint IdCard => _idCard;
        [SerializeField]
        protected TMP_Text _textPlayer;
        [SerializeField]
        protected TMP_Text _countCard;
        [SerializeField]
        protected RawImage _image;
        [SerializeField]
        protected UICard _uICard;
        [SerializeField]
        protected int _index;

        protected void Awake()
        {
            _textPlayer = GetComponentInChildren<UITextPlayer>().GetComponent<TMP_Text>();
            _countCard = GetComponentInChildren<UICountCard>().GetComponent<TMP_Text>();
            _image = GetComponentInChildren<RawImage>();
            GetComponent<Button>().onClick.AddListener(OnClick);
        }
        protected void Start()
        {
            _uICard = UIMenu.Instance.UICard;
        }
        protected abstract void OnClick(); 
        public abstract void Redraw();
        public void OnPointerEnter(PointerEventData eventData)
        {
            _uICard.gameObject.SetActive(true);
            _uICard.Set(Managers.ManagerCard.Instance.Cards.FirstOrDefault(t => t.Id == IdCard));
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            _uICard.gameObject.SetActive(false);
        }
        public void Set(uint idCard, int Index = 0)
        {
            _index = Index;
            _idCard = idCard;
        }
    }
}