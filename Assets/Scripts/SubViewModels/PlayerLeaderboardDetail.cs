using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class PlayerLeaderboardDetail : SubViewModelBase
{
    [Binding]
    public int Rank { get; set; }

    [Binding]
    public string Name { get; set; }

    [Binding]
    public string Timing { get; set; }

    [Binding]
    public bool Highlight { get; set; }

    [Binding]
    public Color ForeColor
    {
        get
        {
            if (Highlight)
                return Color.black;
            else
                return Color.white;
        }
    }
}