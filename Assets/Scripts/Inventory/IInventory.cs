namespace PraxilabsTask
{
    public interface IInventory
    {
        public int Size { get; }
        public int Capacity { get; set; }

        public bool Add(InventoryItem inventoryItem, int number);
    }
}