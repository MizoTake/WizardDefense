using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WizardDefense
{
	public class Castle : MonoBehaviour, ISortie
	{

		[SerializeField]
		private GameObject[] _sortieObjects;
		[SerializeField]
		private Transform _instancePoint;

		public void Sortie (Formation data, int index, Platoon platoon, Vector3? leaderPosition = null)
		{
			var soldier = Instantiate (_sortieObjects.RandomValue (), _instancePoint.position, Quaternion.identity);
			platoon.AddMember (soldier.transform, index, leaderPosition);
			soldier.GetComponent<IFormationable> ().Formation (data, index, platoon);
		}
	}
}