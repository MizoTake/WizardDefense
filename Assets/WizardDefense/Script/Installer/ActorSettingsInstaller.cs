using UnityEngine;
using Zenject;

namespace WizardDefense
{
    [CreateAssetMenu (fileName = "ActorSettingsInstaller", menuName = "Installers/ActorSettingsInstaller")]
    public class ActorSettingsInstaller : ScriptableObjectInstaller<ActorSettingsInstaller>
    {
        public SoldierSettings soldierParam;
        public override void InstallBindings ()
        {
            Container.BindInstance (soldierParam);
        }
    }
}