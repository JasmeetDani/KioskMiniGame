public class ScreenFlasherController : ControllerBase<ScreenFlasherController, ScreenFlasherViewModel>
{
    private IToggleable current;
    private IToggleable next;

    public void Flash(IToggleable current, IToggleable next)
    {
        this.current = current;
        this.next = next;

        this.Enable();
    }

    public override void OnNext()
    {
        current.Disable();
        next.Enable();
    }
}