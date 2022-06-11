namespace CinemaNVS.DAL.Database.Entities.Transactions
{
    public class BookingSeating
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int SeatingId { get; set; }
        public Booking Booking { get; set; }
        public Seating Seating { get; set; }
    }
}
