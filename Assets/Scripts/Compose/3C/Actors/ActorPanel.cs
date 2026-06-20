using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Compose._3C.Actors
{
    public class ActorPanel : MonoDul
    {
        [SerializeField] private TMP_Text actorName;
        [SerializeField] private TMP_Text hp;
        [SerializeField] private TMP_Text attack;
        [SerializeField] private TMP_Text defense;
        [SerializeField] private Slider hpBar;

        public void Render(ActorData data)
        {
            actorName.text = data.Name;
            hp.text = $"{data.CurrentHp}/{data.MaxHp}";
            attack.text = data.Attack.ToString();
            defense.text = data.Defense.ToString();
            hpBar.maxValue = data.MaxHp;
            hpBar.value = data.CurrentHp;
        }
    }
}
