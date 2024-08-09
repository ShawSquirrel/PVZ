using System.Collections.Generic;
using System.Linq;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class CoolDownSystem : ASystem
    {
        protected override void Awake()
        {
            base.Awake();
            AddListen();
        }

        private void AddListen()
        {
        }

        public void PlantedPrincessCard(EPrincessType princessType)
        {
            List<SelectedPrincessCardData> _princessCardList = Battle.Instance._SelectedPrincessCardList;
            foreach (var card in _princessCardList.Where(card => card.PrincessType == princessType))
            {
                card.CoolDown = card.MaxCoolDown;
            }
        }

        

        protected override void Update()
        {
            base.Update();
            if (Battle.Instance.BattleType != EBattleType.Battle) return;
            
            List<SelectedPrincessCardData> _princessCardList = Battle.Instance._SelectedPrincessCardList;
            foreach (SelectedPrincessCardData princessCard in _princessCardList)
            {
                princessCard.CoolDown -= Time.deltaTime;
                princessCard.CoolDown =  Mathf.Clamp(princessCard.CoolDown, 0, princessCard.MaxCoolDown);
            }
        }
    }
}