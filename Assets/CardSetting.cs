using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

namespace Cards
{
    public class CardSetting : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public delegate void ClickCard(uint ID);
        public event ClickCard OnClickCard;
        public delegate void EnterCard(CardSetting cardSetting);
        public event EnterCard OnEnterCard;
        public delegate void ExitCard(CardSetting cardSetting);
        public event EnterCard OnExitCard;

        [SerializeField]
        private TypePlayer _typePlayer;

        [SerializeField]
        private TextMeshPro _name;
        [SerializeField]
        private TextMeshPro _description;
        [SerializeField]
        private TextMeshPro _type;
        [SerializeField]
        private TextMeshPro _cost;
        [SerializeField]
        private MeshRenderer _texture;
        [SerializeField]
        private TextMeshPro _attack;
        [SerializeField]
        private TextMeshPro _health;
        [SerializeField]
        private Canvas _backlight;
        [SerializeField]
        private CardPropertiesData _cardPropertyData;

        public CardPropertiesData CardPropertyData => _cardPropertyData;

        public TypePlayer TypePlayer => _typePlayer;

        public Vector3 DefaultScale => _defaultScale;

        public bool CanAttack => _canAttack; 

        [SerializeField]
        private bool _canAttack = false;
        [SerializeField]
        private Vector3 _defaultScale;
        [SerializeField]
        private MeshRenderer _frontCard;

        private void Awake()
        {
            _defaultScale = transform.localScale;
        }
        public void ChangeAttack(bool value)
        {
            _canAttack = value;
            _backlight.gameObject.SetActive(value);
        }

        public void ReDraw(uint ID)
        {
            _cardPropertyData = Managers.ManagerCard.Instance.AddCardConf(ID);
            _name.text = _cardPropertyData.Name;
            _description.text = _cardPropertyData.Description;
            _type.text = _cardPropertyData.Type == CardUnitType.None ? "" : _cardPropertyData.Type.ToString();
            _cost.text = _cardPropertyData.Cost == 0 ? "" : _cardPropertyData.Cost.ToString();
            _texture.material.SetTexture(_name.text, _cardPropertyData.Texture);
            _texture.material.mainTexture = _cardPropertyData.Texture;
            _attack.text = _cardPropertyData.Attack == 0 ? "" : _cardPropertyData.Attack.ToString();
            _health.text = _cardPropertyData.Health == 0 ? "" : _cardPropertyData.Health.ToString();
            _backlight.gameObject.SetActive(false);
        }

        public void ShowCardInfo(bool value) => _frontCard.enabled = _name.enabled =
            _description.enabled= _type.enabled=_cost.enabled=_texture.enabled = _attack.enabled = _health.enabled = value;


        public void SetTypePlayer(TypePlayer typePlayer)
        {
            _typePlayer = typePlayer;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickCard?.Invoke(CardPropertyData.Id);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnEnterCard?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnExitCard?.Invoke(this);
        }

        public void GetDamage(int value)
        {
            _cardPropertyData.Health-=value;      
            if(_cardPropertyData.Health<=0)
                gameObject.SetActive(false);
        }

        public void Refresh()
        {
            _health.text = _cardPropertyData.Health == 0 ? "" : _cardPropertyData.Health.ToString();
        }

        private void OnDisable()
        {
            Debug.Log("Die");
            if(TryGetComponent(out DragOnDropComponent dragOnDropComponent))
            {
                var defaultParent = dragOnDropComponent.DefaultParent;
                if (defaultParent != null)
                {
                    if (defaultParent.TryGetComponent(out SortingComponent sortingComponent))
                        sortingComponent.SortingCard();
                    if (defaultParent.TryGetComponent(out TableComponent tableComponent))
                        tableComponent.ListCard.Remove(this);
                }
            }

        }
    }
}
