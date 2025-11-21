using StackExchange.Redis;

namespace HospitalManagement.Services
{
    public class TokenBlacklistService
    {
        private readonly IDatabase _redisDb;

        public TokenBlacklistService(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        public async Task BlacklistToken(string token)
        {
            await _redisDb.StringSetAsync($"blacklist:{token}", "1", TimeSpan.FromHours(1));
        }

        public async Task<bool> IsTokenBlacklisted(string token)
        {
            return await _redisDb.KeyExistsAsync($"blacklist:{token}");
        }
    }
}
