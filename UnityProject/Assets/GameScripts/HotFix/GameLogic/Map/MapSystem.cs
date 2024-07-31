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
            GameObject parent = new GameObject("MapRoot");
            GameObject mapItemPrefab = await GameModule.Resource.LoadAssetAsync<GameObject>("MapItem");
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

                    GameObject mapItem = Object.Instantiate(mapItemPrefab, parent.transform);
                    mapItem.name = $"{index}";
                    mapItem.transform.localPosition = new Vector3(-width / 2f + i, height / 2f - j);

                    switch (matrix[i, j])
                    {
                        case 0:
                            mapItem.GetComponent<MeshRenderer>().material.color = Color.gray;
                            mapData.MapItem = new MapItem_Space();
                            
                            break;
                        case 1:
                            mapItem.GetComponent<MeshRenderer>().material.color = Color.green;
                            mapData.MapItem = new MapItem_Grassland();
                            break;
                    }
                    (mapData.MapItem as IPos)?.SetPos(mapItem.transform.position);

                    mapItem.AddComponent<MapItemMouseEvent>().MapItemIndex = index;
                }
            }
        }
    }
}