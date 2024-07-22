using EcsCore;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using System;
using FlowerRoom.Core.Clicker.Items;
using Input;

namespace FlowerRoom.Core.MoveItem
{
    [EcsSystem(typeof(CoreModule))]
    public class MoveClickerItemSystem : IPreInitSystem, IRunSystem, IDestroySystem
    {
        private DataWorld _dataWorld;

        public void PreInit()
        {
            MoveItemAction.StartMoveItem += StartMoveItem;
        }
        
        private void StartMoveItem(string guid)
        {
            var isMoveOtherItemQuery = _dataWorld.Select<InteractiveMoveComponent>();
            if (isMoveOtherItemQuery.Count() > 0)
            {
                var itemMoveComponent = isMoveOtherItemQuery.SelectFirstEntity().GetComponent<ClickerItemComponent>();
                
                if (itemMoveComponent.GUID == guid)
                    EndMove();
                return;
            }

            var itemEntity = _dataWorld.Select<ClickerItemComponent>()
                .Where<ClickerItemComponent>(item => item.GUID == guid)
                .SelectFirstEntity();
            var mousePosition = _dataWorld.OneData<InputData>().MousePosition;

            itemEntity.AddComponent(new InteractiveMoveComponent {
                StartMousePositions = mousePosition,
            });
            itemEntity.GetComponent<ClickerItemComponent>().ClickerItemMono.ForceHidePanelUpgrade();
        }

        public void Run()
        {
            var isMoveComponent = _dataWorld.Select<InteractiveMoveComponent>().Count() > 0;

            if (!isMoveComponent)
                return;
            
            MoveItem();

            var inputData = _dataWorld.OneData<InputData>();
            if (inputData.Click)
                EndMove();
        }
        
        private void MoveItem()
        {
            var moveItemEntity = _dataWorld.Select<ClickerItemComponent>()
                .With<InteractiveMoveComponent>()
                .SelectFirstEntity();
            ref var inputData = ref _dataWorld.OneData<InputData>();

            ref var moveComponent = ref moveItemEntity.GetComponent<InteractiveMoveComponent>();
            ref var clickerItemComponent = ref moveItemEntity.GetComponent<ClickerItemComponent>();

            var deltaMove = inputData.MousePosition - moveComponent.StartMousePositions;
            clickerItemComponent.ClickerItemMono.RectTransform.anchoredPosition += new Vector2(deltaMove.x, deltaMove.y);
            moveComponent.StartMousePositions = inputData.MousePosition;
        }

        private void EndMove()
        {
            if (!IsInDropZone())
                return;
            
            var moveItemEntity = _dataWorld.Select<ClickerItemComponent>()
                .With<InteractiveMoveComponent>()
                .SelectFirstEntity();
            var moveItemComponent = moveItemEntity.GetComponent<ClickerItemComponent>();

            moveItemEntity.RemoveComponent<InteractiveMoveComponent>();
            
            moveItemComponent.ClickerItemMono.ForceShowPanelUpgrade();
        }
        
        private bool IsInDropZone()
        {
            var moveItemEntity = _dataWorld.Select<ClickerItemComponent>()
                .With<InteractiveMoveComponent>()
                .SelectFirstEntity();
            var clickerItemMono = moveItemEntity.GetComponent<ClickerItemComponent>().ClickerItemMono;
            var transformItem = clickerItemMono.transform;
            
            var hit = Physics2D.Raycast(transformItem.position, Vector2.zero);
            var isCollider = hit.collider != null;

            if (!isCollider)
                return false;
            
            var tagDropZone = hit.collider.CompareTag("DropZone");
        
            if (isCollider && tagDropZone)
            {
                foreach (Transform child in hit.collider.transform)
                {
                    if (child != transformItem && IsOverlapping(clickerItemMono.RectTransform, child))
                        return false;
                }
                return true;
            }
            return false;
        }

        private bool IsOverlapping(RectTransform rectTransformItem, Transform other)
        {
            RectTransform rect1 = rectTransformItem;
            RectTransform rect2 = other.GetComponent<RectTransform>();
        
            var overlap = RectTransformExtensions.Overlaps(rect1, rect2);
            return overlap;
        }

        public void Destroy()
        {
            
        }
    }
}