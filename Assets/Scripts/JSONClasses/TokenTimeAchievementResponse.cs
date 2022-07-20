
public class TokenTimeAchievementResponse
{
    public string Token;

    public long TimePlayed;

    public long Achievements;

    public TokenTimeAchievementResponse(string token, long timePlayed, long achievements)
    {
        Token = token;
        TimePlayed = timePlayed;
        Achievements = achievements;
    }
}
