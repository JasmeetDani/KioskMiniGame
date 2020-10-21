using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class StartScreenViewModel : ViewModelBase<StartScreenViewModel, StartScreenController>
{
    public Transform Parent;
    public Transform Home;
    public Animator SlideAnimator;
}