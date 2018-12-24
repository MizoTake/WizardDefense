using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WizardDefense
{
    public class SoldierSpawner
    {
        readonly Soldier.Factory _soldierFactory;
        private List<Soldier> _instancedSoldiers = new List<Soldier> ();
        public IReadOnlyList<Soldier> InstancedSoldiers { get { return _instancedSoldiers; } }

        public SoldierSpawner (Soldier.Factory soldierFactory)
        {
            _soldierFactory = soldierFactory;
        }

        public Soldier Instantiate ()
        {
            var soldier = _soldierFactory.Create ();
            _instancedSoldiers.Add (soldier);
            return soldier;
        }
    }
}