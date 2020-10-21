using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;
using UnityEngine.Playables;

public class Stage1Controller : ControllerBase<Stage1Controller, Stage1ViewModel>
{
    [Inject]
    private Stage2Controller next;
    [Inject]
    private GameController gameController;
    private IDisposable timer;
    private IDisposable resetStage;
    private int dotsTouched;
    private int nexts;
    private int dotsToShow;
    public Queue<int> dotsClicked = new Queue<int>();
    public Queue<int> DotsClicked => dotsClicked;

    public override void OnEnable()
    {
        resetStage = Disposable.Create(() =>
        {
            Color c = viewModel.Rubber.color;
            viewModel.Rubber.color = new Color(c.r, c.g, c.b, 1);
            c = viewModel.Shells[0].color;
            viewModel.Shells[0].color = new Color(c.r, c.g, c.b, 0);
            c = viewModel.Shells[1].color;
            viewModel.Shells[1].color = new Color(c.r, c.g, c.b, 0);

            dotsClicked.Clear();

            viewModel.Director.Stop();

            Time.timeScale = 1.0f;
        });
        timer = gameController.OnTimeElapsed.ObserveOnMainThread<TimeSpan>().Subscribe(this.OnTimeElapsed);
        nexts = 0;
        dotsToShow = 3;
        ShuffleAndShowDots(dotsToShow);
        viewModel.Objective.text = "TO ADD SECRET RECIPE TO PUREST NATURAL RUBBER";

        Time.timeScale = 1.5f;

        viewModel.Director.Play();

        gameController.StartGame();
    }
    public override void OnDisable()
    {
        resetStage.Dispose();
        timer.Dispose();
    }

    public override void OnNext()
    {
        Disable();
        next.Enable();
    }

    public void OnTimeElapsed(TimeSpan value)
    {
        viewModel.TimeElapsed.text = gameController.GameTime;
    }

    private void ShuffleAndShowDots(int dotsToShow)
    {
        dotsTouched = 0;

        float w = viewModel.Dots[0].sizeDelta.x;
        float h = viewModel.Dots[0].sizeDelta.y;
        Vector2 size = new Vector2(w, h);
        Vector2[] arr = new Vector2[3];
        bool tryAgain = false;
        for(int i = 0;i < dotsToShow;)
        {
            float x = UnityEngine.Random.Range(0, viewModel.Parent.sizeDelta.x - w);
            float y = UnityEngine.Random.Range(0, viewModel.Parent.sizeDelta.y - h);
            arr[i] = new Vector2(x, y);
            for(int j = i - 1;j >= 0;--j)
            {
                Rect r = new Rect(arr[j], size);
                Rect s = new Rect(arr[i], size);
                if(r.Overlaps(s))
                {
                    tryAgain = true;
                    break;
                }
            }
            if(tryAgain)
            {
                tryAgain = false;
                continue;
            }
            ++i;
        }
        for (int i = 0; i < dotsToShow; ++i)
        {
            viewModel.Dots[i].anchoredPosition = arr[i];
            viewModel.Dots[i].gameObject.SetActive(true);
        }
    }

    public async void DotTouched(int index)
    {
        viewModel.Dots[index].gameObject.SetActive(false);
        dotsTouched++;

        if(viewModel.Director.state == PlayState.Paused)
        {
            viewModel.Director.Resume();
        }
        else
        {
            dotsClicked.Enqueue(0);
        }
        if (dotsTouched == dotsToShow)
        {
            nexts++;
            if (nexts == 5)
            {
                return;
            }
            if (nexts == 3)
            {
                dotsToShow = 1;
                viewModel.Objective.text = "TO MOULD RUBBER INTO\n 2 HALF SHELLS";
            }
            if (nexts <= 4)
            {
                ShuffleAndShowDots(dotsToShow);
            }
        }
    }
}