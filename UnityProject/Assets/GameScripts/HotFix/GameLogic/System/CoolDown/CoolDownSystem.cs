﻿using System.Collections.Generic;
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
            List<PrincessCard> _princessCardList = Battle.Instance._PrincessCardList;
            foreach (var card in _princessCardList.Where(card => card.PrincessType == princessType))
            {
                card.CoolDown = card.MaxCoolDown;
            }
        }

        

        protected override void Update()
        {
            base.Update();
            if (Battle.Instance.BattleType != EBattleType.Battle) return;
            
            List<PrincessCard> _princessCardList = Battle.Instance._PrincessCardList;
            foreach (PrincessCard princessCard in _princessCardList)
            {
                princessCard.CoolDown -= Time.deltaTime;
                princessCard.CoolDown =  Mathf.Clamp(princessCard.CoolDown, 0, princessCard.MaxCoolDown);
            }
        }
    }
}