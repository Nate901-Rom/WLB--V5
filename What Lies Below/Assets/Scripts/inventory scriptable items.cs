using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool isStackable = true;
}
[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int quantity;

    public void ClearSlot()
    {
        item = null;
        quantity = 0;
    }
}
