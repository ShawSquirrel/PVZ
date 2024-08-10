using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class AMapItem : ObjectBaseWithInstance, IEntity, IMapItem, IUnit, IPlanted
    {
        public GameObject _Obj { get; set; }
        public Transform _TF { get; set; }
        public virtual EMapItemType MapItemType { get; }
        public virtual bool Planted()
        {
            return false;
        }

        protected override void Release(bool isShutdown)
        {
            if (_Obj != null)
            {
                Object.Destroy(_Obj);
            }
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            _Obj.SetActiveSelf(true);
        }

        protected override void OnUnSpawn()
        {
            base.OnUnSpawn();
            _Obj.SetActiveSelf(false);
        }

        protected override void EndObjectInitialize()
        {
            _Obj ??= Target as GameObject;
            _TF  ??= _Obj.transform;
            MapItemInitialize();
        }

        protected virtual void MapItemInitialize()
        {
            
        }

        protected void SetColor(Color color)
        {
            _Obj.GetComponent<SpriteRenderer>().color = color;
        }

        public override GameObject GetInstance()
        {
            GameObject target = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>("MapItem"));
            return target;
        }
    }
}