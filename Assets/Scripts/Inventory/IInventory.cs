using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    public int Size {  get; }
    public int Capacity { get; set; }

    public bool Add(IInventoryItem inventoryItem);
    public void Clear();
}
