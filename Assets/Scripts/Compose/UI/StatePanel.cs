using System;
using Compose.Actors;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Compose.UI
{
    public class StatePanel : BasePanel
    {
        [SerializeField] private TMP_Text atk;
        [SerializeField] private TMP_Text def;
        [SerializeField] private TMP_Text hp;

        [SerializeField] private Image avatar;

        private PlayerData data => RunManager.Instance.playerData;

        private void Start()
        {
            Refresh();
        }

        private void Refresh()
        {
            atk.text = data.atk.ToString();
            def.text = data.def.ToString();
            hp.text = $"{data.curHp} / {data.hp}";
        }
    }
}