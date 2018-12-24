using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WizardDefense
{
    public static class SoldierExtensions
    {
        public static Soldier NearTarget (this IEnumerable<Soldier> collection, Soldier target)
        {
            var result = collection.ToArray () [0];
            var minDistance = 1000f;
            collection.ForEach (_ =>
            {
                var disX = Mathf.Abs (_.transform.position.x - target.transform.position.x);
                var disZ = Mathf.Abs (_.transform.position.z - target.transform.position.z);
                var distance = Mathf.Sqrt (disX * disX + disZ * disZ);
                result = (distance < minDistance && _ != target) ? _ : result;
            });
            return result;
        }
    }
}