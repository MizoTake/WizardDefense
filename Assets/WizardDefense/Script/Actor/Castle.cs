using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace WizardDefense
{
	public partial class Castle : MonoBehaviour
	{

		[Inject]
		private SoldierSpawner _spawner;

		[SerializeField]
		private GameObject[] _sortieObjects;
		[SerializeField]
		private Transform _instancePoint;

	}

	public partial class Castle : ISortie
	{
		public void Sortie (Formation data, int index, Platoon platoon, Vector3? leaderPosition = null)
		{
			// var soldier = Instantiate (_sortieObjects.RandomValue (), _instancePoint.position, Quaternion.identity);
			var soldier = _spawner.Instantiate ();
			platoon.AddMember (soldier.transform, index, leaderPosition);
			soldier.GetComponent<IFormationable> ().Formation (data, index, platoon);
		}
	}
}