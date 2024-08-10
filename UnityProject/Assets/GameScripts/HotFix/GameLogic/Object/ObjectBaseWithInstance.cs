using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class ObjectBaseWithInstance : ObjectBase, IGetInstance
    {
        protected override void Release(bool isShutdown)
        {
            
        }

        public virtual GameObject GetInstance()
        {
            return null;
        }
    }
}