using System.Collections.Generic;

namespace Lindholm.Webshop2021.WebApi.Dtos.Auth
{
    public class ProfileDto
    {
        public List<string> Permissions { get; set; }
        public string Name { get; set; }
    }
}