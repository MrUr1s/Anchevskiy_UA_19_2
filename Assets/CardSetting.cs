using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

namespace Cards
{
    public class CardSetting : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public delegate void ClickCard(uint ID);
        public event ClickCard OnClickCard;
        public delegate void EnterCard(CardSetting cardSetting);
        public event EnterCard OnEnterCard;
        public delegate void ExitCard(CardSetting cardSetting);
        public event ExitCard OnExitCard;

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
        private RawImage _backlight;
        [SerializeField]
        private CardPropertiesData _cardPropertyData;

        public CardPropertiesData CardPropertyData => _cardPropertyData;

        public TypePlayer TypePlayer => _typePlayer;

        public Vector3 DefaultScale => _defaultScale;

        public bool CanAttack => _canAttack; 

        public bool IsTaunt => CardPropertyData.IsTaunt;
        
        [SerializeField]
        private bool _canAttack = false;
        [SerializeField]
        private Vector3 _defaultScale;
        [SerializeField]
        private MeshRenderer _frontCard;

        [SerializeField]
        private TypeAbilityIsTarget _typeAbility;
        public TypeAbilityIsTarget TypeAbility => _typeAbility;
        [SerializeField]
        private bool _isAbilityUsed = false;
        public bool IsAbilityUsed => _isAbilityUsed;
        public Action<TableComponent, CardSetting> Abillity { get; private set; }

        private void Awake()
        {
            _defaultScale = transform.localScale;
        }
        public void ChangeAttack(bool value)
        {
            _canAttack = value;
            CheckAbillity();
        }
        public void SetIsAbilitySelectCard(TypeAbilityIsTarget value) => _typeAbility = value;
        public void SetAbility(Action<TableComponent, CardSetting> action) => Abillity = action;

        public void OnAbilityUsed() => _isAbilityUsed = true;

        public void SetCardPropertyData(int? Attack = null, int? Health=null, int? Cost = null)
        {
            _cardPropertyData.Attack = Attack.GetValueOrDefault(_cardPropertyData.Attack);
            _cardPropertyData.Cost = Cost.GetValueOrDefault(_cardPropertyData.Cost);
            _cardPropertyData.Health = Health.GetValueOrDefault(_cardPropertyData.Health);
            Refresh();
              
            if(_cardPropertyData.Health<=0)
                gameObject.SetActive(false);
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
            _backlight.color = Color.clear;
        }

        internal void ClearEvent()
        {
            OnExitCard = null;
            OnClickCard = null;
            OnEnterCard = null;
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
            _attack.text = _cardPropertyData.Attack == 0 ? "" : _cardPropertyData.Attack.ToString();
        }

        public void CheckAbillity(int Mana=-1)
        {
            if ((CardPropertyData.Cost <= Mana || _canAttack) && TypePlayer==Managers.GameManager.Instance.TurnPlayer)
                _backlight.color = Color.green;
            else
                _backlight.color = Color.clear;
        }

        public void VisibleTarget(bool value)
        {
            _backlight.color = value? Color.red: Color.clear;
        }
        private void OnDisable()
        {
            Debug.Log("Die");
            if(TryGetComponent(out DragOnDropComponent dragOnDropComponent))
            {
                var defaultParent = dragOnDropComponent.DefaultParent;
                if (defaultParent != null)
                {
                    if (defaultParent.TryGetComponent(out TableComponent tableComponent))
                        tableComponent.ListCard.Remove(this);
                    if (defaultParent.TryGetComponent(out SortingComponent sortingComponent))
                        sortingComponent.SortingCard();
                }
            }
        } 
    

    }
}
