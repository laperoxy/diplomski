
public class TokenTimeResponse
{
    public string Token;

    public long TimePlayed;

    public TokenTimeResponse(string token, long timePlayed)
    {
        Token = token;
        TimePlayed = timePlayed;
    }
}
