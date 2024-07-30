using Cysharp.Threading.Tasks;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class Map
    {
        public static async UniTask MapGenerate(int[,] matrix)
        {
            GameObject parent = new GameObject("MapRoot");
            GameObject mapItemPrefab = await GameModule.Resource.LoadAssetAsync<GameObject>("MapItem");
            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    GameObject mapItem = GameObject.Instantiate(mapItemPrefab, parent.transform);
                    mapItem.AddComponent<MapItemMouseEvent>();
                    mapItem.name                                        = $"({i},{j})";
                    mapItem.transform.localPosition                     = new Vector3(-width / 2f + i, height / 2f - j);
                    mapItem.GetComponent<MeshRenderer>().material.color = matrix[i, j] == 1 ? Color.yellow : Color.green;
                }
            }
        }
    }
}