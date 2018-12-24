using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WizardDefense
{
    public class SoldierSpawner
    {
        readonly Soldier.Factory _soldierFactory;

        public SoldierSpawner (Soldier.Factory soldierFactory)
        {
            _soldierFactory = soldierFactory;
        }

        public Soldier Instantiate () => _soldierFactory.Create ();
    }
}