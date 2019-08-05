using Library.Models.Enums;

namespace Library.Models.Contracts
{
    public interface IBook
    {
        int ID { get; }
        string Author { get; }
        string Title { get; }
        string ISBN { get; }
        string Genre { get; }
        string Publisher { get; }
        int Year { get; }
        int Rack { get; }
        BookStatus Status { get; set; }
    }
}
