using UnityEngine;
using Utilities;

namespace Compose
{
    public class Dul
    {
        
    }

    public class MonoDul : MonoBehaviour
    {
        
    }
    public class ComposeManager : MonoSingleton<ComposeManager>
    {
        protected override void Awake()
        {
            base.Awake();
            
            DontDestroyOnLoad(gameObject);
        }
    }
}