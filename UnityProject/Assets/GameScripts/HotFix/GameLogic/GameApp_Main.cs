using Cysharp.Threading.Tasks;
using GameLogic;
using TEngine;
using UnityEngine;

public partial class GameApp
{
    public Battle Battle;

    private void AddLogicSystemMain()
    {
    }

    private async UniTask StartMain()
    {
        new GameObject("GameRoot").AddComponent<GameRoot>().GameApp = this;
        await GameInit();

        GameModule.UI.ShowUIAsync<UI_Main>();
        await UniTask.CompletedTask;
    }

    private async UniTask GameInit()
    {
        await GameModule.Localization.LoadLanguage("Localization");
        GameModule.Localization.Language = Language.ChineseSimplified;
        await UniTask.CompletedTask;
    }

    public void StartGame()
    {
        Battle.Instance.Active();
    }

    public void SelectPrincess(EPrincessType princessType)
    {
    }

    private void Main_Update()
    {
    }
}