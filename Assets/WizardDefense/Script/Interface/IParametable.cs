using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardDefense
{
	public interface IParametable
	{

		[SerializeField]
		SoldierSettings Parameter { get; }
	}
}