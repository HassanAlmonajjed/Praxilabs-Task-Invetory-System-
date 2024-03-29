using NUnit.Framework;
using UnityEngine;
using NSubstitute;

public class InventoryTests
{
    Inventory _inventory;

    [SetUp]
    public void SetupInventory()
    {
        _inventory = Resources.Load<Inventory>("Players Inventory");
        _inventory.Capacity = 10;
    }

    [TearDown]
    public void TeardownInventory() 
    {
        _inventory.Clear();
    }

    [Test]
    public void AddNewItemToEmptyInventory_InventorySizeOne()
    {
        var item = Resources.Load<InventoryItem>("Items/Sword");

        _inventory.Add(item);
        Assert.AreEqual(1, _inventory.Size);
    }

    [Test]
    public void AddTwoItemsOfTheSameType_InventorySizeIsOneItemCountIncrease()
    {
        var item = Substitute.For<IInventoryItem>();

        _inventory.Add(item);
        Assert.AreEqual(1, _inventory.Size);

        _inventory.Add(item);

        Assert.AreEqual(1, _inventory.Size);
        Assert.AreEqual(2, _inventory.NumberOfItemsOf(item));
    }

    [Test]
    public void GetNumberOfItems_ItemNotExsit()
    {
        var item = Substitute.For<IInventoryItem>();
        Assert.Negative(_inventory.NumberOfItemsOf(item));
    }

    [Test]
    public void AddItemToFullInventory_ItemNotAdded()
    {
        _inventory.Capacity = 0;

        var item = Substitute.For<IInventoryItem>();
        bool hasAdded = _inventory.Add(item);

        Assert.IsFalse(hasAdded);
    }

    [Test]
    public void AddItemToEmptyInventory_ItemAdded()
    {
        var item = Substitute.For<IInventoryItem>();
        bool hasAdded = _inventory.Add(item);

        Assert.IsTrue(hasAdded);
    }
}
