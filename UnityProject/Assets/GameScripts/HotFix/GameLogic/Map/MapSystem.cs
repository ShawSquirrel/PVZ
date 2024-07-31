using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class MapSystem
    {
        public Dictionary<Vector2Int, MapData> MapDataDict = new Dictionary<Vector2Int, MapData>();

        public async UniTask MapGenerate(int[,] matrix)
        {
            Transform parent = new GameObject("MapRoot").transform;
            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector2Int index = new Vector2Int(i, j);

                    if (MapDataDict.TryGetValue(index, out MapData mapData) == false)
                    {
                        mapData = new MapData();
                        MapDataDict.Add(index, mapData);
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

                    mapItem.TF.position = new Vector3(-width / 2f + i, height / 2f - j);
                    mapItem.TF.name     = index.ToString();
                    mapItem.TF.SetParent(parent);
                    mapItem.Obj.AddComponent<MapItemMouseEvent>().MapItemIndex = index;
                }
            }
        }
    }
}