using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeHubGarage.Domain
{
    public class AuthenticationResponse
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresIn { get; set; }
        public bool Success { get; set; }

        [NotMapped]
        public IEnumerable<string> Errors { get; set; }
    }
}