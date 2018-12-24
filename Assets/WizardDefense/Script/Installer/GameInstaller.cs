using UnityEngine;
using Zenject;

namespace WizardDefense
{
    public class GameInstaller : MonoInstaller
    {

        public GameObject _soldierPrefab;

        public override void InstallBindings ()
        {
            Container.BindInterfacesTo<SoldierSpawner> ().AsSingle ();
            Container.Bind<Soldier.Factory> ().AsSingle ().WithArguments (_soldierPrefab);
        }
    }
}