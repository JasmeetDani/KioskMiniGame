using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityWeld.Binding;

[Binding]
public class LeaderboardViewModel : ViewModelBase<LeaderboardViewModel, LeaderboardController>, IPointerDownHandler
{
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

    public void OnPointerDown(PointerEventData eventData)
    {
        controller.OnNext();
    }
}