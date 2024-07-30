using Cysharp.Threading.Tasks;
using TEngine;

public partial class GameApp
{
    private void AddLogicSystemMain()
    {
    }

    private async UniTask StartMain()
    {
        GameModule.Timer.AddTimer(On3Second, 3f, true);
        await UniTask.CompletedTask;
    }

    private void On3Second(object[] args)
    {
        Log.Info("On3Second");
    }
}