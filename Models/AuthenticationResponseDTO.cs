using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AuthenticationResponseDTO
    {
        public bool isAuthSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public string Token { get; set; }
        public UserDTO UserDTO { get; set; }
    }
}
