using UnityEngine.UIElements;
public class HeroPanel : VisualElement
{
    public new class UxmlFactory : UxmlFactory<HeroPanel, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits { }
}
