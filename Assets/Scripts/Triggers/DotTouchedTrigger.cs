using UnityEngine;
using Zenject;

public class DotTouchedTrigger : MonoBehaviour
{
    [Inject]
    private Stage1Controller controller;
    [SerializeField]
    private int index;

    public void OnDotTouched()
    {
        controller.DotTouched(this.index);
    }
}