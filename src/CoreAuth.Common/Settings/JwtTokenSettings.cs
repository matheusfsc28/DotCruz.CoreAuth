namespace CoreAuth.Common.Settings
{
    public class JwtTokenSettings
    {
        public uint ExpirationTimeMinutes { get; set; }
        public uint RefreshTokenExpirationTimeDays { get; set; }
        public string SigningKey { get; set; } = string.Empty;
    }
}
