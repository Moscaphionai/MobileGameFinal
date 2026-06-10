using UnityEngine;

namespace Utilities
{
    // ============================================================
    // 静态单例基类
    // 约定: T 必须有无参构造函数，外部不得手动 new
    // 用法: public class FooManager : Singleton<FooManager> { }
    // 子类可覆写 OnInitialize() 替代构造函数初始化逻辑
    // ============================================================
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                    _instance.OnInitialize();
                }
                return _instance;
            }
        }

        protected virtual void OnInitialize() { }
    }

    // ============================================================
    // MonoBehaviour 单例基类
    // 约定:
    //   1. 场景中预先放置唯一实例，或自行通过 AddComponent 创建
    //   2. 不得在同一场景中存在多个同类实例
    //   3. Awake 中完成 _instance 赋值
    //   4. 子类覆写 OnInitialize() / OnDeinitialize() 进行初始化和清理，
    //      无需调用 base，也无需关心 Awake/OnDestroy
    // ============================================================
    public abstract class MonoSingleton<T> : UnityEngine.MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;

        public static T Instance => _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.Log("[Singleton] Instance already exists!");
            }
            _instance = this as T;
            OnInitialize();
        }

        private void OnDestroy()
        {
            OnDeinitialize();
            if (_instance == this)
                _instance = null;
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnDeinitialize() { }
    }
}
