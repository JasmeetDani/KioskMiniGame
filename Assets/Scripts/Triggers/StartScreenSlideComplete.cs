using UnityEngine;
using Zenject;

public class StartScreenSlideComplete : MonoBehaviour
{
    [Inject]
    private StartScreenController controller;

    public void SwapSubScreen()
    {
        controller.SwapSubScreen();
    }
}