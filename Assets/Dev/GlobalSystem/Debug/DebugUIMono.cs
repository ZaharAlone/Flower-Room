using FlowerRoom.Core.Clicker;
using UnityEngine;

namespace FlowerRoom.GlobalSystem.Debug
{
    public class DebugUIMono : MonoBehaviour
    {
        public void AddPowerClick_1()
        {
            AddPowerClickAction(1);
        }
        
        public void AddPowerClick_5()
        {
            AddPowerClickAction(5);
        }

        public void AddPowerClick_10()
        {
            AddPowerClickAction(10);
        }

        private void AddPowerClickAction(int value)
        {
            ClickerAction.ChangeAddPowerClick?.Invoke(value);
        }
    }
}