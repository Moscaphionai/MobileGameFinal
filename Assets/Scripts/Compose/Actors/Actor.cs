using System;
using Compose.Effects;
using ScriptableObjects;
using ScriptableObjects.Actors;
using UnityEngine;

namespace Compose.Actors
{
    [Serializable]
    public abstract class ActorData
    {
        public string name;
        public int atk;
        public int def;
        public int hp;
        public int curHp;
        public int shield;
        public Sprite idle;

        protected ActorData(ActorInfo info, Sprite idle)
        {
            name = info.name;
            atk = info.atk;
            def = info.def;
            hp = info.hp;
            curHp = hp;
            this.idle = idle;
        }

        public void ApplyEffect(EffectData effect, int value)
        {
            switch (effect.type)
            {
                case EffectType.Damage:
                    var shieldDamage = value;
                    if (shieldDamage > shield)
                        shieldDamage = shield;

                    shield -= shieldDamage;
                    value -= shieldDamage;
                    curHp -= value;

                    if (curHp < 0)
                        curHp = 0;
                    break;
                case EffectType.Shield:
                    shield += value;
                    break;
                case EffectType.Heal:
                    curHp += value;
                    if (curHp > hp)
                        curHp = hp;
                    break;
            }
        }
    }

    public abstract class Actor : MonoDul
    {
        [SerializeField] private ActorView view;
        public Guid uid;
        public ActorData data;

        protected void Init(ActorData data)
        {
            uid = Guid.NewGuid();
            this.data = data;
            Refresh();
        }

        public void Refresh()
        {
            view.Render(data);
        }
    }
}
