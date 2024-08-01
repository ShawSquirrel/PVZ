using UnityEngine;

namespace GameLogic
{
    public interface IUnit
    {
        public GameObject _Obj { get; set; }
        public Transform _TF { get; set; }
    }
}