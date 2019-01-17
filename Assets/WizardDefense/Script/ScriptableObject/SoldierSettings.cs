using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardDefense
{
	public class SoldierSettings : ScriptableObject
	{
		public int HP;
		public int ATK;
		public float ATKInterval;
		public float SearchDistance;
		public float VisibilityDistance;
		public SearchType Type;

		public enum SearchType
		{
			NEAR,
			FAR
		}
	}
}