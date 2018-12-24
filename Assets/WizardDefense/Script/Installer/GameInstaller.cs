using UnityEngine;
using Zenject;

namespace WizardDefense
{
    public class GameInstaller : MonoInstaller
    {

        public GameObject _soldierPrefab;

        public override void InstallBindings ()
        {
            Container.Bind<SoldierSpawner> ().AsSingle ();
            Container.BindFactory<Soldier, Soldier.Factory> ().FromComponentInNewPrefab (_soldierPrefab);
        }
    }
}