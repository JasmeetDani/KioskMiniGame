using UnityEngine;
using Zenject;

public class FinishedDunlopBallAnimationComplete : MonoBehaviour
{
    [Inject]
    private CompletionController controller;

    public void OnAnimationComplete()
    {
        controller.AnimationComplete();
    }
}