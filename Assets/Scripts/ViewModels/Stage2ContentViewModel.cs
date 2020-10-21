using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Stage2ContentViewModel : MonoBehaviour
{
    [Inject]
    public Stage2Controller controller;


    public Transform[] Shells;

    public Transform WrapsParent;

    public Image Flaps;

    public Animator ShellsAnimator;

    public Transform RubberBall;

    [SerializeField]
    private Animator[] Wraps;
    private bool FirstFlapPlaced = false;
    private bool FirstRoundComplete = false;

    public bool ShellsJoined = false;


    public void DisableShellDrag()
    {
        foreach (Transform t in Shells)
        {
            Destroy(t.gameObject.GetComponent<Drag3dObject>());
        }
    }

    public async Task LerpShellsToFormBall(float speed, Vector3 target)
    {
        var t = new TaskCompletionSource<object>();
        StartCoroutine(LerpToTarget(t, 0, speed, target, Quaternion.Euler(0, 90, 0)));
        StartCoroutine(LerpToTarget(t, 1, speed, target, Quaternion.Euler(0, -90, 0)));
        await t.Task;
    }

    IEnumerator LerpToTarget(TaskCompletionSource<object> t, int index, float speed, Vector3 target, Quaternion targetRotation)
    {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(Shells[index].position, target);
        Vector3 startPosition = Shells[index].localPosition;
        Quaternion startRotation = Shells[index].localRotation;
        float distCovered = 0;
        float fractionOfJourney = 0;
        while (fractionOfJourney < 1)
        {
            distCovered = (Time.time - startTime) * speed;
            fractionOfJourney = distCovered / journeyLength;
            Shells[index].localPosition = Vector3.Lerp(startPosition, target, fractionOfJourney);
            Shells[index].localRotation = Quaternion.Lerp(startRotation, targetRotation, fractionOfJourney);
            yield return null;
        }

        if (!t.Task.IsCompleted)
        {
            t.SetResult(null);
        }
    }

    public void ShellReadyToWrap(int index)
    {
        controller.ShellReadyToWrap(index);
    }

    public void NotifyFirstFlapPlaced()
    {
        FirstFlapPlaced = true;
    }

    public bool FlapDropPossible(ref bool second)
    {
        second = FirstRoundComplete;

        if (ShellsJoined && (!FirstFlapPlaced || FirstRoundComplete))
        {
            return true;
        }

        return false;
    }

    public void OnFirstRoundComplete()
    {
        FirstRoundComplete = true;
    }

    public void OnShellRotateComplete()
    {
        Wraps[controller.FlapsQ.Dequeue()].SetTrigger("Next");
    }

    public async Task AllDone()
    {
        await Task.Delay(1000);
        controller.OnNext();
    }
}