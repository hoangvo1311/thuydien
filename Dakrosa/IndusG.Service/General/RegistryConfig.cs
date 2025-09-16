using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndusG.Service.General
{
    public class RegistryConfig
    {
        private readonly RegistryKey regKey;
        public RegistryConfig()
        {
            regKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\IndusG_Solutions");
        }
        public RegistryConfig(string regCnfgPath)
        {
            regKey = Registry.CurrentUser.CreateSubKey(regCnfgPath);
        }
        ~RegistryConfig()
        {
            regKey.Close();
        }

        /// <summary>
        /// writes value into specided Registry key
        /// </summary>
        /// <param name="keyName">key name</param>
        /// <param name="keyVal">value for the key</param>
        public void wrtiteRegistry(string keyName, string keyVal)
        {
            regKey.SetValue(keyName, keyVal, RegistryValueKind.String);
        }


        /// <summary>
        /// reads value from specified Registry key
        /// </summary>
        /// <param name="keyName">key name to be</param>
        /// <returns>returns value as string</returns>
        public string readRegistry(string keyName)
        {
            if (regKey != null)
            {
                return regKey.GetValue(keyName, string.Empty).ToString();
            }
            return string.Empty;
        }
    }
}
