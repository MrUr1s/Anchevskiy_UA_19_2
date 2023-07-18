
using UnityEngine;

namespace Cards
{
    public class SortingComponent : MonoBehaviour
    {
        [SerializeField]
        private float _dX = 8;
        [ContextMenu("SortingCard")]
        public void SortingCard()
        {
            var SortComponent = GetComponentsInChildren<DragOnDropComponent>();

            for (int i = 0; i < SortComponent.Length; i++)
            {
                SortComponent[i].transform.localPosition = new Vector3((i * _dX) - ((SortComponent.Length - 1) * _dX) / 2, 1, 0);
                SortComponent[i].transform.localRotation = Quaternion.identity;
            }
        }
    }
}