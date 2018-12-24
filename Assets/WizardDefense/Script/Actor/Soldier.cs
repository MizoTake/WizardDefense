using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace WizardDefense
{
	public partial class Soldier : MonoBehaviour
	{

		[Inject]
		private Soldier[] _soldiers;

		[SerializeField]
		private SoldierSettings _parameter;

		private NavMeshAgent _agent;
		private Vector3 _releativePos;
		private Transform _trackingLeader = null;
		private Vector3 _nextPosition;

		// Use this for initialization
		void Start ()
		{
			_agent = GetComponent<NavMeshAgent> ();
		}

		// Update is called once per frame
		void Update ()
		{
			// リーダーじゃない時
			if (_trackingLeader)
			{
				_agent.destination = _trackingLeader.transform.position + _releativePos;
			}
			else
			{
				_agent.destination = _nextPosition + _releativePos;
			}
		}

		public class Factory : PlaceholderFactory<int, Soldier>
		{
			private DiContainer _container;
			private Soldier[] _soldiers;

			[Inject]
			public void Construct (Soldier[] soldiers, DiContainer container)
			{
				_container = container;
				_soldiers = soldiers;
			}

			public Soldier Craete (int i)
			{
				return _container.InstantiatePrefab (_soldiers[i]).GetComponent<Soldier> ();
			}
		}
	}

	public partial class Soldier : IParametable
	{
		public SoldierSettings Parameter
		{
			get
			{
				return _parameter;
			}
		}
	}

	public partial class Soldier : IFormationable
	{
		public void Formation (Formation data, int index, Platoon platoon)
		{
			_releativePos = data.RelativePosition (index);
			if (index == 0)
			{
				_nextPosition = platoon.NeutoralPosition;
			}
			else
			{
				_trackingLeader = platoon.Member[0];
				_nextPosition = _trackingLeader.position;
			}
		}
	}
}