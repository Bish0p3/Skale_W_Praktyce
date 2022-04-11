using SQLite;

namespace Skale_W_Praktyce.Models
{
    public class Bookmark
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string ScaleName { get; set; }
    }
}
