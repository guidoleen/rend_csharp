using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD_Enviornment.Services
{
    internal class ViesBtwService
    {
        internal string createViesBtwApiHeaderCode(string viesApiKeyId, string viesApiKey) => Convert.ToBase64String(
                System.Text.Encoding.ASCII.GetBytes($"{viesApiKeyId}:{viesApiKey}")
            );
    }
}
