using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD_Enviornment.Services
{
    internal class EncryptionService : IEncryptionService
    {
        private byte[]? _encryptedData;
        private byte[]? _decryptedData;
        private string _encryptionKey { get; set; } = string.Empty;
        public IEncryptionService SetEncryptionKey(string key)
        {
            this._encryptionKey = key; 
            return this;
        }

        public byte[] GetEncryptedData()
        {
            return _encryptedData ?? new byte[] { };
        }
        
        public byte[] GetDecryptedData()
        {
            return _decryptedData ?? new byte[] { };
        }

        public IEncryptionService Encrypt(byte[] data)
        {
            var totalLength = data.Length + _encryptionKey.Length;
            byte[] encryptedData = new byte[totalLength];

            var reversedData = data; // data.Reverse().ToArray();
            var currentByte = new byte();

            for (int i = 0, j = 0, k = 0; i < totalLength; i++)
            {
                if((i % 2) == 0)
                {
                    if (j < _encryptionKey.Length)
                    {
                        currentByte = (byte) _encryptionKey[j];
                        j++;
                    }
                }
                else
                {
                    if(k < data.Length)
                    {
                        currentByte = reversedData[k];
                        k++;
                    }
                }
                encryptedData[i] = currentByte;
            }

            this._encryptedData = encryptedData;

            return this;
        }

        public IEncryptionService Decrypt(byte[] data)
        {
            var totalLenght = data.Length; // - _encryptionKey.Length;
            byte[] decryptedData = new byte[totalLenght];

            for (int i = 0, j = 0; i < data.Length; i++)
            {
                if (i % 2 != 0)
                {
                    decryptedData[j] = data[i];
                    j++;
                }
            }

            var reversedData = decryptedData.Reverse().ToArray();
            this._decryptedData = reversedData;

            return this;
        }
    }
}
