using Microsoft.Azure.KeyVault.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IKeyVault
    {
        Task<SecretBundle> GetSecretAsync(string secret);
    }
}
