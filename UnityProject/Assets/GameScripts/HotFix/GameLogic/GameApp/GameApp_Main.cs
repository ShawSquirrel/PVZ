using System.IO;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        await UniTask.WaitUntil(() => GameModule.UI.IsAnyLoading() == false);
        await HideSplash();
    }

    public async UniTask ShowSplash()
    {
        GameModule.UI.ShowUIAsync<UI_LoadSplash>();

        await UniTask.WaitUntil(() => GameModule.UI.IsAnyLoading() == false);
        bool isComplete = false;
        DOTween.To(() => 0f, value => GameEvent.Send(UIEvent.SetLoadSplash, value), 1f, 1f)
           .OnComplete(() => isComplete = true);
        await UniTask.WaitUntil(() => isComplete);
    }

    public async UniTask HideSplash()
    {
        GameModule.UI.ShowUIAsync<UI_LoadSplash>();

        await UniTask.WaitUntil(() => GameModule.UI.IsAnyLoading() == false);
        bool isComplete = false;
        DOTween.To(() => 1f, value => GameEvent.Send(UIEvent.SetLoadSplash, value), 0f, 1f)
           .OnComplete(() => isComplete = true);
        await UniTask.WaitUntil(() => isComplete);
        GameModule.UI.CloseUI<UI_LoadSplash>();
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

    public async UniTask StartGame()
    {
        await ShowSplash();
        GameModule.UI.ShowUI<UI_SelectLevel>();
        await HideSplash();
    }

    public void LoadLevel(int level = 0)
    {
        GameModule.UI.CloseAll();
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