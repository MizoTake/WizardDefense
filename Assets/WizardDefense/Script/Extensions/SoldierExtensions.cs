using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WizardDefense
{
    public static class SoldierExtensions
    {
        public static Soldier NearTarget (this IEnumerable<Soldier> collection, Soldier from)
        {
            Soldier result = null;
            var minDistance = 1000f;
            collection.ForEach (_ =>
            {
                var disX = Mathf.Abs (_.transform.position.x - from.transform.position.x);
                var disZ = Mathf.Abs (_.transform.position.z - from.transform.position.z);
                var distance = Mathf.Sqrt (disX * disX + disZ * disZ);
                result = (distance < minDistance && _.BelongToCastle != from.BelongToCastle) ? _ : result;
            });
            return result;
        }
    }
}