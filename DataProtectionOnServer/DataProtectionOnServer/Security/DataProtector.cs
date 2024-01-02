using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;

namespace DataProtectionOnServer.Security
{
    public class DataProtector
    {
        private string path;
        private byte[] entropy = new byte[16];

        public DataProtector(string path)
        {
            this.path = path;
            entropy = RandomNumberGenerator.GetBytes(16);
        }



        public int EncryptedData(string value)
        {
            var encodedData = Encoding.UTF8.GetBytes(value);
            var fileStram = new FileStream(path, FileMode.OpenOrCreate);
            int length = encryptDataToFile(encodedData, entropy, DataProtectionScope.CurrentUser, fileStram);
            fileStram.Close();
            return length;
        }

        int encryptDataToFile(byte[] userData, byte[] entropy, DataProtectionScope dataProtectionScope, FileStream fileStream)
        {
            //switch (dataProtectionScope)
            //{
            //    case DataProtectionScope.CurrentUser:
            //        break;
            //    case DataProtectionScope.LocalMachine:
            //        break;
            //    default:
            //        break;
            //}
            var encrypted = ProtectedData.Protect(userData, entropy, dataProtectionScope);
            if (fileStream.CanWrite && encrypted != null)
            {
                fileStream.Write(encrypted, 0, encrypted.Length);

            }
            return encrypted.Length;

        }

        public string DecryptData(int length)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            byte[] decrypt = decryptFromFile(fileStream, entropy, DataProtectionScope.CurrentUser, length);
            fileStream.Close();
            return Encoding.UTF8.GetString(decrypt);
        }

        private byte[] decryptFromFile(FileStream fileStream, byte[] entropy, DataProtectionScope currentUser, int length)
        {
            var input = new byte[length];
            var output = new byte[length];

            if (fileStream.CanRead)
            {
                fileStream.Read(input, 0, input.Length);
                output = ProtectedData.Unprotect(input, entropy, currentUser);

            }

            return output;
        }
    }
}
