using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
  #region Singleton
  public static EquipmentManager instance;

  public void Awake()
  {
    if (instance != null)
    {
      Debug.LogWarning("There is already an equipment manager instantiated");
    }

    instance = this;
  }
  #endregion

  public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
  public OnEquipmentChanged onEquipmentChanged;

  Equipment[] currentEquipment;
  private Inventory inventory;

  // Start is called before the first frame update
  void Start()
  {
    inventory = Inventory.instance;
    var numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
    currentEquipment = new Equipment[numSlots];
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.U))
    {
      UnequipAll();
    }
  }

  public void Equip(Equipment newItem)
  {
    int slotIndex = (int)newItem.equipSlot;
    var oldItem = currentEquipment[slotIndex];

    if (oldItem != null)
    {
      inventory.Add(oldItem);
    }

    onEquipmentChanged?.Invoke(newItem, oldItem);
    currentEquipment[slotIndex] = newItem;
  }

  public void Unequip(int slotIndex)
  {
    var oldItem = currentEquipment[slotIndex];
    if (oldItem != null)
    {
      inventory.Add(oldItem);
      currentEquipment[slotIndex] = null;
      onEquipmentChanged?.Invoke(null, oldItem);
    }
  }

  public void UnequipAll()
  {
    for (int i = 0; i < currentEquipment.Length; i += 1)
    {
      Unequip(i);
    }
  }
}
