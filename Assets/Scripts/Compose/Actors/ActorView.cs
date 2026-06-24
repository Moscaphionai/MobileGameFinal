using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Compose.Actors
{
    public class ActorView : MonoDul
    {
        [SerializeField] protected SpriteRenderer idle;
        [SerializeField] protected TMP_Text hp;
        [SerializeField] protected Slider hpBar;

        public virtual void Render(ActorData data)
        {
            idle.sprite = data.idle;
            hp.text = $"{data.curHp} / {data.hp}";
            hpBar.maxValue = data.hp;
            hpBar.value = data.curHp;
        }
    }
}
