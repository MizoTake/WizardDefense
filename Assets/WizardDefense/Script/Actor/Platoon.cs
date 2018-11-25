using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WizardDefense
{
	public class Platoon
	{
		public Transform[] Member { get; private set; }
		public Formation Formation { get; private set; }
		public int Index { get; private set; }
		public Vector3 NeutoralPosition { get; private set; }
		public Vector3 LeaderPosition
		{
			get
			{
				return Member.First ().position;
			}
		}

		public Platoon (Formation formation, int index)
		{
			this.Formation = formation;
			this.Index = index;
			Member = new Transform[formation.Point.Length];
		}

		public void AddMember (Transform newMember, int index, Vector3? nuetoralPosition)
		{
			Member[index] = newMember;
			if (index == 0)
			{
				this.NeutoralPosition = nuetoralPosition.Value;
			}
		}
	}
}