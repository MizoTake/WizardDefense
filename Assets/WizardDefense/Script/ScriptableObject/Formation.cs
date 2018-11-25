using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardDefense
{
	public class Formation : ScriptableObject
	{
		public int LeaderIndex;
		public Vector3[] Point;
	}

	public static class FormationExtensions
	{
		public static Vector3 LeaderPosition (this Formation fm)
		{
			return fm.Point[fm.LeaderIndex];
		}
		public static Vector3 RelativePosition (this Formation fm, int index)
		{
			return fm.Point[index];
		}
	}
}