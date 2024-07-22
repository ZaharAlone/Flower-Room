using EcsCore;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using System;
using FlowerRoom.Core.CurrencyFlower;
using FlowerRoom.Core.Traderow;

namespace FlowerRoom.Core.Levels
{
    [EcsSystem(typeof(CoreModule))]
    public class LevelsSystem : IInitSystem, IDestroySystem
    {
        private DataWorld _dataWorld;

        public void Init()
        {
            CreateNewGame();
        }

        private void CreateNewGame()
        {
            InitFirstLevelCurrentSessionGame(0);
        }

        private void InitFirstLevelCurrentSessionGame(int indexNewLevel)
        {
            _dataWorld.CreateOneData(new LevelData
            {
                CurrentIndexLevel = indexNewLevel,
            });
            
            InitLevel(indexNewLevel);
        }
        
        private void InitLevel(int indexNewLevel)
        {
            TraderowAction.InitLevel?.Invoke(indexNewLevel);
        }

        public void Destroy() { }
    }
}