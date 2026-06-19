using UnityEngine;

namespace ScriptableObjects
{
    public abstract class ActorInfo
    {
        public string name;
        public int atk;
        public int def;
        public int hp;
    }

    public class PlayerInfo : ActorInfo
    {
    }

    public class EnemyInfo : ActorInfo
    {
    }

    public abstract class ActorSO : ScriptableObject
    {
        public ActorInfo info { get; private set; }
    }


    [CreateAssetMenu(fileName = "PlayerName", menuName = "ScriptableObjects/PlayerSO")]
    public class PlayerSO : ActorSO
    {

        private PlayerSO()
        {
        }
    }

    public class EnemySO : ActorSO
    {
        private EnemySO()
        {
        }
    }
}