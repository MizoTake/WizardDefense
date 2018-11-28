using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardDefense
{
	public interface IControllable
	{
		bool MouseClick ();
	}

	public static class IControllableExtensions
	{
		public static bool MouseClick (this IControllable controller)
		{
			return true;
		}
	}
}