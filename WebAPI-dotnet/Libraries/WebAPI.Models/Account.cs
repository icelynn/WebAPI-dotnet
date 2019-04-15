namespace WebAPI.Models
{
    /// <summary>
    /// For <c>UserController</c> to parse the input data.
    /// </summary>
    public class AccountInfo
    {
        public string UserID { get; set; }
        public string Password { get; set; }
    }
}
