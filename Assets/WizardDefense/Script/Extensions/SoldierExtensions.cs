using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WizardDefense
{
    public static class SoldierExtensions
    {
        public static Soldier NearTarget (this IEnumerable<Soldier> collection, Soldier from, float searchDistance)
        {
            Soldier result = null;
            var minDistance = searchDistance;
            collection
                .Where (x => x.BelongToCastle.Side != from.BelongToCastle.Side && x.Parameter.HP > 0)
                .ForEach (x =>
                {
                    var disX = Mathf.Abs (x.transform.position.x - from.transform.position.x);
                    var disZ = Mathf.Abs (x.transform.position.z - from.transform.position.z);
                    var distance = Mathf.Sqrt (disX * disX + disZ * disZ);
                    result = (distance < minDistance) ? x : result;
                });
            return result;
        }
    }
}