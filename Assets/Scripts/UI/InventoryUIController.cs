using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

//Script that handles Inventory logic
public class InventoryUIController : MonoBehaviour
{
    public List<Sprite> IconSprites;
    public List<InventorySlot> InventoryItems = new List<InventorySlot>();

    public UIDocument heroPanel;

    private void Awake()
    {
        var root = heroPanel.rootVisualElement;
        var slotContainer = root.Q<VisualElement>("ItemsCointainer");
        //Create InventorySlots and add them as children to the SlotContainer
        for (int i = 0; i < 20; i++)
        {
            InventorySlot item = new InventorySlot();
            slotContainer.Add(item);
            InventoryItems.Add(item);
        }
    }
    
    private void OnEnable()
    {
        //KeyPickUpTriggerScript.KeyPickedUp += AddKey;
    }

    private void OnDisable()
    { 
        //KeyPickUpTriggerScript.KeyPickedUp -= AddKey;
    }

    private void AddKey()
    {
        //find first empty inventory slot and add key to it
        var item = InventoryItems.Find(x => x.item.Contains("empty"));
        if(item != null)
        {
            item.icon.image = IconSprites.FirstOrDefault(x => x.name.Equals("keyUI")).texture;
            item.item = "Key";
        }
    }
}
