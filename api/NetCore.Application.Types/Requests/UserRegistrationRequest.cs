using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace NetCore.Application.Types
{
    [DataContract]
    public class UserRegistrationRequest
    {
        [DataMember(Name = "firstName")]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "email")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [DataMember(Name = "password")]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
