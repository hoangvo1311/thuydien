using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LicenseHelper
{
    public class License
    {
        /// <summary>
        /// Computes Hardware Id
        /// </summary>
        /// <returns>HardwareId</returns>
        public static string GetHardwareId()
        {
            var macs = GetMacs();
            var deviceIds = GetDeviceIds();

            if (deviceIds != null)
                macs = string.Join(",", macs, deviceIds);

            return macs;
        }

        /// <summary>
        /// Get device identifier
        /// </summary>
        /// <returns></returns>
        private static string GetDeviceIds()
        {
            string cpuid = GetManagementProperty("win32_processor", "processorID", "PID");
            string hddId = GetManagementProperty("win32_diskdrive", "serialNumber", "HDD");
            string motherBoardId = GetManagementProperty("win32_baseboard", "serialNumber", "MBD");
            string biosId = GetManagementProperty("Win32_BIOS", "serialNumber", "BID");

            var combinedid = $"{cpuid}-{hddId}-{motherBoardId}-{biosId}";
            return combinedid == "---" ? null : combinedid;
        }

        /// <summary>
        /// Get a property from win32 management object
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="property"></param>
        /// <param name="prependId"></param>
        /// <returns></returns>
        private static string GetManagementProperty(string objectName, string property, string prependId)
        {
            try
            {
                ManagementClass managClass = new ManagementClass(objectName);
                ManagementObjectCollection managCollec = managClass.GetInstances();

                foreach (ManagementBaseObject managObj in managCollec)
                {
                    var value = managObj?.Properties[property]?.Value?.ToString().Trim();

                    if (value != null)
                        return $"{{{prependId}:{value}}}";
                }
            }
            catch (Exception)
            {
                //Management object may fail, in this case return null
            }

            return null;
        }

        public static string GetMacs()
        {
            var macIds = NetworkInterface.GetAllNetworkInterfaces().
                Where(t => t.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || t.NetworkInterfaceType == NetworkInterfaceType.Ethernet).
                 Select(t => t.GetPhysicalAddress().ToString());

            return string.Join(",", macIds);
        }

        /// <summary>
        /// Check if any hardware token matches
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static bool MatchAnyToken(string source, string destination)
        {
            var sourceTokens = source.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var destinationTokens = destination.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var sourceToken in sourceTokens)
            {
                foreach (var destToken in destinationTokens)
                {
                    if (!string.IsNullOrWhiteSpace(sourceToken) &&
                        !string.IsNullOrWhiteSpace(destToken) &&
                        sourceToken.ToLowerInvariant() == destToken.ToLowerInvariant())
                        return true;
                }
            }

            return false;
        }
    }
}
