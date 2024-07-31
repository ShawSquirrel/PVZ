using UnityEngine;

namespace GameLogic
{
    public interface IUnit
    {
        public GameObject Obj { get; set; }
        public Transform TF { get; set; }
    }
}