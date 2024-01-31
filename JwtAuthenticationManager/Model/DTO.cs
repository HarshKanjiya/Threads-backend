using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAuthenticationManager.Model
{
    public class GenerateTokenRequestDTO
    {
        public string UserName { get; set; }
        public string Role { get; set; }
    }
    public class TokenResponseDTO
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
    }

}
