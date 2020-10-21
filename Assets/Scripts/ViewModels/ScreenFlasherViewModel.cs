public class ScreenFlasherViewModel : ViewModelBase<ScreenFlasherViewModel, ScreenFlasherController>
{
    public void OnFlashEnd()
    {
        controller.Disable();
    }

    public void OnNext()
    {
        controller.OnNext();
    }
}