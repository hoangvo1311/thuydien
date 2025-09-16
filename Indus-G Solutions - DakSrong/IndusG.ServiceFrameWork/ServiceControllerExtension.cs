using System.Management;
using System.ServiceProcess;

namespace IndusG.ServiceFrameWork
{
    public enum ServericeStartMode
    {
        Boot,
        System,
        Automatic,
        Manual,
        Disabled 
    }

    public static class ServiceControllerExtension
    {
        /// <summary>
        /// Set  windows service startup mode i.e. "Automatic", "Manual" or "Disabled"
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="startMode"></param>
        public static void SetStartupMode(this ServiceController controller, ServericeStartMode startMode)
        {
            string path = "Win32_Service.Name='" + controller.ServiceName + "'";

            ManagementPath mp = new ManagementPath(path);

            //construct the management object
            ManagementObject managementObj = new ManagementObject(mp);

            //Use the invokeMethod method of the ManagementObject 
            managementObj.InvokeMethod("ChangeStartMode", new object[] { startMode.ToString() });
        }

        /// <summary>
        ///  Get  windows service startup mode i.e. "Automatic", "Manual" or "Disabled"
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static string GetStartMode(this ServiceController controller)
        {
            string path = "Win32_Service.Name='" + controller.ServiceName + "'";
            ManagementPath mp = new ManagementPath(path);

            //construct the management object
            ManagementObject managementObj = new ManagementObject(mp);

            //Use the invokeMethod method of the ManagementObject 
            return (string)managementObj["StartMode"];
        }
    }
}
