using EcsCore;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using FlowerRoom.Core.Clicker.Items;
using FlowerRoom.Core.CurrencyFlower;

namespace FlowerRoom.Core.Clicker.ProductionPerSeconds
{
    [EcsSystem(typeof(CoreModule))]
    public class ProductionPerSecondsSystem : IRunSystem
    {
        private DataWorld _dataWorld;

        private const float one_seconds = 1;
        private float _currentTimeTick;

        public void Run()
        {
            _currentTimeTick += Time.deltaTime;

            if (_currentTimeTick >= one_seconds)
            {
                _currentTimeTick -= one_seconds;
                ProductionPerSecondResource();
            }
        }
        
        private void ProductionPerSecondResource()
        {
            var clickerItemsEntities = _dataWorld.Select<ClickerItemComponent>().GetEntities();

            var debugCheckAdd = 0f;
            foreach (var clickerItemEntity in clickerItemsEntities)
            {
                var clickerItemComponent = clickerItemEntity.GetComponent<ClickerItemComponent>();
                var finalAddResource = clickerItemComponent.AddCurrencyPerSecond + clickerItemComponent.BonusValuePerSecond;
                var perSecondResource = (int)Mathf.Floor(finalAddResource);

                if (perSecondResource > 0)
                    CurrencyFlowerAction.AddCurrencyFlower.Invoke(perSecondResource);

                debugCheckAdd += perSecondResource;
            }
            
            Debug.Log($"final add in second {debugCheckAdd}");
        }
    }
}