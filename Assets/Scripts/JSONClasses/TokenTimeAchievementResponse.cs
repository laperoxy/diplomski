
public class TokenTimeAchievementResponse
{
    public string Token;

    public long TimePlayed;

    public long Achievements;

    public long GamesPlayed;

    public TokenTimeAchievementResponse(string token, long timePlayed, long achievements, long gamesPlayed)
    {
        Token = token;
        TimePlayed = timePlayed;
        Achievements = achievements;
        GamesPlayed = gamesPlayed;
    }
}
