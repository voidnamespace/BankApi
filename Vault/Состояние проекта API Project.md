# 🏗 Banking API Project — Status & Roadmap

## 🔹 Текущее состояние ✅

### Сущности

- Client
    
- BankCard
    
- Transaction
    

### Enum’ы

- Role
    
- TransactionType
    

### DbContext

- AppDbContext с:
    
    - DbSet
        
    - DbSet
        
    - DbSet
        
- Зарегистрирован в DI (`Program.cs`)
    

### NuGet пакеты EF Core

- Microsoft.EntityFrameworkCore
    
- Microsoft.EntityFrameworkCore.Sqlite
    
- Microsoft.EntityFrameworkCore.Design
    

### Прочее

- Все изменения смёржены в main через Pull Request
    
- Тестовый проект создан, тесты пока не делались
    
- Commit Convention добавлен (`COMMIT_CONVENTION.md`)
    

---

## 🔹 Следующие шаги ⏳

### 1️⃣ Настройка связей сущностей

- 1 клиент → много карт (1:N)
    
- 1 карта → много транзакций (1:N)
    
- Настройка OnDelete и ForeignKey
    

### 2️⃣ Создание DTO для API

- ClientDTO, BankCardDTO, TransactionDTO
    
- Для POST/PUT отдельные CreateClientDTO
    

### 3️⃣ Реализация контроллеров

- CRUD для клиентов, карт и транзакций
    
- Асинхронные методы (async/await)
    
- Минимальная валидация данных
    

### 4️⃣ Добавление тестов

- Unit-тесты для сервисов и репозиториев
    
- Тестирование маппинга DTO ↔ Entity
    

### 5️⃣ Дополнительно / опционально

- Enum в базе как строка (`.HasConversion<string>()`)
    
- Авторизация / роли (если ТЗ требует)
    

---

## 🔹 Прогресс / статус задач

| Задача               | Статус | Комментарий                   |
| -------------------- | ------ | ----------------------------- |
| Сущности             | ✅      | Client, BankCard, Transaction |
| Enum’ы               | ✅      | Role, TransactionType         |
| DbContext            | ✅      | AppDbContext + DI             |
| NuGet пакеты EF Core | ✅      | Добавлены                     |
| Настройка связей     | ⏳      | Нужно сделать 1:N связи       |
| DTO                  | ⏳      | Пока не созданы               |
| Контроллеры          | ⏳      | Пока не реализованы           |
| Тесты                | ⏳      | Пока не написаны              |

---

💡 **Совет по использованию:**

- Обновляй раздел «Текущее состояние» после каждого PR или значимого коммита
    
- Ставь ✅, если задача выполнена
    
- Ставь ⏳, если задача в процессе
    
- Добавляй новые задачи в раздел «Следующие шаги» по мере развития проекта