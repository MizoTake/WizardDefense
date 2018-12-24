using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WizardDefense
{
    public class SoldierSpawner : ITickable
    {
        readonly Soldier.Factory _soldierFactory;
        private int _soldierCnt = 0;

        public SoldierSpawner (Soldier.Factory soldierFactory)
        {
            _soldierFactory = soldierFactory;
        }

        public void Tick ()
        {
            // TODO: 生成フラグ
            // if (ShouldSpawnNewEnemy ())
            {
                _soldierFactory.Create (_soldierCnt);
                _soldierCnt += 1;
            }
        }
    }
}