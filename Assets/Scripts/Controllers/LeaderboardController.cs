using Mono.Data.Sqlite;
using System.Data.Linq;
using UniRx;
using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Linq;
using System;

public class LeaderboardController : ControllerBase<LeaderboardController, LeaderboardViewModel>
{
    [Inject]
    private StartScreenController startScreenController;

    public override void OnNext()
    {
        startScreenController.OnNext();
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
            foreach(var row in table)
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
                    Timing = duration
                });
            }
        }

        context.Dispose();
        connection.Close();
        connection.Dispose();

        return leaderboardItems;
    }
}