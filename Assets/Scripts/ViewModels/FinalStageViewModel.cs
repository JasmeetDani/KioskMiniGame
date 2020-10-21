using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityWeld.Binding;
using UnityEngine.UI;

[Binding]
public class FinalStageViewModel : ViewModelBase<FinalStageViewModel, FinalStageController>, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI TimeElapsed;

    public Rigidbody Ball;

    public Image FillBar;

    public Transform Outline;

    public GraphicRaycaster raycaster;

    public void OnPointerDown(PointerEventData eventData)
    {
        controller.FillUpBar();

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        controller.DisableOutline();
    }
}