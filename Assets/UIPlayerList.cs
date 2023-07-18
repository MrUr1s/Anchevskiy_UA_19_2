using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Cards.UIMenu
{
    public class UIPlayerList : UIList
    {
        protected override void SetPrefab()
        {
            _prefab = Resources.Load<UIPlayerButton>("UIPlayerButton");
        }
        protected override void SetList()
        {           
            List.Add(Instantiate(_prefab, _content));
            List.ForEach(t => t.gameObject.SetActive(false));
        }

        public void RedrawAll()
        {
            List.ForEach(t=>t.Redraw());
        }

        public override void ResetList()
        {

            List.ForEach(t => t.gameObject.SetActive(false));
            foreach (var item in Managers.PlayerManager.Instance.Players.Select(t => t.PlayerType))
            {
                UIPlayerButton player = new();
                if (List.Any(t => !t.gameObject.activeSelf))
                {
                    player = (UIPlayerButton)List.First(t => !t.gameObject.activeSelf);
                    player.gameObject.SetActive(true);
                    player.Set(item);
                }
                else
                {
                    player = (UIPlayerButton)Instantiate(_prefab, _content);
                    player.gameObject.SetActive(true);
                    player.Set(item);
                    List.Add(player);
                }
            }
        }
    }
}
