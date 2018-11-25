﻿using System;
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

		[Inject]
		private Castle _castle;

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
			AddPlatoon (_formations.RandomValue (), _platoons.Count);
			_current = _formations.RandomValue ();
			_currentFormationIndex = 0;

			Bind ();
		}

		private void Bind ()
		{
			Observable
				.Interval (TimeSpan.FromSeconds (_sortieInterval))
				.Subscribe (_ =>
				{
					Debug.Log (_current.Point.Length + " " + _currentFormationIndex);
					if (_current.Point.Length == _currentFormationIndex)
					{
						AddPlatoon (_formations.RandomValue (), _platoons.Count);
						_current = _formations.RandomValue ();
						_currentFormationIndex = 0;
					}
					if (_currentFormationIndex == 0)
					{
						var pos = Vector3.zero.RandomX (-10, 10) + Vector3.zero.RandomZ (-10, 10);
						pos.y = 1f;
						_castle.Sortie (_current, _currentFormationIndex, _platoons.Last (), pos);
					}
					else
					{
						_castle.Sortie (_current, _currentFormationIndex, _platoons.Last ());
					}
					_currentFormationIndex += 1;
				})
				.AddTo (this);
		}

		// Update is called once per frame
		void Update ()
		{

		}

		private void AddPlatoon (Formation formation, int platoonsCount)
		{
			_platoons.Add (new Platoon (formation, platoonsCount));
		}
	}
}