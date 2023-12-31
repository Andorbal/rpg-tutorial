using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
  public Image icon;
  public Button removeButton;
  Item item;

  public void AddItem(Item newItem)
  {
    item = newItem;

    icon.sprite = item.icon;
    icon.enabled = true;
    removeButton.interactable = true;
  }

  public void ClearSlot()
  {
    item = null;

    icon.enabled = false;
    icon.sprite = null;
    removeButton.interactable = false;
  }

  public void OnRemoveButton()
  {
    Inventory.instance.Remove(item);
  }

  public void UseItem()
  {
    if (item != null)
    {
      item.Use();
    }
  }
}
