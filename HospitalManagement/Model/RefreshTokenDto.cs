namespace HospitalManagement.Model
{
    public class RefreshTokenDto
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public int UserId { get; set; }
    }
}
