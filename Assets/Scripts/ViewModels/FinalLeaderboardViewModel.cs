using System.Collections.Generic;
using TMPro;
using UnityWeld.Binding;

[Binding]
public class FinalLeaderboardViewModel : ViewModelBase<FinalLeaderboardViewModel, FinalLeaderboardController>
{
    public TextMeshProUGUI heading;


    private List<PlayerLeaderboardDetail> leaderboardItems = new List<PlayerLeaderboardDetail>();
    [Binding]
    public List<PlayerLeaderboardDetail> LeaderboardItems
    {
        get => leaderboardItems;
        set
        {
            if (leaderboardItems == value) return;
            leaderboardItems = value;
            OnPropertyChanged();
        }
    }

    [Binding]
    public void OnNext()
    {
        controller.OnNext();
    }
}