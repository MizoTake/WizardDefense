using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UniRx;
using UniRx.Async;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace WizardDefense
{
	public partial class Soldier : MonoBehaviour
	{
		[Inject]
		private readonly SoldierSettings _masterParameter;

		[SerializeField]
		private MeshRenderer _renderer;
		[SerializeField]
		private TextMeshPro _previewHP;

		private SoldierSettings _parameter;
		private NavMeshAgent _agent;
		private Vector3 _releativePos;
		private Transform _chaseTarget = null;
		private Vector3 _nextPosition;

		public Castle BelongToCastle { get; set; }
		public MeshRenderer Renderer { get { return _renderer; } }

		// Use this for initialization
		void Start ()
		{
			_agent = GetComponent<NavMeshAgent> ();
			_parameter = Instantiate (_masterParameter);

			Bind ();
		}

		// Update is called once per frame
		void Update ()
		{
			// debug
			_previewHP.text = _parameter.HP.ToString ();
		}

		private void Bind ()
		{
			Soldier target = null;
			this.UpdateAsObservable ()
				.Select (_ => (target) ? target : BelongToCastle.Soldiers.NearTarget (from: this, searchDistance: _parameter.SearchDistance))
				.Where (x => x)
				.Subscribe (x =>
				{
					_nextPosition = x.transform.position;
					target = (x.Parameter.HP > 0) ? x : null;
					_agent.destination = (_chaseTarget) ? _chaseTarget.transform.position + _releativePos : _nextPosition + _releativePos;
				})
				.AddTo (this);

			Observable.Interval (TimeSpan.FromSeconds (_parameter.ATKInterval))
				.Where (_ => target)
				.Subscribe (_ =>
				{
					var targetDis = Vector3.Distance (transform.position, target.transform.position);
					if (targetDis <= _parameter.VisibilityDistance)
					{
						target.Damage (_parameter.ATK);
					}
				})
				.AddTo (this);
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
				BelongToCastle.Remove (this);
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
				_chaseTarget = platoon.Member.First ();
				_nextPosition = platoon.LeaderPosition;
			}
		}
	}
}