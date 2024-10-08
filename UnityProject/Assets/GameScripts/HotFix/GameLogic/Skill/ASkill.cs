﻿using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class ASkill : ObjectBaseWithInstance, IUnit, IEntity, ISkill, IMove, IAttribute
    {
        public Rigidbody _Rigid { get; set; }

        public GameObject _Obj { get; set; }
        public Transform _TF { get; set; }
        public AttributeDictionary _AttributeDict { get; set; } = new AttributeDictionary();

        protected override void EndObjectInitialize()
        {
            _Obj   = Target as GameObject;
            _TF    = _Obj.transform;
            _Rigid = _Obj.GetComponent<Rigidbody>();
        }

        protected override void Release(bool isShutdown)
        {
        }

    }
}