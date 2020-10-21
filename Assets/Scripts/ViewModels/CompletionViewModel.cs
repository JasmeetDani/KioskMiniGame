using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityWeld.Binding;
using Zenject;

[Binding]
public class CompletionViewModel : ViewModelBase<CompletionViewModel, CompletionController>, IPointerDownHandler
{
    public Transform UI;
    public Transform FinishedBallContainer;
    public Transform FinishedBalls;
    public GraphicRaycaster GUIRayCaster;

    private GameObject FinisheBallsObj;
    private TaskCompletionSource<object> t;

    private string gameTime = "";
    [Binding]
    public string GameTime
    {
        get => gameTime;
        set
        {
            if (gameTime == value) return;
            gameTime = value;
            OnPropertyChanged();
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        controller.OnNext();
    }

    public void CreateDunlopBall(DiContainer container)
    {
        FinisheBallsObj = container.InstantiatePrefab(FinishedBalls, FinishedBallContainer);
    }
    public void DestroyDunlopBall()
    {
        Destroy(FinisheBallsObj);
    }


    public async Task WaitForDunopBallAnimationComplete()
    {
        t = new TaskCompletionSource<object>();
        await t.Task;
    }

    public void AnimationComplete()
    {
        t.SetResult(null);
    }
}