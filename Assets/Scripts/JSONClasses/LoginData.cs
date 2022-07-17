
public class LoginData
{
    public string Username;
    
    public string Token;

    public long TimePlayed;

    public LoginData(string username, string token, long timePlayed)
    {
        Username = username;
        Token = token;
        TimePlayed = timePlayed;
    }

    public LoginData(string username, TokenTimeResponse tokenTimeResponse)
    {
        Username = username;
        Token = tokenTimeResponse.Token;
        TimePlayed = tokenTimeResponse.TimePlayed;
    }
    
}
