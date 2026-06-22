using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Compose.Map
{
    public class NodeBaseView : MonoDul
    {
        [SerializeField] private TMP_Text nodeName;
        [SerializeField] private Image icon;
        [SerializeField] private Button button;

        public void Init(NodeBase node)
        {
            button.onClick.AddListener(node.Select);
        }

        public void Render(NodeBaseModel model)
        {
            nodeName.text = model.name;
            icon.sprite = model.icon;
        }
    }
}
