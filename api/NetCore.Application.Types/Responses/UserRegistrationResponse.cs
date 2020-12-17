using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace NetCore.Application.Types
{
    [DataContract]
    public class UserRegistrationResponse
    {
        [DataMember(Name = "isCreated")]
        [JsonPropertyName("isCreated")]
        public bool IsCreated { get; set; }

        [DataMember(Name = "userName")]
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [DataMember(Name = "registrationDate")]
        [JsonPropertyName("registrationDate")]
        public DateTime RegistrationDate { get; set; }
    }
}
