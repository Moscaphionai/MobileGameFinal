using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Compose.Card
{
    public sealed class CardView : MonoDul
    {
        [SerializeField] private Image rarity;
        [SerializeField] private TMP_Text cardName;
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text cost;
        [SerializeField] private TMP_Text cardKind;

        public void Render(CardData data)
        {
            rarity.color = Constants.Rarity[(int)data.rarity];
            cardName.text = data.name;
            description.text = data.description;
            cost.text = data.cost.ToString();
            cardKind.text = data.type.ToString();
        }
    }
}
