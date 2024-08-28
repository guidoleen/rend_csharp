using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD_Enviornment.Services
{
    internal interface IEncryptionService
    {
        IEncryptionService SetEncryptionKey(string key);
        IEncryptionService Encrypt(byte[] data);
        IEncryptionService Decrypt(byte[] data);
    }
}
