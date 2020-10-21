using UnityEngine;
using Zenject;

public class GameStarter : MonoBehaviour
{
    [Inject]
    private StartScreenController startScreenController;

    private void Start()
    {
        Input.multiTouchEnabled = false;

        startScreenController.Enable();
    }
}