using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Core
{
    internal class BookLogic
    {
        public List<Book> books = new List<Book>();

        public void Create(int id,string title, string author, int year, string genre)
        {
            var book = new Book(id, title, author, year, genre);
            books.Add(book);
        }

        public Book Read(int id)
        {
            // Ищем книгу по ID
            foreach (var book in books)
            {
                if (book.Id == id)
                    return book;
            }
            return null; // Если не нашли
        }

        public List<Book> ReadAll()
        {
            return new List<Book>(books); // Возвращаем копию списка
        }

        public bool Update(int id, string title, string author, int year, string genre)
        {
            var book = Read(id);
            if (book != null)
            {
                book.Title = title;
                book.Author = author;
                book.Year = year;
                book.Genre = genre;
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var book = Read(id);
            if (book != null)
            {
                books.Remove(book);
                return true;
            }
            return false;
        }

        public Dictionary<string, List<Book>> GroupByGenre()
        {
            var result = new Dictionary<string, List<Book>>();

            foreach (var book in books)
            {
                if (!result.ContainsKey(book.Genre))
                {
                    result[book.Genre] = new List<Book>();
                }
                result[book.Genre].Add(book);
            }

            return result;
        }

        public List<Book> FindByAuthor(string author)
        {
            var result = new List<Book>();

            foreach (var book in books)
            {
                if (book.Author.Equals(author, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(book);
                }
            }

            return result;
        }

        public int GetBooksCount()
        {
            return books.Count;
        }
    }
}
