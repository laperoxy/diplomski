namespace JSONClasses
{ 
    [System.Serializable]
    public class LoginData
    {
        public string Username { get; set;}
        
        public TokenTimeResponse TokenTimeResponse { get; set;}

        public LoginData(string username, TokenTimeResponse tokenTimeResponse)
        {
            Username = username;
            TokenTimeResponse = tokenTimeResponse;
        }
    }
}