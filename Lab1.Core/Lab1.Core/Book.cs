namespace Lab1.Core
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }

        /// <summary>
        /// Создает книгу с указанными параметрами
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="year"></param>
        /// <param name="genre"></param>
        public Book(int id, string title, string author, int year, string genre)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            Genre = genre;
        }

        /// <summary>
        /// Возвращает строковое представление книги
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Id}: '{Title}' - {Author} ({Year}г.) {Genre}";
        }
    }
}
