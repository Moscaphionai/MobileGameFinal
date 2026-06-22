using System;
using System.Collections.Generic;
using Compose.UI;
using Utilities;

namespace Compose
{
    public sealed class UIManager : MonoSingleton<UIManager>
    {
        private readonly Dictionary<Type, BasePanel> panels = new();

        protected override void Awake()
        {
            base.Awake();

            foreach (var panel in GetComponentsInChildren<BasePanel>(true))
            {
                Register(panel);

                if (panel.ShowOnStart)
                    panel.Show(true);
                else
                    panel.Hide(true);
            }
        }

        public void Register(BasePanel panel)
        {
            panels[panel.GetType()] = panel;
        }

        public T Get<T>() where T : BasePanel
        {
            return panels.TryGetValue(typeof(T), out var panel) ? (T)panel : null;
        }

        public void Show<T>(bool immediately = false) where T : BasePanel
        {
            Get<T>()?.Show(immediately);
        }

        public void Hide<T>(bool immediately = false) where T : BasePanel
        {
            Get<T>()?.Hide(immediately);
        }

        public void HideAll(bool immediately = false)
        {
            foreach (var panel in panels.Values)
                panel.Hide(immediately);
        }
    }
}
