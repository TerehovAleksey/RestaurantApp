namespace Domain
{
    /// <summary>
    /// Данные, которые необходимы для аутентификации и получения токена
    /// </summary>
    public class LoginCredentialsDto
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
