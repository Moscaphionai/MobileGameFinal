using Compose.Actors;
using Messages;
using Messages.Commands.Battle;
using Messages.Events.Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using CardComponent = Compose.Card.Card;

namespace Compose.UI
{
    public sealed class BattlePanel : BasePanel
    {
        [SerializeField] private GameObject actorPrefab;
        [SerializeField] private Transform actorRoot;
        [SerializeField] private Vector3 playerPosition;
        [SerializeField] private Vector3 enemyPosition;
        [SerializeField] private TMP_Text energy;
        [SerializeField] private TMP_Text turn;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private GameObject handRoot;
        [SerializeField] private Button endTurnButton;
        [SerializeField] private float cardSpacing = 140f;
        [SerializeField] private float cardScale = 0.35f;

        private PlayerView playerActorView;
        private EnemyView enemyActorView;
        private BattleData data;

        private void OnEnable()
        {
            EventQueueManager.Instance.AddListener<BattleStartedEvent>(OnBattleStarted);
            EventQueueManager.Instance.AddListener<BattleDataChangedEvent>(OnBattleDataChanged);
            EventQueueManager.Instance.AddListener<BattleTurnChangedEvent>(OnBattleTurnChanged);
            endTurnButton.onClick.AddListener(EndTurn);
        }

        private void OnDisable()
        {
            EventQueueManager.Instance.RemoveListener<BattleStartedEvent>(OnBattleStarted);
            EventQueueManager.Instance.RemoveListener<BattleDataChangedEvent>(OnBattleDataChanged);
            EventQueueManager.Instance.RemoveListener<BattleTurnChangedEvent>(OnBattleTurnChanged);
            endTurnButton.onClick.RemoveListener(EndTurn);
        }

        private void OnBattleStarted(BattleStartedEvent evt)
        {
            var playerActor = Instantiate(actorPrefab, playerPosition, Quaternion.identity, actorRoot);
            var enemyActor = Instantiate(actorPrefab, enemyPosition, Quaternion.identity, actorRoot);

            playerActorView = playerActor.GetComponentInChildren<PlayerView>();
            enemyActorView = enemyActor.GetComponentInChildren<EnemyView>();

            playerActorView.Render(evt.player);
            enemyActorView.Render(evt.enemy);
        }

        private void OnBattleDataChanged(BattleDataChangedEvent evt)
        {
            data = evt.data;
            RefreshActors();
            RefreshEnergy();
            RefreshHand();
        }

        private void OnBattleTurnChanged(BattleTurnChangedEvent evt)
        {
            turn.text = $"{evt.current}  Round {evt.round}";
        }

        private void RefreshActors()
        {
            playerActorView.Render(data.player);
            enemyActorView.Render(data.enemy);
        }

        private void RefreshEnergy()
        {
            energy.text = $"Energy {data.energy} / {data.maxEnergy}";
        }

        private void RefreshHand()
        {
            handRoot.ClearChildren();

            var offset = (data.hand.Count - 1) * cardSpacing * 0.5f;
            for (var i = 0; i < data.hand.Count; i++)
            {
                var go = Instantiate(cardPrefab, handRoot.transform);
                var rect = go.GetComponent<RectTransform>();
                rect.localScale = Vector3.one * cardScale;
                rect.anchoredPosition = new Vector2(i * cardSpacing - offset, 0f);

                var card = go.GetComponent<CardComponent>();
                card.Init(data.hand[i], i);
            }
        }

        private void EndTurn()
        {
            CommandQueueManager.Instance.Send(new EndPlayerTurnCommand());
        }
    }
}
