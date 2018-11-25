using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardDefense
{
	public interface ISortie
	{
		void Sortie (Formation data, int index, Platoon platoon, Vector3? leaderPosition = null);
	}
}