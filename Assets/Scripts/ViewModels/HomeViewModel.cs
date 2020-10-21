using UnityEngine;
using UnityEngine.EventSystems;
using UnityWeld.Binding;

[Binding]
public class HomeViewModel : ViewModelBase<HomeViewModel, HomeController>, IPointerDownHandler
{
    public Rigidbody[] balls;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        controller.OnNext();
    }
}