using BankApi.Enums;

namespace BankApi.DTOs
{
    public class ClientDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.Client;

        // ������ Id ���������� ���� �������
        public List<Guid> BankCardIds { get; set; } = new List<Guid>();
    }
}
