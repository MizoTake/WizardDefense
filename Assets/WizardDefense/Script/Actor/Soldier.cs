using System.Collections;
using System.Collections.Generic;
using TMPro;
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
		private SoldierSettings _parameter; // readonly?

		[SerializeField]
		private MeshRenderer _renderer;
		[SerializeField]
		private TextMeshPro _previewHP;

		private NavMeshAgent _agent;
		private Vector3 _releativePos;
		private Transform _trackingLeader = null;
		private Vector3 _nextPosition;

		public Castle BelongToCastle { get; set; }
		public MeshRenderer Renderer { get { return _renderer; } }

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
			var target = BelongToCastle.Soldiers.NearTarget (from: this);
			if (target)
			{
				_nextPosition = target.transform.position;
				var targetDis = Vector3.Distance (transform.position, target.transform.position);
				if (targetDis <= 100)
				{
					target.Damage (_parameter.ATK);
				}
				Debug.Log (targetDis);
			}

			// debug
			_previewHP.text = _parameter.HP.ToString ();
		}

		public class Factory : PlaceholderFactory<Soldier> { }
	}

	public partial class Soldier : IDamageable
	{
		public void Damage (int value)
		{
			_parameter.HP -= value;
			if (_parameter.HP <= 0)
			{
				Destroy (this.gameObject);
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