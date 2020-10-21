using Mono.Data.Sqlite;
using System;
using System.Data.Linq;
using System.Diagnostics;
using System.Timers;
using UniRx;
using UnityEngine;

public class GameController
{
    private const int timerGranularity = 10;

    private int currentPlayerID;
    public int CurrentPlayerID => currentPlayerID;

    private DateTime gameStartTime;

    private Stopwatch st = new Stopwatch();
    public string GameTime
    {
        get
        {
            if (st.Elapsed.Hours > 0)
            {
                return st.Elapsed.ToString(@"hh\:mm\:ss\:ff");
            }
            else if (st.Elapsed.Minutes > 0)
            {
                return st.Elapsed.ToString(@"mm\:ss\:ff");
            }
            else
            {
                return st.Elapsed.ToString(@"ss\:ff");
            }
        }
    }

    Subject<TimeSpan> onTimeElapsed = new Subject<TimeSpan>();
    public IObservable<TimeSpan> OnTimeElapsed => onTimeElapsed;

    private Timer timer;

    public void Init(DateTime gameStartTime, int currentPlayerID)
    {
        this.currentPlayerID = currentPlayerID;
        this.gameStartTime = gameStartTime;
    }

    public void StartGame()
    {
        st.Reset();
        st.Start();
        timer = new Timer(timerGranularity);
        timer.Elapsed += (s, e) =>
        {
            onTimeElapsed.OnNext(st.Elapsed);
        };
        timer.Start();
    }

    public void EndGame()
    {
        timer.Stop();
        timer.Dispose();
        st.Stop();

        string timeToSave = (gameStartTime + st.Elapsed).ToString("yyyy-MM-dd HH:mm:ss.fff");

        var connection = new SqliteConnection(string.Format(@"Data Source={0};Version=3",
                        Application.streamingAssetsPath + "\\" + "Players.db"));
        var context = new DataContext(connection);

        if (context.DatabaseExists())
        {
            var query = @"UPDATE PlayerData SET EndTime={0} WHERE ID={1}";
            context.ExecuteCommand(query, timeToSave, currentPlayerID);
        }

        context.Dispose();
        connection.Close();
        connection.Dispose();
    }
}