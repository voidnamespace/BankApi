namespace BankApi.DTOs
{
    // DTO ��� �������� ������ ������� ����� POST
    public class PostClientDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // ������ ����� ������������ � ������� ����� �����������
        public string Password { get; set; } = string.Empty;
    }
}
