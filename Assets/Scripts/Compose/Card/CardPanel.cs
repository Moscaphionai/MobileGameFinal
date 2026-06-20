using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Compose.Card
{
    public sealed class CardPanel : MonoDul
    {
        [SerializeField] private Image rarity;
        [SerializeField] private TMP_Text cardName;
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text cost;
        [SerializeField] private TMP_Text cardKind;

        public void Render(CardData data)
        {
            rarity.color = Constants.Rarity[(int)data.Rarity];
            cardName.text = data.Name;
            description.text = data.Description;
            cost.text = data.Cost.ToString();
            cardKind.text = data.Type.ToString();
        }
    }
}
