using ObjectsFall.Model;
using System.Collections.Generic;

namespace ObjectsFall.Database
{
    public class GameRecordRepository : ObjectsFallRepository
    {
        public GameRecordRepository()
        {

        }

        public List<GameRecord> GameRecords()
        {
            return _database.Query<GameRecord>("SELECT * FROM GameRecord");
        }
      
        public GameRecord GetGameRecordById(int id)
        {
            return _database.FindWithQuery<GameRecord>("SELECT * FROM collection WHERE id = :id", id);
        }

        public void UpdateScore(int id, int score)
        {
            _database.Execute("UPDATE collection SET score = :score WHERE id = :id", score,id); 
        }

        public void SaveGameRecord(GameRecord gameRecord)
        {
             _database.Insert(gameRecord);
        }
        public void DeleteGameRecord(GameRecord gameRecord)
        {
            _database.Delete(gameRecord);
        }
    }
}
