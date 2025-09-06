using BankApi.Enums;

namespace BankApi.DTOs
{
    public class PutClientDTO
    {
        // Nullable, ����� ��������� ������ ���� ������ ����� ��������
        public string? Name { get; set; }
        public string? Email { get; set; }

        // Nullable, ����� ����� ������, ������� ������������ ���
        public Role? Role { get; set; }
    }
}
