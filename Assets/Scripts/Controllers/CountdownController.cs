using Zenject;
using UnityEngine;

public class CountdownController : ControllerBase<CountdownController, CountdownViewModel>
{
    [Inject]
    private Stage1Controller next;
    [Inject]
    private DiContainer container;
    private GameObject Content;
    
    public override void OnNext()
    {
        Disable();
        next.Enable();
    }
    public override void OnDisable()
    {
        Object.Destroy(Content);
    }
    public override void OnEnable()
    {
        viewModel.start.gameObject.SetActive(true);
    }

    public void OnCountdownStart()
    {
        viewModel.start.gameObject.SetActive(false);
        Content = container.InstantiatePrefab(viewModel.animation.gameObject, viewModel.transform);
    }
}