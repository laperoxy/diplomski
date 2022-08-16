
public class LoginData
{
    public string Username;
    
    public string Token;

    public long TimePlayed;

    public long Achievements;

    public long GamesPlayed;

    public LoginData(string username, string token, long timePlayed, long achievements, long gamesPlayed)
    {
        Username = username;
        Token = token;
        TimePlayed = timePlayed;
        Achievements = achievements;
        GamesPlayed = gamesPlayed;
    }

    public LoginData(string username, TokenTimeAchievementResponse tokenTimeAchievementResponse)
    {
        Username = username;
        Token = tokenTimeAchievementResponse.Token;
        TimePlayed = tokenTimeAchievementResponse.TimePlayed;
        Achievements = tokenTimeAchievementResponse.Achievements;
        GamesPlayed = tokenTimeAchievementResponse.GamesPlayed;
    }
    
}
