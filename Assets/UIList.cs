using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.UIMenu
{
    public abstract class UIList : MonoBehaviour
    {
        [SerializeField]
        protected UIButton _prefab;
        [SerializeField]
        protected RectTransform _content;
        [SerializeField]
        protected List<UIButton> _list = new();

        public List<UIButton> List => _list; 

        protected void Awake()
        {
            _content = GetComponentInChildren<Content>().GetComponent<RectTransform>();
            SetPrefab();
            SetList();

        }

        public void Clear()
        {
            List.ForEach(t => t.gameObject.SetActive(false));
        }

        protected abstract void SetList();

        protected abstract void SetPrefab();
        public abstract void ResetList();
    }
}