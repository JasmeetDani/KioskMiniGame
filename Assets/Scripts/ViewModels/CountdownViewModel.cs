using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class CountdownViewModel : ViewModelBase<CountdownViewModel, CountdownController>
{
    public Transform start;
    public Transform animation;

    [Binding]
    public void OnCountdownStart()
    {
        controller.OnCountdownStart();
    }
}