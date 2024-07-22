namespace FlowerRoom.Core.Clicker.Items
{
    public struct ClickerItemComponent
    {
        public string KeyItem;
        public string GUID;
        
        public float MultiplierValue;
        public ClickerItemsType ClickerItemsType;
        public ClickerItemMono ClickerItemMono;

        public int WateringCountBuy;
        public int WeedingCountBuy;
        public int FertilizingCountBuy;
    }
}