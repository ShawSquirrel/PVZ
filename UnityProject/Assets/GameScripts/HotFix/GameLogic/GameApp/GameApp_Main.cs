using System.IO;
using Cysharp.Threading.Tasks;
using GameLogic;
using TEngine;
using UnityEngine;

public partial class GameApp
{
    public GameData GameData;

    private void AddLogicSystemMain()
    {
    }

    private void AddListener()
    {
        Utility.Unity.AddUpdateListener(Main_Update);
        Utility.Unity.AddDestroyListener(Main_Destroy);
    }


    private async UniTask StartMain()
    {
        AddListener();
        GameData = LoadConfig();
        new GameObject("GameRoot").AddComponent<GameRoot>().GameApp = this;
        await GameInit();

        GameModule.UI.ShowUIAsync<UI_Main>();
        await UniTask.CompletedTask;
    }

    private GameData LoadConfig()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "GameData.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game loaded from " + filePath);
            return data;
        }

        return new GameData();
    }

    public void SaveConfig(GameData gameData)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "GameData.json");
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(filePath, json);
        Log.Info("Game saved to " + filePath);
    }

    private async UniTask GameInit()
    {
        await GameModule.Localization.LoadLanguage("Localization");
        GameModule.Localization.Language = Language.ChineseSimplified;
        await UniTask.CompletedTask;
    }

    public void StartGame()
    {
        GameModule.UI.ShowUI<UI_SelectLevel>();
        // Battle.Instance.Active();
    }

    public void LoadLevel(int level = 0)
    {
        Battle.Instance.Active();
        Battle.Instance.Init(level);
    }


    private void Main_Update()
    {
    }

    private void Main_Destroy()
    {
        SaveConfig(GameData);
    }
}