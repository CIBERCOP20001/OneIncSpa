using OneInc.Common.Model;
using StackExchange.Redis;
using System.Text.Json;

namespace OneInc.DAL.Repository
{
    public class EncodedStringsRepo : IEncodedStringsRepo
    {
        private readonly IConnectionMultiplexer _redisConn;

        public EncodedStringsRepo(IConnectionMultiplexer redisConn)
        {
            _redisConn = redisConn;
        }
        public void CreatedEncodeString(EncodedString encodedString)
        {
            if (encodedString == null)
            {
                throw new ArgumentOutOfRangeException(nameof(encodedString));
            }

            var db = _redisConn.GetDatabase();

            var serializedObj = JsonSerializer.Serialize(encodedString);

            db.StringSet(encodedString.Id, serializedObj, new TimeSpan(1, 0, 0));
        }

        public EncodedString? GetEncodedStringById(string id)
        {
            if (id == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var db = _redisConn.GetDatabase();

            var encodedString = db.StringGet(id);

            if (!string.IsNullOrEmpty(encodedString))
            {
                return JsonSerializer.Deserialize<EncodedString>(encodedString);
            }
            return null;
        }
    }
}
