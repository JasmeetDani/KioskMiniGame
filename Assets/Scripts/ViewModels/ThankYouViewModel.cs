using UnityWeld.Binding;

[Binding]
public class ThankYouViewModel : ViewModelBase<ThankYouViewModel, ThankYouController>
{
    [Binding]
    public void OnNext()
    {
        controller.OnNext();
    }
}