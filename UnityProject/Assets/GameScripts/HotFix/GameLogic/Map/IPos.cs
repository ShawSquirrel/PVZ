using UnityEngine;

namespace GameLogic
{
    public interface IPos
    {
        public Vector3 Position { get; set; }
        public Vector3 GetPos();
        public void SetPos(Vector3 pos);
    }
}