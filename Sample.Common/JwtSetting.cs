namespace Sample.Common
{
    public class JwtSettingModel
    {
        public int ExpireIn { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Secret { get; set; }
    }
}
