using Mono.Data.Sqlite;
using System.Data.Linq;
using UniRx;
using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Linq;
using System;

public class FinalLeaderboardController : ControllerBase<FinalLeaderboardController, FinalLeaderboardViewModel>
{
    [Inject]
    private ThankYouController next;
    [Inject]
    private GameController gameController;

    public override void OnNext()
    {
        Disable();
        next.Enable();
    }

    public override void OnEnable()
    {
        var playerData = GetTop10Players();
        if (playerData.Count > 0)
        {
            viewModel.LeaderboardItems = playerData;
        }
    }

    private List<PlayerLeaderboardDetail> GetTop10Players()
    {
        var leaderboardItems = new List<PlayerLeaderboardDetail>();

        var connection = new SqliteConnection(string.Format(@"Data Source={0};Version=3;Read Only=True",
                        Application.streamingAssetsPath + "\\" + "Players.db"));

        var context = new DataContext(connection);

        if (context.DatabaseExists())
        {
            var query = @"SELECT *, (JulianDay(EndTime) - JulianDay(StartTime))*86400 as Duration
                        FROM PlayerData order by Duration LIMIT 10";

            List<PlayerData> table = context.ExecuteQuery<PlayerData>(query).ToList<PlayerData>();
            int i = 1;
            foreach (var row in table)
            {
                TimeSpan time = row.EndTime - row.StartTime;
                string duration;
                if (time.Hours > 0)
                {
                    duration = time.ToString(@"hh\:mm\:ss\:ff");
                }
                else if (time.Minutes > 0)
                {
                    duration = time.ToString(@"mm\:ss\:ff");
                }
                else
                {
                    duration = time.ToString(@"ss\:ff");
                }

                leaderboardItems.Add(new PlayerLeaderboardDetail
                {
                    Name = row.Name,
                    Rank = i++,
                    Timing = duration,
                    Highlight = (row.ID == gameController.CurrentPlayerID)
                });
            }

            var rec = table.Where(entry => entry.ID == gameController.CurrentPlayerID).SingleOrDefault();
            if(rec != null)
            {
                viewModel.heading.text = "YOU MADE IT TO TOP 10 !!!";
            }
            else
            {
                viewModel.heading.text = "TODAY'S LEADERBOARD";
            }
        }

        context.Dispose();
        connection.Close();
        connection.Dispose();

        return leaderboardItems;
    }
}