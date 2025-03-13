using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.DTOs.Requests.Auth
{
    public class AuthRequestDto
    {
        public required string CredentialCode { get; set; }
    }
}