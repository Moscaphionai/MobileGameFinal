using Compose.Map;
using UnityEngine;
using Utilities;

namespace Compose.UI
{
    public class MapPanel : BasePanel
    {
        public GameObject selections;
        
        [SerializeField] private GameObject nodeView;
        
        private void OnEnable()
        {
            Refresh();
        }
        
        private void Refresh()
        {
            selections.ClearChildren();

            foreach (var node in MapManager.Instance.CurNodes)
            {
                var go = Instantiate(nodeView, selections.transform);
                var view = go.GetComponent<MapNodeView>();
                view.Init(node);
            }
        }
    }
}
