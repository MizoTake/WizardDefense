using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardDefense
{
	public interface IFormationable
	{
		void Formation (Formation data, int index, Platoon platoon);
	}
}