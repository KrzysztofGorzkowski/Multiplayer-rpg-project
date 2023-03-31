using UnityEngine.UIElements;

public class InventorySlot : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InventorySlot, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits { }

    public Image icon;
    public string item = "empty";
    public InventorySlot()
    {
        //Create a new Image element and add it to the root
        icon = new Image();
        Add(icon);
        //Add USS style properties to the elements
        icon.AddToClassList("slotIcon");
        AddToClassList("slotContainer");
    }
}
