using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace WizardDefense
{
	public partial class Soldier : MonoBehaviour
	{

		// [Inject]
		// private Castle _castle;

		[SerializeField]
		private SoldierSettings _parameter;

		private NavMeshAgent _agent;
		private Vector3 _releativePos;
		private Transform _trackingLeader = null;
		private Vector3 _nextPosition;

		public Castle BelongToCastle { get; set; }

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
			var target = BelongToCastle.Soldiers.NearTarget (this);
			_nextPosition = target.transform.position;
		}

		public class Factory : PlaceholderFactory<Soldier> { }
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