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
        System.DateTime CheckoutDate { get; set; }
        System.DateTime DueDate { get; set; }
        System.DateTime ResevedDate { get; set; }
    }
}
