using System;
using System.Data.Linq.Mapping;

[Table]
public class PlayerData
{
    [Column(IsPrimaryKey = true)]
    public int ID;

    [Column]
    public string Name;

    [Column]
    public string Email;

    [Column]
    public string Country;

    [Column]
    public DateTime StartTime;

    [Column]
    public DateTime EndTime;

    [Column]
    public bool OptOut;

    public float Duration;
}