namespace ScratchProject.Api.DataTransferObjects
{
    public class UserAuthenticationSuccessResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryTime{ get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
