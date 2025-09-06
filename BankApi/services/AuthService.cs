using BankApi.DTOs;
using BankApi.Entities;

namespace BankApi.Services
{
    public class AuthService
    {
        private readonly ClientService _clientService;
        private readonly PasswordService _passwordService;
        private readonly TokenService _tokenService;

        public AuthService(ClientService clientService, PasswordService passwordService, TokenService tokenService)
        {
            _clientService = clientService;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var client = await _clientService.GetByEmailAsync(email);
            if (client == null)
                throw new UnauthorizedAccessException("Пользователь не найден");

            if (!_passwordService.VerifyPassword(client.Password, password))
                throw new UnauthorizedAccessException("Неверный пароль");

            var token = _tokenService.GenerateToken(client);
            return token;
        }
    }
}
