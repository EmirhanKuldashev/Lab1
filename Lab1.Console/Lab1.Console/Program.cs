using System;
using Lab1.Core;

namespace Lab1.ConsoleApp
{
    class Program
    {
        static BookLogic bookLogic = new BookLogic();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Система управления книгами";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== СИСТЕМА УПРАВЛЕНИЯ КНИГАМИ ===");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Показать все книги");
                Console.WriteLine("3. Найти книгу по ID");
                Console.WriteLine("4. Обновить книгу");
                Console.WriteLine("5. Удалить книгу");
                Console.WriteLine("6. Группировать по жанру");
                Console.WriteLine("7. Найти книги по автору");
                Console.WriteLine("8. Статистика");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddBook(); break;
                    case "2": ShowAllBooks(); break;
                    case "3": FindBookById(); break;
                    case "4": UpdateBook(); break;
                    case "5": DeleteBook(); break;
                    case "6": GroupByGenre(); break;
                    case "7": FindByAuthor(); break;
                    case "8": ShowStatistics(); break;
                    case "0":
                        Console.WriteLine("До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        static void AddBook()
        {
            Console.WriteLine("\n=== ДОБАВЛЕНИЕ НОВОЙ КНИГИ ===");

            try
            {
                Console.Write("Введите ID книги: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Введите название: ");
                string title = Console.ReadLine();

                Console.Write("Введите автора: ");
                string author = Console.ReadLine();

                Console.Write("Введите год издания: ");
                int year = int.Parse(Console.ReadLine());

                Console.Write("Введите жанр: ");
                string genre = Console.ReadLine();

                bookLogic.Create(id, title, author, year, genre);
                Console.WriteLine("Книга успешно добавлена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ShowAllBooks()
        {
            Console.WriteLine("\n=== ВСЕ КНИГИ В БИБЛИОТЕКЕ ===");

            var allBooks = bookLogic.ReadAll();

            if (allBooks.Count == 0)
            {
                Console.WriteLine("Библиотека пуста!");
                return;
            }

            foreach (var book in allBooks)
            {
                Console.WriteLine(book.ToString());
            }

            Console.WriteLine($"\nВсего книг: {allBooks.Count}");
        }

        static void FindBookById()
        {
            Console.WriteLine("\n=== ПОИСК КНИГИ ПО ID ===");

            try
            {
                Console.Write("Введите ID книги: ");
                int id = int.Parse(Console.ReadLine());

                var book = bookLogic.Read(id);

                if (book != null)
                    Console.WriteLine($"Найдена: {book}");
                else
                    Console.WriteLine("Книга с таким ID не найдена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void UpdateBook()
        {
            Console.WriteLine("\n=== ОБНОВЛЕНИЕ ДАННЫХ КНИГИ ===");

            try
            {
                Console.Write("Введите ID книги для обновления: ");
                int id = int.Parse(Console.ReadLine());

                var existingBook = bookLogic.Read(id);
                if (existingBook == null)
                {
                    Console.WriteLine("Книга с таким ID не найдена!");
                    return;
                }

                Console.WriteLine($"Текущие данные: {existingBook}");
                Console.WriteLine("\nВведите новые данные:");

                Console.Write("Новое название: ");
                string title = Console.ReadLine();

                Console.Write("Новый автор: ");
                string author = Console.ReadLine();

                Console.Write("Новый год: ");
                int year = int.Parse(Console.ReadLine());

                Console.Write("Новый жанр: ");
                string genre = Console.ReadLine();

                bool success = bookLogic.Update(id, title, author, year, genre);

                if (success)
                    Console.WriteLine("Книга успешно обновлена!");
                else
                    Console.WriteLine("Ошибка при обновлении!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void DeleteBook()
        {
            Console.WriteLine("\n=== УДАЛЕНИЕ КНИГИ ===");

            try
            {
                Console.Write("Введите ID книги для удаления: ");
                int id = int.Parse(Console.ReadLine());

                bool success = bookLogic.Delete(id);

                if (success)
                    Console.WriteLine("Книга успешно удалена!");
                else
                    Console.WriteLine("Книга с таким ID не найдена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void GroupByGenre()
        {
            Console.WriteLine("\n=== ГРУППИРОВКА КНИГ ПО ЖАНРУ ===");

            var groupedBooks = bookLogic.GroupByGenre();

            if (groupedBooks.Count == 0)
            {
                Console.WriteLine("Нет книг для группировки!");
                return;
            }

            foreach (var genreGroup in groupedBooks)
            {
                Console.WriteLine($"\nЖАНР: {genreGroup.Key} ({genreGroup.Value.Count} книг)");
                foreach (var book in genreGroup.Value)
                {
                    Console.WriteLine($"   {book.Title} - {book.Author}");
                }
            }
        }

        static void FindByAuthor()
        {
            Console.WriteLine("\n=== ПОИСК КНИГ ПО АВТОРУ ===");

            Console.Write("Введите имя автора: ");
            string author = Console.ReadLine();

            var authorBooks = bookLogic.FindByAuthor(author);

            if (authorBooks.Count == 0)
            {
                Console.WriteLine($"Книги автора '{author}' не найдены!");
                return;
            }

            Console.WriteLine($"\nКниги автора '{author}':");
            foreach (var book in authorBooks)
            {
                Console.WriteLine($"   {book}");
            }
        }

        static void ShowStatistics()
        {
            Console.WriteLine("\n=== СТАТИСТИКА БИБЛИОТЕКИ ===");

            int totalBooks = bookLogic.GetBooksCount();
            Console.WriteLine($"Всего книг в библиотеке: {totalBooks}");

            if (totalBooks > 0)
            {
                var grouped = bookLogic.GroupByGenre();
                Console.WriteLine("\nРаспределение по жанрам:");
                foreach (var genre in grouped)
                {
                    Console.WriteLine($"   {genre.Key}: {genre.Value.Count} книг");
                }
            }
        }
    }
}

