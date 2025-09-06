using BankApi.Enums;

namespace BankApi.DTOs
{
    public class PutClientDTO
    {
        // Nullable, чтобы обновлять только если пришло новое значение
        public string? Name { get; set; }
        public string? Email { get; set; }

        // Nullable, админ может менять, обычный пользователь нет
        public Role? Role { get; set; }
    }
}
