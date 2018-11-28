using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace WizardDefense
{
	[System.Serializable]
	public struct NavAgent : IComponentData { }

	public class NavAgentComponent : ComponentDataWrapper<NavAgent> { }
}