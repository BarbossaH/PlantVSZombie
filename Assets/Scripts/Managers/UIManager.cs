using System.Collections.Generic;

using UnityEngine;

namespace Managers
{
    public class UIManager:SingletonMono<UIManager>
    {
        [SerializeField] private GameObject[] panels; // 预加载的 UI 面板
        private readonly Dictionary<string, GameObject> panelDictionary = new Dictionary<string, GameObject>();

        protected override void Init()
        {
            foreach (var panel in panels)
            {
                panelDictionary.Add(panel.name, panel);
            }
        }

        public void ShowUIPanel(string panelName)
        {
            if (panelDictionary.TryGetValue(panelName, out var panel))
            {
                panel.SetActive(true);
            }
        }

        public void HideUIPanel(string panelName)
        {
            if (panelDictionary.TryGetValue(panelName, out var panel))
            {
                panel.SetActive(false);
            }
        }
    }
}