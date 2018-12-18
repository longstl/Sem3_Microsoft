using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using ConsumingRESTServiceCRUD_Client.ServiceReference1;

namespace ConsumingRESTServiceCRUD_Client.Models
{
    public class BookServiceClient
    {
        BookServicesClient client = new BookServicesClient();
        private readonly string BASE_URL = "http://localhost:2736/BookServices.svc";

        public List<Book> getAllBooksClient()
        {
            var list = client.GetBookList().ToList();
            var rt = new List<Book>();
            list.ForEach(b => rt.Add(new Book()
            {
                BookId = b.BookId,
                ISBN = b.ISBN,
                Title = b.Title
            }));
            return rt;

            /*var syncClient = new WebClient();
            var content = syncClient.DownloadString(BASE_URL + "Books");
            var json_serialize = new JavaScriptSerializer();
            return json_serialize.Deserialize<List<Book>>(content);*/
        }

        public string AddBook(Book newBook)
        {
            var book = new ServiceReference1.Book()
            {
                BookId = newBook.BookId,
                ISBN = newBook.ISBN,
                Title = newBook.Title
            };
            return client.AddBook(book);
        }

        public string DeleteBook(int id)
        {
            return client.DeleteBook(Convert.ToString(id));
        }

        public string EditBook(Book newBook)
        {
            var book = new ServiceReference1.Book()
            {
                BookId = newBook.BookId,
                ISBN = newBook.ISBN,
                Title = newBook.Title
            };
            return client.UpdateBook(book);
        }

        public Book GetBookById(int id)
        {
            var book = client.GetBookById(Convert.ToString(id));
            var book1 = new Book()
            {
                BookId = book.BookId,
                ISBN = book.ISBN,
                Title = book.Title
            };
            return book1;
        }
    }
}