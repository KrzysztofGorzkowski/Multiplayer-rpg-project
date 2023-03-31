using UnityEngine.UIElements;
public class HpBar : VisualElement
{
    public new class UxmlFactory : UxmlFactory<HpBar, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits { }

    public HpBar()
    {
        RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
        RegisterCallback<MouseEnterEvent>(OnMouseEnter);
    }
    private void OnMouseLeave(MouseLeaveEvent evt)
    {
        this.Q<Label>("HpPointsLabel").visible = false;
    }
    private void OnMouseEnter(MouseEnterEvent evt)
    {
        this.Q<Label>("HpPointsLabel").visible = true;
    }
}
