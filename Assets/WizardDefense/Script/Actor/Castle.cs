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

		[SerializeField, TooltipAttribute ("生成する種類を設定する配列")]
		private GameObject[] _sortieObjects;
		[SerializeField]
		private Transform _instancePoint;

		public Side Side { get; set; }

		public IReadOnlyList<Soldier> Soldiers
		{
			get
			{
				return _spawner.InstancedSoldiers;
			}
		}
	}

	public partial class Castle : ISortie
	{
		public void Sortie (Formation data, int index, Platoon platoon, Vector3? leaderPosition = null, Color? color = null)
		{
			// var soldier = Instantiate (_sortieObjects.RandomValue (), _instancePoint.position, Quaternion.identity);
			var soldier = _spawner.Instantiate ();
			if (color != null)
			{
				//TODO: Debug処理 本番では別の方法を考える
				soldier.Renderer.materials[1].color = color.Value;
			}
			soldier.BelongToCastle = this;
			platoon.AddMember (soldier.transform, index, leaderPosition);
			soldier.GetComponent<IFormationable> ().Formation (data, index, platoon);
		}
	}
}