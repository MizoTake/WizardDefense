using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Momiji;
using UniRx;
using UnityEngine;
using Zenject;

namespace WizardDefense
{
	public class King : MonoBehaviour
	{

		[Inject (Id = "PlayerCastle")]
		private Castle _playerCastle;
		[Inject (Id = "EnemyCastle")]
		private Castle _enemyCastle;

		[SerializeField]
		private Side _side;
		[SerializeField]
		private Color _color;

		[SerializeField]
		private double _sortieInterval;
		[SerializeField]
		private Formation[] _formations;

		private Formation _current;
		private int _currentFormationIndex;
		private List<Platoon> _platoons = new List<Platoon> ();

		// Use this for initialization
		void Start ()
		{
			NextPlatoon ();

			Bind ();

			switch (_side)
			{
				case Side.Player:
					_playerCastle.Side = _side;
					break;
				case Side.Enemy:
					_enemyCastle.Side = _side;
					break;
			}
		}

		private void Bind ()
		{
			Observable
				.Interval (TimeSpan.FromSeconds (_sortieInterval))
				.Subscribe (_ =>
				{
					if (_current.Point.Length == _currentFormationIndex)
					{
						NextPlatoon ();
					}
					if (_currentFormationIndex == 0)
					{
						// TODO: 変数化
						var pos = Vector3.zero.RandomX (-10, 10) + Vector3.zero.RandomZ (-10, 10);
						pos.y = 1f;
						switch (_side)
						{
							case Side.Player:
								_playerCastle.Sortie (_current, _currentFormationIndex, _platoons.Last (), pos, color : _color);
								break;
							case Side.Enemy:
								_enemyCastle.Sortie (_current, _currentFormationIndex, _platoons.Last (), pos, color : _color);
								break;
						}
					}
					else
					{
						switch (_side)
						{
							case Side.Player:
								_playerCastle.Sortie (_current, _currentFormationIndex, _platoons.Last (), color : _color);
								break;
							case Side.Enemy:
								_enemyCastle.Sortie (_current, _currentFormationIndex, _platoons.Last (), color : _color);
								break;
						}
					}
					_currentFormationIndex += 1;
				})
				.AddTo (this);
		}

		// Update is called once per frame
		void Update ()
		{

		}

		private void NextPlatoon ()
		{
			var nextFormation = _formations.RandomValue ();
			AddPlatoon (nextFormation, _platoons.Count);
			_current = nextFormation;
			_currentFormationIndex = 0;
		}

		private void AddPlatoon (Formation formation, int platoonsCount)
		{
			_platoons.Add (new Platoon (formation, platoonsCount));
		}
	}
}