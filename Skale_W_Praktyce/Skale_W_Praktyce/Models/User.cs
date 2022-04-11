using SQLite;

namespace Skale_W_Praktyce.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Username { get; set; }
        public string UserImage { get; set; }
    }
}
