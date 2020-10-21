using Mono.Data.Sqlite;
using System.Data.Linq;
using UnityEngine;
using Zenject;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

public class DataCollectionController : ControllerBase<DataCollectionController, DataCollectionViewModel>
{
    [Inject]
    private CountdownController next;
    [Inject]
    private GameController gameController;

    public override void OnNext()
    {
        if (!viewModel.HasErrors())
        {
            Disable();
            SavePayerDetails();
            next.Enable();
        }
    }

    public override void OnEnable()
    {
        viewModel.OptOut.isOn = true;
        viewModel.Name.text = "";
        viewModel.Name.caretPosition = 0;
        viewModel.Email.text = "";
        viewModel.Email.caretPosition = 0;

        viewModel.Name.ActivateInputField();
        viewModel.SetFocusOnName();
    }

    private void SavePayerDetails()
    {
        var connection = new SqliteConnection(string.Format(@"Data Source={0};Version=3",
                        Application.streamingAssetsPath + "\\" + "Players.db"));
        var context = new DataContext(connection);

        if (context.DatabaseExists())
        {
            var Name = viewModel.Name.text;
            var Email = viewModel.Email.text;
            var Country = viewModel.Countries.Text;
            var OptOut = viewModel.OptOut.isOn;

            var query = @"INSERT INTO PlayerData(Name,Email,Country,OptOut,StartTime,EndTime)
                            values({0}, {1}, {2}, {3}, {4}, {5})";
            DateTime gameStartTime = DateTime.UtcNow;
            string timeToSave = gameStartTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            context.ExecuteCommand(query, Name, Email, Country, OptOut, timeToSave, timeToSave);

            query = @"SELECT ID FROM PlayerData where Email={0} ORDER BY ID";
            List<PlayerData> table = context.ExecuteQuery<PlayerData>(query , Email).ToList<PlayerData>();
            gameController.Init(gameStartTime, table.Last<PlayerData>().ID);
        }

        context.Dispose();
        connection.Close();
        connection.Dispose();
    }

    public bool DoesEmailAlreadyExist(string Email)
    {
        var connection = new SqliteConnection(string.Format(@"Data Source={0};Version=3;Read Only=True",
                       Application.streamingAssetsPath + "\\" + "Players.db"));
        var context = new DataContext(connection);

        if (context.DatabaseExists())
        {
            var query = @"SELECT ID FROM PlayerData where Email={0}";
            List<PlayerData> table = context.ExecuteQuery<PlayerData>(query, Email).ToList<PlayerData>();
            if (table.Count != 0)
            {
                context.Dispose();
                connection.Close();
                connection.Dispose();

                return true;
            }
        }

        context.Dispose();
        connection.Close();
        connection.Dispose();

        return false;
    }

    public bool IsValidEmail(string Email)
    {
        string theEmailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                   + "@"
                                   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";
        return Regex.IsMatch(Email, theEmailPattern);
    }

    public bool IsValidCountry(string Country)
    {
        return viewModel.Countries.AvailableOptions.Contains(Country);
    }
}