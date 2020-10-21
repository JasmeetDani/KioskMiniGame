using UnityEngine;
using Zenject;

public class ShellsCollideHandler : MonoBehaviour
{
    [Inject]
    private Stage2Controller controller;

    private void OnTriggerEnter(Collider other)
    {
        controller.OnShellsCollide();
    }
}