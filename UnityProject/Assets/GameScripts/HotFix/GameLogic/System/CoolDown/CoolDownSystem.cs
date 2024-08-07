using System.Collections.Generic;
using System.Linq;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class CoolDownSystem : ASystem
    {
        private List<PrincessCard> _princessCardList = new List<PrincessCard>();

        protected override void Awake()
        {
            base.Awake();
            AddListen();
        }

        private void AddListen()
        {
            GameEvent.AddEventListener<List<EPrincessType>>(Event.ConfirmPrincessCard, OnConfirmPrincessCard);
            GameEvent.AddEventListener<EPrincessType>(Event.ConfirmPrincessCard, OnPlantedPrincessCard);
        }

        private void OnPlantedPrincessCard(EPrincessType princessType)
        {
            foreach (var card in _princessCardList.Where(card => card.PrincessType == princessType))
            {
                card.CoolDown = card.MaxCoolDown;
            }
        }

        private void OnConfirmPrincessCard(List<EPrincessType> list)
        {
            var config = ConfigSystem.Instance.Tables.TPrincessCard;
            _princessCardList.Clear();
            foreach (EPrincessType princessType in list)
            {
                _princessCardList.Add(PrincessCard.ToObject(config.Get(princessType)));
            }
        }

        protected override void Update()
        {
            base.Update();

            if (Battle.Instance.BattleType != EBattleType.Battle) return;
            foreach (PrincessCard princessCard in _princessCardList)
            {
                princessCard.CoolDown -= Time.deltaTime;
                princessCard.CoolDown =  Mathf.Clamp(princessCard.CoolDown, 0, princessCard.MaxCoolDown);
            }
        }
    }
}