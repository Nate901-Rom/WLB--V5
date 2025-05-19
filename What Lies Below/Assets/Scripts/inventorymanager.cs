using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public int slotCount = 9; // Like the hotbar
    public InventorySlot[] slots;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        slots = new InventorySlot[slotCount];
        for (int i = 0; i < slotCount; i++)
            slots[i] = new InventorySlot();
    }

    public bool AddItem(Item newItem, int amount = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == newItem && newItem.isStackable)
            {
                slots[i].quantity += amount;
                return true;
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].item = newItem;
                slots[i].quantity = amount;
                return true;
            }
        }

        Debug.Log("Inventory full!");
        return false;
    }

    public void RemoveItem(int index, int amount = 1)
    {
        if (slots[index].item != null)
        {
            slots[index].quantity -= amount;
            if (slots[index].quantity <= 0)
                slots[index].ClearSlot();
        }
    }
}