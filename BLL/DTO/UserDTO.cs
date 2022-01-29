namespace BLL.DTO
{
    /// <summary>
    /// Data tranfer object for User model
    /// </summary>
    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
