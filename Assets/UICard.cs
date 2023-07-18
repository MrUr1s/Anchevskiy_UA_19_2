using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cards.UIMenu
{
    public class UICard : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _name;
        [SerializeField]
        private TextMeshProUGUI _description;
        [SerializeField]
        private TextMeshProUGUI _type;
        [SerializeField]
        private TextMeshProUGUI _cost;
        [SerializeField]
        private RawImage _texture;
        [SerializeField]
        private TextMeshProUGUI _attack;
        [SerializeField]
        private TextMeshProUGUI _health;

        public void Set(CardPropertiesData cardPropertiesData)
        {
            _name.text = cardPropertiesData.Name;
            _description.text = cardPropertiesData.Description;
            _type.text = cardPropertiesData.Type == CardUnitType.None ? "" : cardPropertiesData.Type.ToString();
            _cost.text = cardPropertiesData.Cost == 0 ? "" : cardPropertiesData.Cost.ToString();
            _texture.texture = cardPropertiesData.Texture;
            _attack.text = cardPropertiesData.Attack == 0 ? "" : cardPropertiesData.Attack.ToString();
            _health.text = cardPropertiesData.Health == 0 ? "" : cardPropertiesData.Health.ToString();
        }
    }
}