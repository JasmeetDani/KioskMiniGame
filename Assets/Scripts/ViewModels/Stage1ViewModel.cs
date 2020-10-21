using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityWeld.Binding;

[Binding]
public class Stage1ViewModel : ViewModelBase<Stage1ViewModel, Stage1Controller>
{
    public TextMeshProUGUI TimeElapsed;

    public TextMeshProUGUI Objective;

    public Image Rubber;
    public Image[] Shells;

    public RectTransform Parent;
    public RectTransform[] Dots;

    public Animator Animator;

    public PlayableDirector Director;

    public void AnimDone()
    {
        if(controller.DotsClicked.Count > 0)
        {
            controller.DotsClicked.Dequeue();
            return;
        }
        Director.Pause();
    }
    public void NoAction()
    {
        Director.Pause();
    }

    public void AllDone()
    {
        controller.OnNext();
    }
}