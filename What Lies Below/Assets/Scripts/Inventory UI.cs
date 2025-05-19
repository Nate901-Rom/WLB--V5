using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public Text quantityText;
    private int index;

    public void SetupSlot(int i)
    {
        index = i;
    }

    void Update()
    {
        var slot = Inventory.Instance.slots[index];
        if (slot.item != null)
        {
            icon.sprite = slot.item.icon;
            icon.enabled = true;
            quantityText.text = slot.item.isStackable ? slot.quantity.ToString() : "";
        }
        else
        {
            icon.enabled = false;
            quantityText.text = "";
        }
    }
}
