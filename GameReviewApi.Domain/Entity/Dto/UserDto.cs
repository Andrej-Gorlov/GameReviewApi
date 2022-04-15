using GameReviewApi.Domain.Entity.Authenticate;
using System.Text.Json.Serialization;

namespace GameReviewApi.Domain.Entity.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }

        public string? Email { get; set; }
        public Role Role { get; set; }
    }
}
