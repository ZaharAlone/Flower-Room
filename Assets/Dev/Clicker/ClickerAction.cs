using System;

namespace FlowerRoom.Core.Clicker
{
    public static class ClickerAction
    {
        public static Action<string> AddNewItemScene;
        
        public static Action<int> ChangeAddPowerClick;
        public static Action<string> ClickItem;
    }
}