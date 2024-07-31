using System;
using TEngine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameLogic
{
    public class AMapItem : ObjectBase, IEntity, IMapItem, IUnit
    {
        public GameObject Obj { get; set; }
        public Transform TF { get; set; }
        public virtual EMapItemType MapItemType { get; }

        protected override void Release(bool isShutdown)
        {
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            Obj ??= (Target as GameObject);
            TF  ??= (Target as GameObject).transform;
        }
    }
}