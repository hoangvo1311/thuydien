using System;
using System.Configuration;
using System.Reflection;

namespace IndusG.ServiceFrameWork
{
    public static class ServiceHelper
    {
        /// <summary>
        /// Get config from configuration file by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfiguration(string key)
        {
            Assembly service = Assembly.GetEntryAssembly();
            Configuration config = ConfigurationManager.OpenExeConfiguration(service.Location);

            if (config.AppSettings.Settings[key] != null)
            {
                return config.AppSettings.Settings[key].Value;
            }
            else
            {
                throw new ArgumentException("Settings collection does not contain the requested key:" + key);
            }
        }
    }
}