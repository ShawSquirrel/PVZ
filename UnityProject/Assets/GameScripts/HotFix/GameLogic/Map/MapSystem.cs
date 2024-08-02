using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class MapSystem
    {
        public Transform _parent;
        public Dictionary<Vector2Int, MapData> _mapDataDict = new Dictionary<Vector2Int, MapData>();

        public async UniTask MapGenerate(int[,] matrix)
        {
            if (_parent == null)
            {
                _parent      = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>("Map")).transform;
                _parent.name = "Map";

                _parent = _parent.Find("MapRoot");
            }


            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector2Int index = new Vector2Int(i, j);

                    if (_mapDataDict.TryGetValue(index, out MapData mapData) == false)
                    {
                        mapData = new MapData();
                        _mapDataDict.Add(index, mapData);
                    }

                    AMapItem mapItem;

                    switch (matrix[i, j])
                    {
                        case 0:
                            mapItem = PoolHelper.Spawn<MapItem_Space>();
                            break;
                        case 1:
                            mapItem = PoolHelper.Spawn<MapItem_Grassland>();
                            break;
                        default:
                            mapItem = PoolHelper.Spawn<MapItem_Space>();
                            break;
                    }

                    mapData._MapItem = mapItem;
                    mapItem._TF.SetParent(_parent, false);
                    mapItem._TF.localPosition                                   = new Vector3(i, -j);
                    mapItem._TF.name                                            = index.ToString();
                    mapItem._Obj.AddComponent<MapItemMouseEvent>().MapItemIndex = index;
                }
            }

            await UniTask.CompletedTask;
        }
    }
}