using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Compose.Actors
{
    public class ActorView : MonoDul
    {
        [SerializeField] private TMP_Text hp;
        [SerializeField] private Slider hpBar;

        public void Render(ActorData data)
        {
            hp.text = $"{data.curHp} / {data.hp}";
            hpBar.maxValue = data.hp;
            hpBar.value = data.curHp;
        }
    }
}
