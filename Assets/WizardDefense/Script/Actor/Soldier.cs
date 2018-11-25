using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace WizardDefense
{
	public class Soldier : MonoBehaviour, IParametable, IFormationable
	{
		[SerializeField]
		private SoldierSettings _parameter;

		private NavMeshAgent _agent;
		private Vector3 _releativePos;
		private Transform _trackingLeader = null;
		private Vector3 _nextPosition;

		public SoldierSettings Parameter
		{
			get
			{
				return _parameter;
			}
		}

		public void Formation (Formation data, int index, Platoon platoon)
		{
			_releativePos = data.RelativePosition (index);
			if (index == 0)
			{
				_nextPosition = platoon.NeutoralPosition;
				transform.GetChild (0).GetComponent<MeshRenderer> ().material.color = Color.red;
			}
			else
			{
				_trackingLeader = platoon.Member[0];
				_nextPosition = _trackingLeader.position;
			}
		}

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
	}
}