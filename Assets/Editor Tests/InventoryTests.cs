using NUnit.Framework;
using PraxilabsTask;
using UnityEngine;

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
    public void TeardownInventory() => _inventory.Clear();

    #region Add Tests

    [Test]
    public void AddTwoItemsOfTheSameType_InventorySizeIsOneItemCountIncrease()
    {
        var item = ScriptableObject.CreateInstance<ArmorInventoryItem>();

        _inventory.Add(item);
        Assert.AreEqual(1, _inventory.Size);

        _inventory.Add(item);

        Assert.AreEqual(1, _inventory.Size);
        Assert.AreEqual(2, _inventory.NumberOfItemsOf(item));
    }

    [Test]
    public void AddTwoItemsOfDifferentTypes_InventorySizeIsTwo()
    {
        var itemA = ScriptableObject.CreateInstance<ArmorInventoryItem>();
        itemA.ItemType = ItemType.Armor;
        _inventory.Add(itemA);

        var itemB = ScriptableObject.CreateInstance<ArmorInventoryItem>();
        itemB.ItemType = ItemType.Potion;
        _inventory.Add(itemB);

        Assert.AreEqual(2, _inventory.Size);
        Assert.AreEqual(1, _inventory.NumberOfItemsOf(itemA));
        Assert.AreEqual(1, _inventory.NumberOfItemsOf(itemB));
    }

    [Test]
    public void GetNumberOfItems_ItemNotExsit()
    {
        var item = ScriptableObject.CreateInstance<ArmorInventoryItem>();
        Assert.Negative(_inventory.NumberOfItemsOf(item));
    }

    [Test]
    public void AddItemToFullInventory_ItemNotAdded()
    {
        _inventory.Capacity = 0;

        var item = ScriptableObject.CreateInstance<ArmorInventoryItem>();
        bool hasAdded = _inventory.Add(item);

        Assert.IsFalse(hasAdded);
    }

    [Test]
    public void AddItemToEmptyInventory_ItemAddedAndSizeIsOne()
    {
        var item = ScriptableObject.CreateInstance<ArmorInventoryItem>();
        item.ItemType = ItemType.Armor;

        bool hasAdded = _inventory.Add(item);

        Assert.IsTrue(hasAdded);
        Assert.AreEqual(1, _inventory.Size);
    }

    #endregion

    #region Remove Tests

    [Test]
    public void AddItemThenRemoveIt_SizeIsZero()
    {
        var item = ScriptableObject.CreateInstance<ArmorInventoryItem>();
        _inventory.Add(item);

        Assert.NotZero(_inventory.Size);

        _inventory.RemoveItem(item);
        Assert.Zero(_inventory.Size);
    }

    [Test]
    public void AddTwoItemThenRemoveOne_SizeIsOneAndNumberOfItemsIsOne()
    {
        InventoryItem item = ScriptableObject.CreateInstance<ArmorInventoryItem>();
        _inventory.Add(item);
        _inventory.Add(item);

        _inventory.RemoveItem(item);

        Assert.AreEqual(1, _inventory.Size);

    }

    [Test]
    public void AddTwoItemThenRemoveBoth_SizeIsZero()
    {
        var item = ScriptableObject.CreateInstance<ArmorInventoryItem>();
        _inventory.Add(item);
        _inventory.Add(item);

        _inventory.RemoveItem(item);
        _inventory.RemoveItem(item);

        Assert.AreEqual(0, _inventory.Size);
    }

    [Test]
    public void AddThreeItemsThenRemoveAllstack_SizeIsZero()
    {
        var item = ScriptableObject.CreateInstance<ArmorInventoryItem>();
        _inventory.Add(item);
        _inventory.Add(item);
        _inventory.Add(item);

        _inventory.RemoveAllItems(item);

        Assert.Zero(_inventory.Size);
    }

    #endregion
}
