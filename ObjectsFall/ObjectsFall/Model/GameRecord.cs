using SQLite;

namespace ObjectsFall.Model
{
    [Table("GameRecord")]
    public class GameRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Total { get; set; }
    }
}
