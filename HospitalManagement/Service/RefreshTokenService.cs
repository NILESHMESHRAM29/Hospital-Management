using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace HospitalManagement.Services
{
    public class RefreshTokenService
    {
        private readonly IDatabase _redisDb;

        public RefreshTokenService(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        // Save refresh token (expires in 7 days)
        public async Task SaveRefreshTokenAsync(string userId, string refreshToken)
        {
            string key = $"refresh_token:{userId}";
            await _redisDb.StringSetAsync(key, refreshToken, TimeSpan.FromDays(7));
        }

        // Get refresh token
        public async Task<string?> GetRefreshTokenAsync(string userId)
        {
            string key = $"refresh_token:{userId}";
            return await _redisDb.StringGetAsync(key);
        }

        // Delete refresh token (logout)
        public async Task DeleteRefreshTokenAsync(string userId)
        {
            string key = $"refresh_token:{userId}";
            await _redisDb.KeyDeleteAsync(key);
        }
    }
}
