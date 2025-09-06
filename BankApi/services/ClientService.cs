using BankApi.Data;
using BankApi.DTOs;
using BankApi.Entities;
using BankApi.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Services
{
    public class ClientService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ClientService> _logger;

        public ClientService(AppDbContext context, ILogger<ClientService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ClientDTO>> GetAllAsync(int page, int pageSize)
        {
            _logger.LogInformation("Запрос на получение всех клиентов (page {page}, pageSize {pageSize})", page, pageSize);

            var query = _context.Clients
                .Include(c => c.BankCards)
                .AsQueryable();

            var clients = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (clients.Count == 0)
                _logger.LogWarning("Клиенты не найдены");

            return clients.Select(c => new ClientDTO
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Role = c.Role,
                BankCardIds = c.BankCards.Select(b => b.Id).ToList()
            }).ToList();
        }

        public async Task<ClientDTO> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Запрос на получение клиента по ID");

            var client = await _context.Clients
                .Include(c => c.BankCards)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                _logger.LogWarning($"Клиент с Id {id} не найден");
                throw new KeyNotFoundException("Клиент не найден");
            }

            return new ClientDTO
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Role = client.Role,
                BankCardIds = client.BankCards.Select(b => b.Id).ToList(),
            };
        }

        public async Task<ClientDTO> PostAsync(PostClientDTO postClientDTO)
        {
            if (postClientDTO == null)
                throw new ArgumentNullException(nameof(postClientDTO), "Client data is required");

            _logger.LogInformation("Запрос на создание нового клиента");

            var client = new Client
            {
                Id = Guid.NewGuid(),
                Name = postClientDTO.Name,
                Email = postClientDTO.Email,
                Password = postClientDTO.Password,
                Role = Role.Client 
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Клиент создан успешно с Id {ClientId}", client.Id);

            return new ClientDTO
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Role = client.Role,
                BankCardIds = client.BankCards?.Select(b => b.Id).ToList() ?? new List<Guid>()
            };
        }


        public async Task<ClientDTO> PutAsync(Guid id, PutClientDTO putClientDTO, string userRole)
        {
            var oldClient = await _context.Clients.FindAsync(id) ?? throw new KeyNotFoundException($"Client with Id {id} not found");
            oldClient.Name = putClientDTO.Name ?? oldClient.Name;
            oldClient.Email = putClientDTO.Email ?? oldClient.Email;

            if (userRole == "Admin")
            {
                oldClient.Role = putClientDTO.Role ?? oldClient.Role;
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("Клиент с Id {ClientId} обновлён успешно", oldClient.Id);

            return new ClientDTO
            {
                Id = oldClient.Id,
                Name = oldClient.Name,
                Email = oldClient.Email,
                Role = oldClient.Role,
                BankCardIds = oldClient.BankCards?.Select(b => b.Id).ToList() ?? new List<Guid>()
            };
        }
        public async Task<bool> DeleteAsync (Guid id)
        {
            var delClient = await _context.Clients.FindAsync(id);
            if (delClient == null)
            {
                _logger.LogWarning("Попытка удалить клиента с Id {ClientId}, но он не найден.", id);
                throw new KeyNotFoundException($"Client with Id {id} not found");
            }
                

            _context.Clients.Remove(delClient);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Client with Id {ClientId} removed.", id);

            return true;

        }
    }
}
