using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace WizardDefense
{

	class SetDestinationBarrier : BarrierSystem { }
	class PathSuccessBarrier : BarrierSystem { }
	class PathErrorBarrier : BarrierSystem { }

	[DisableAutoCreation]
	public class NavAgentSystem : JobComponentSystem
	{
		public struct AgentData
		{
			
		}
	}
}