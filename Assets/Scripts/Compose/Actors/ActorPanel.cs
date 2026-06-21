using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Compose.Actors
{
    public class ActorPanel : MonoDul
    {
        [SerializeField] private TMP_Text hp;
        [SerializeField] private Slider hpBar;

        public void Render(ActorData data)
        {
            hp.text = $"{data.CurrentHp} / {data.MaxHp}";
            hpBar.maxValue = data.MaxHp;
            hpBar.value = data.CurrentHp;
        }
    }
}
