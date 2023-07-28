using OneLine;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Cards.ScriptableObjects
{
	[CreateAssetMenu(fileName = "NewCardPackConfiguration", menuName = "CardConfigs/Card Pack Configuration")]
	public class CardPackConfiguration : ScriptableObject
	{
		[SerializeField]
		private bool _isConstruct;

		[SerializeField]
		private SideType _sideType;
		[SerializeField]
		private ushort _cost;
		[SerializeField, OneLine(Header = LineHeader.Short)]
		private CardPropertiesData[] _cards;

        public CardPropertiesData[] Cards => _cards;

        public IEnumerable<CardPropertiesData> UnionProperties(IEnumerable<CardPropertiesData> array)
		{
			TryToContruct();

			return array.Union(Cards);
		}

		[ContextMenu("TryToContruct")]
		private void TryToContruct()
		{
			if (_isConstruct) return;

			for(int i = 0; i < Cards.Length; i++)
			{
				Cards[i].Cost = _cost;
				Cards[i].Description = CardUtility.GetDescriptionById(Cards[i].Id);
                Cards[i].IsBattlecry = CardUtility.GetBattlecry(Cards[i].Id);
                Cards[i].IsCharge = CardUtility.GetCharge(Cards[i].Id);
                Cards[i].IsTaunt = CardUtility.GetTaunt(Cards[i].Id);
            }

			_isConstruct = true;
		}
	}
}