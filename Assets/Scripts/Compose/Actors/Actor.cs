using System;
using ScriptableObjects.Actors;
using UnityEngine;

namespace Compose.Actors
{
    public abstract class ActorData
    {
        public string Name { get; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int MaxHp { get; }
        public int CurrentHp { get; private set; }
        public bool IsDead => CurrentHp == 0;

        protected ActorData(ActorInfo info)
        {
            Name = info.name;
            Attack = Math.Max(0, info.atk);
            Defense = Math.Max(0, info.def);
            MaxHp = Math.Max(1, info.hp);
            CurrentHp = MaxHp;
        }

        public void SetAttack(int value)
        {
            Attack = Math.Max(0, value);
        }

        public void SetDefense(int value)
        {
            Defense = Math.Max(0, value);
        }

        public void TakeDamage(int value)
        {
            CurrentHp = Math.Max(0, CurrentHp - Math.Max(0, value));
        }

        public void Heal(int value)
        {
            CurrentHp = Math.Min(MaxHp, CurrentHp + Math.Max(0, value));
        }
    }

    public abstract class Actor : MonoDul
    {
        [SerializeField] private ActorPanel panel;

        public ActorData Data { get; protected set; }

        protected void Init(ActorData data)
        {
            Data = data;
            Refresh();
        }

        public void SetAttack(int value)
        {
            Data.SetAttack(value);
            Refresh();
        }

        public void SetDefense(int value)
        {
            Data.SetDefense(value);
            Refresh();
        }

        public void TakeDamage(int value)
        {
            Data.TakeDamage(value);
            Refresh();
        }

        public void Heal(int value)
        {
            Data.Heal(value);
            Refresh();
        }

        public void Refresh()
        {
            panel.Render(Data);
        }
    }
}
