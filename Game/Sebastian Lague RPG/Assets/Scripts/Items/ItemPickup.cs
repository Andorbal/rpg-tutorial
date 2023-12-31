using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
  public Item item;

  public override void Interact()
  {
    base.Interact();

    PickUp();
  }

  private void PickUp()
  {
    Debug.Log($"Picking up {item.name}");
    var wasPickedUp = Inventory.instance.Add(item);
    Debug.Log($"Was picked up: {wasPickedUp}");
    if (wasPickedUp)
    {
      Destroy(gameObject);
    }
  }
}
