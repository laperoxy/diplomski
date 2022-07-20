
public class LoginData
{
    public string Username;
    
    public string Token;

    public long TimePlayed;

    public long Achievements;

    public LoginData(string username, string token, long timePlayed, long achievements)
    {
        Username = username;
        Token = token;
        TimePlayed = timePlayed;
        Achievements = achievements;
    }

    public LoginData(string username, TokenTimeAchievementResponse tokenTimeAchievementResponse)
    {
        Username = username;
        Token = tokenTimeAchievementResponse.Token;
        TimePlayed = tokenTimeAchievementResponse.TimePlayed;
        Achievements = tokenTimeAchievementResponse.Achievements;
    }
    
}
