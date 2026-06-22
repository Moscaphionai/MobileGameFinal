using System;
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

        protected ActorData(ActorInfo info)
        {
            name = info.name;
            atk = info.atk;
            def = info.def;
            hp = info.hp;
            curHp = hp;
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
