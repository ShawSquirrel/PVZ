using System;
using System.Reflection;
using TEngine;

namespace GameLogic
{
    public class ASystem : IDisposable
    {
        private bool isDispose;

        protected virtual void Awake()
        {
            Log.Info($"{GetType()} Awake");
            
            Type baseType = typeof(ASystem);
            Type derivedType = GetType();
            MethodInfo baseMethod = baseType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo derivedMethod = derivedType.GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);

            if (derivedMethod != null && derivedMethod.DeclaringType == derivedType && baseMethod != derivedMethod)
            {
                // 子类重写了方法
                Utility.Unity.AddUpdateListener(Update);
            }
        }

        protected virtual void Update()
        {
            Log.Info($"{GetType()} Update");
        }

        ~ASystem()
        {
            Log.Info($"析构函数 ~{GetType()}");
            if (isDispose == false)
            {
                isDispose = true;
                Utility.Unity.RemoveUpdateListener(Update);
            }
        }

        public void Dispose()
        {
            Log.Info($"Dispose ~{GetType()}");
            if (isDispose == false)
            {
                isDispose = true;
                Utility.Unity.RemoveUpdateListener(Update);
            }
        }


        public static T CreateSystem<T>() where T : ASystem, new()
        {
            T t = new T();
            t.Awake();
            return t;
        }
    }
}