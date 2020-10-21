using Zenject;

public abstract class ControllerBase<C, VM> : IToggleable, IScreen
                                                where C : ControllerBase<C, VM>
                                                where VM : ViewModelBase<VM, C>
{
    [Inject]
    protected VM viewModel;
    
    public virtual void Enable()
    {
        viewModel.gameObject.SetActive(true);
    }
    public virtual void Disable()
    {
        viewModel.gameObject.SetActive(false);
    }

    public virtual void OnEnable() {}
    public virtual void OnDisable() {}
           
    public abstract void OnNext();
}