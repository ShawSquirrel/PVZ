using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TEngine;

namespace GameLogic
{
    public class Test
    {
        public const bool isTest = false;

        public static void Start()
        {
            Log.Debug($"---------------Test---------------");
            CancellationTokenSource ctk = new CancellationTokenSource();
            GameModule.Timer.AddTimer((value) => ctk.Cancel(), 2f);
            LongRunningTaskAsync(ctk).Forget();
        }

        private static async UniTask LongRunningTaskAsync(CancellationTokenSource ctk)
        {
            try
            {
                Log.Info("LongRunningTaskAsync task...");
                await LongRunningTaskAsync1(ctk);
                Log.Info("LongRunningTaskAsync task completed.");
            }
            catch (OperationCanceledException)
            {
                Log.Info("LongRunningTaskAsync task was cancelled.");
            }
        }
        private static async UniTask LongRunningTaskAsync1(CancellationTokenSource ctk)
        {
            try
            {
                Log.Info("LongRunningTaskAsync1 running task...");
                await UniTask.Delay(4000, cancellationToken: ctk.Token);
                Log.Info("LongRunningTaskAsync1 task completed.");
            }
            catch (OperationCanceledException)
            {
                Log.Info("LongRunningTaskAsync1 task was cancelled.");
                throw;
            }
        }
    }
}