using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration
{
    public enum DeCodeType { NONE, Base64 }

    /// <summary>
    /// 提供Configuration 取得連線字串(Base64)
    /// </summary>
    /// <param name="ConnectionName">連線字串名稱</param>
    /// <returns>string</returns>
    public static class ConfigExtension
    {
        public static string? GetConnectionString(this IConfiguration configuration, string name, DeCodeType deCodeType = DeCodeType.Base64)
        {
            string? result;

            try
            {
                if (deCodeType == DeCodeType.Base64)
                    result = Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(Base64DeCode(configuration?.GetConnectionString(name), Encoding.UTF8)));
                else
                    result = configuration?.GetConnectionString(name);
            }
            catch (Exception)
            {
                throw;
            }
             
            return result;
        }

        /// <summary>
        /// 用Base64進行解碼
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        private static SecureString Base64DeCode(string value, Encoding encode)
        {
            byte[] bytes = Convert.FromBase64String(value);
            SecureString secureString = new SecureString();
            foreach (char ch in encode.GetString(bytes).ToCharArray())
                secureString.AppendChar(ch);
            secureString.MakeReadOnly();

            return secureString;
        }
    }
}
