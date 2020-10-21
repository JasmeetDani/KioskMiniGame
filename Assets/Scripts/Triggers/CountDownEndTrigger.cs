using UnityEngine;
using Zenject;

public class CountDownEndTrigger : MonoBehaviour
{
    [Inject]
    private CountdownController controller;

    public void OnCountDownEnd()
    {
        controller.OnNext();
    }
}