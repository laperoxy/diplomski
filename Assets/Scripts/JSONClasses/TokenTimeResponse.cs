namespace JSONClasses
{
    [System.Serializable]
    public class TokenTimeResponse
    {
        public string Token { get; set; }
        
        public long TimePlayed { get; set;}

        public TokenTimeResponse(string token = "", long timePlayed = 0)
        {
            Token = token;
            TimePlayed = timePlayed;
        }
    }
}