using TMPro;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class Stage2ViewModel : ViewModelBase<Stage2ViewModel, Stage2Controller>
{
    public TextMeshProUGUI TimeElapsed;

    public Transform Parent;
    public Transform Content;
}