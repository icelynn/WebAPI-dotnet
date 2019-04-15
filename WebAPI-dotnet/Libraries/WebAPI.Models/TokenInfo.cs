namespace WebAPI.Models
{
    /// <summary>
    /// For <c>TokenController</c> to parse the input data.
    /// </summary>
    public class TokenInfo
    {
        public string UserGUID { get; set; }
        public string Insurer { get; set; }
    }
}
