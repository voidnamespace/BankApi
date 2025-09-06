namespace BankApi.DTOs
{
    // DTO для создания нового клиента через POST
    public class PostClientDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Пароль будет хэшироваться в сервисе перед сохранением
        public string Password { get; set; } = string.Empty;
    }
}
