
using TMPro;
using UnityEngine;

namespace Cards
{
    public class UIMana : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _countMana;

        private void OnValidate()
        {     
            _countMana=GetComponentInChildren<TextMeshProUGUI>();
        }
        public void SetCountMana(int count, int max)
        {
            _countMana.text = count.ToString() + (max >= 10 ? "" : " / "+ max.ToString());
        }
    }
}