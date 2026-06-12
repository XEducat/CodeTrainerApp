# CodeTrainer 💻
### Інтерактивна система діагностування вмінь при складанні алгоритмів

**CodeTrainer** — це сучасна навчально-діагностична платформа, призначена для автоматизованої перевірки алгоритмічних навичок студентів. Проєкт поєднує в собі потужність **Roslyn Scripting API** для динамічного виконання коду та гнучкість архітектури на базі **.NET**.

---

## 🛠 Технологічний стек

- **Core:** .NET 10.0+ / C#
- **Execution Engine:** Microsoft.CodeAnalysis.CSharp.Scripting (Roslyn)
- **Architecture:** Web API (REST) + Desktop Client (WinForms)
- **Database & ORM:** MS SQL Server + Entity Framework Core
- **Security:** IdentityServer / JWT Auth
- **Testing:** xUnit + Moq

---

## ✨ Основні можливості

- 📝 **Інтерактивні квізи:** Тестування теоретичних знань з алгоритмізації.
- ⚙️ **Code Sandbox:** Написання та виконання C# коду в реальному часі.
- 🔍 **Автоматична верифікація:** Миттєва перевірка правильності алгоритмів на базі набору тест-кейсів.
- 📊 **Звітність:** Детальний аналіз помилок та продуктивності коду (час виконання, використання пам'яті).
- 🔐 **Рольова модель:** Окремі кабінети для студентів (проходження завдань) та менторів (управління контентом).


## 🚀 Як запустити
Цей пункт містить перелік необхідних інструментів та кроки для локального запуску системи навчання програмуванню.

### 1. Системні вимоги
Для запуску проекту вам знадобляться:

*   **Visual Studio 2026** (версія Community або вище).
    *   При встановленні оберіть робочі навантаження:
        *   `.NET desktop development` (для WinForms додатку).
        *   `ASP.NET and web development` (для API сервера).
*   **SDK .NET 10.0+** (встановлюється разом з Visual Studio).
*   **SQL Server Express** (або будь-яка інша версія MS SQL Server).
*   **[Опціонально]* SQL Server Management Studio (SSMS)** — для перегляду бази даних.

### 2. Початкове налаштування (CLI)
Для керування міграціями бази даних потрібно встановити інструмент EF Core. Відкрийте термінал (PowerShell) та виконайте:

```powershell
dotnet tool install --global dotnet-ef
```

### 3. Кроки для запуску
Цей пункт містить перелік необхідних інструментів та кроки для локального запуску системи навчання програмуванню.

1.  **Клонування проекту:**
    Завантажте проект з GitHub або скопіюйте файли в окрему папку.

2.  **Налаштування бази даних:**
    *   Відкрийте файл `CodeTrainerAPI/appsettings.json`.
    *   Переконайтеся, що в секції `ConnectionStrings` вказано правильний шлях до вашого локального SQL Server (наприклад, `Server=(localdb)\\mssqllocaldb`).

3.  **Створення таблиць:**
    Відкрийте термінал у папці проекту `CodeTrainerAPI` та виконайте команду:
    ```powershell
    dotnet ef database update
    ```
    Це створить базу даних `CodeTrainerDB` та всі необхідні таблиці.

4.  **Запуск системи:**
    *   Відкрийте файл рішення `WinFormsApp1.slnx` (або `.sln`) у Visual Studio.
    *   Натисніть правою кнопкою миші на рішення (Solution) -> **Configure Startup Projects**.
   <img width="1411" height="205" alt="image" src="https://github.com/user-attachments/assets/29df8ffa-d77e-47b1-b0e8-d97af03ac826" />

    *   Оберіть **Multiple startup projects**:
        *   `CodeTrainerAPI` — **Start**
        *   `WinFormsApp1` — **Start**
   <img width="796" height="448" alt="image" src="https://github.com/user-attachments/assets/e3e38fc4-e65e-4b50-93cd-b490e39f94ca" />

    *   Натисніть **F5** для запуску.
---
*Розроблено в рамках дипломного проєкту бакалавра, 2026.*
