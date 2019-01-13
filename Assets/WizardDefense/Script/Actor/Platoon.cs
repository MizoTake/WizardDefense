using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WizardDefense
{
	public class Platoon
	{
		public List<Transform> Member { get; private set; } = new List<Transform> ();
		public Formation Formation { get; private set; }
		public int Index { get; private set; }
		public Vector3 NeutoralPosition { get; private set; }
		public Vector3 LeaderPosition
		{
			get
			{
				var result = Member.Where (_ => _ != null).First ();
				return result.position;
			}
		}

		public Platoon (Formation formation, int index)
		{
			this.Formation = formation;
			this.Index = index;
		}

		public void AddMember (Transform newMember, int index, Vector3? nuetoralPosition)
		{
			Member.Add (newMember);
			if (index == 0)
			{
				this.NeutoralPosition = nuetoralPosition.Value;
			}
		}

		public void RemoveMember (Transform member)
		{
			if (Member.IndexOf (member) == 0)
			{
				this.NeutoralPosition = member.transform.position;
			}
			Member.Remove (member);
		}
	}
}