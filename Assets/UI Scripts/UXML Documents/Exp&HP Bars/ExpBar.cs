using UnityEngine.UIElements;
public class ExpBar : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ExpBar, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits { }

    public ExpBar()
    {
        RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
        RegisterCallback<MouseEnterEvent>(OnMouseEnter);
    }
    private void OnMouseLeave(MouseLeaveEvent evt)
    {
        this.Q<Label>("ExpPointsLabel").visible = false;
    }
    private void OnMouseEnter(MouseEnterEvent evt)
    {
        this.Q<Label>("ExpPointsLabel").visible = true;
    }
}

