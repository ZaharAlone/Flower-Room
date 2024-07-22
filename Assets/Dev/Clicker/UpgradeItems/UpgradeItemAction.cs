using System;
namespace FlowerRoom.Core.Clicker.UpgradeItems
{
    public static class UpgradeItemAction
    {
        public static Action<string> UpdateUIUpgradeItem;
        
        public static Action<string, UpgradeClickerItemType> UpgradeItem;
    }
}