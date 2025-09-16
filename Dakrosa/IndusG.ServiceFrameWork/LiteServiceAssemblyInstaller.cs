using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Management;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;

namespace IndusG.ServiceFrameWork
{
    public class LiteServiceAssemblyInstaller : Installer
    {
        /// <summary>
        /// Gets or sets the type of the windows service to install.
        /// </summary>
        public ServiceAttribute Configuration { get; set; }

        public virtual string ServiceName { get; set; }

        public virtual string DisplayName { get; set; }

        public string AssemblyPath { get; set; }

        /// <summary>
        /// Installer class, to use run InstallUtil against this .exe
        /// </summary>
        /// <param name="stateSaver">The saved state for the installation.</param>
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            // install the service
            ConfigureInstallers();
            base.Install(stateSaver);

            Console.WriteLine(string.Format("Installing  service : {0}.", this.ServiceName));
        }

        /// <summary>
        /// Removes the counters, then calls the base uninstall.
        /// </summary>
        /// <param name="savedState">The saved state for the installation.</param>
        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            // load the assembly file name and the config
            ConfigureInstallers();
            base.Uninstall(savedState);

            Console.WriteLine(string.Format("Un-Installing  service : {0}.", this.ServiceName));
        }

        /// <summary>
        /// Rolls back to the state of the counter, and performs the normal rollback.
        /// </summary>
        /// <param name="savedState">The saved state for the installation.</param>
        public override void Rollback(System.Collections.IDictionary savedState)
        {
            ConfigureInstallers();
            base.Rollback(savedState);

            Console.WriteLine(string.Format("Rolling back  service  : {0}.", this.ServiceName));
        }

        /// <summary>
        /// Method to configure the installers
        /// </summary>
        private void ConfigureInstallers()
        {
            if (Installers.Count == 0)
            {
                //load the assembly file name and the config
                Installers.Add(ConfigureProcessInstaller());
                Installers.Add(ConfigureServiceInstaller());
            }
        }

        /// <summary>
        /// Helper method to configure a process installer for this windows service
        /// </summary>
        /// <returns>Process installer for this service</returns>
        private ServiceProcessInstaller ConfigureProcessInstaller()
        {
            var result = new ServiceProcessInstaller();

            // If user name is local system, run as local system account
            if (Configuration != null && Configuration.UserName != null 
                && Configuration.UserName.Equals("local system", StringComparison.OrdinalIgnoreCase))
            {
                result.Account = ServiceAccount.LocalSystem;
            }
            else
            {
                // if a user name is not provided, will run under network service acct
                if (Configuration == null || string.IsNullOrEmpty(Configuration.UserName))
                {
                    result.Account = ServiceAccount.NetworkService;
                }
                else
                {

                    // otherwise, runs under the specified user authority
                    result.Account = ServiceAccount.User;
                    result.Username = Configuration.UserName;
                    result.Password = Configuration.Password;
                }
            }

            return result;
        }

        /// <summary>
        /// Helper method to configure a service installer for this windows service
        /// </summary>
        /// <returns>Process installer for this service</returns>
        private ServiceInstaller ConfigureServiceInstaller()
        {
            // create and config a service installer
            var result = new ServiceInstaller
            {
                ServiceName = this.ServiceName,
                DisplayName = this.DisplayName,
                Description = Configuration.Description,
                StartType = Configuration.StartMode,
                DelayedAutoStart = true
            };

            return result;
        }

        /// <summary>
        /// Starts installing services
        /// </summary>
        public void Install()
        {
            string path = "/assemblypath=" + AssemblyPath;

            using (var ti = new TransactedInstaller())
            {

                ti.Installers.Add(this);
                ti.Context = new InstallContext(null, new[] { path });
                ti.Install(new Hashtable());
            }
        }

        /// <summary>
        /// checks if the service is in list
        /// if yes install is skipped and service name is
        /// removed from the list
        /// </summary>
        /// <param name="existingServices"></param>
        /// <returns></returns>
        public void OptionalInstall(List<string> existingServices)
        {
            if (existingServices.FirstOrDefault(t => t == this.ServiceName) == null)
                this.Install();
            else
                existingServices.Remove(this.ServiceName);
        }

        /// <summary>
        /// Starts uninstall services
        /// </summary>
        public void Uninstall()
        {
            var controller = ServiceController.GetServices().FirstOrDefault(t => t.ServiceName == this.ServiceName);

            if (controller != null)
            {
                string path = "/assemblypath=" + AssemblyPath;

                using (var ti = new TransactedInstaller())
                {
                    ti.Installers.Add(this);
                    ti.Context = new InstallContext(null, new[] { path });
                    ti.Uninstall(null);
                }
            }
        }

        /// <summary>
        /// Starts uninstall of all  windows services from system
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="assemblyPath"></param>
        /// <returns></returns>
        public static bool UninstallSeviceNameStartingWith(string serviceName, string assemblyPath = null, Action<string> progressMessage = null)
        {
            var controllers = ServiceController.GetServices().Where(t => t.ServiceName.StartsWith(serviceName)).ToList();

            int serviceCount = 1;

            foreach (var controller in controllers)
            {
                LiteServiceAssemblyInstaller liteUninstaller = new LiteServiceAssemblyInstaller() { ServiceName = controller.ServiceName, DisplayName = controller.DisplayName };

                if (progressMessage != null)
                    progressMessage(string.Format("Uninstalling service ({0}/{1}) : {2}", serviceCount, controllers.Count, controller.ServiceName));

                if (assemblyPath != null)
                {
                    liteUninstaller.AssemblyPath = assemblyPath;
                }

                liteUninstaller.Configuration = new ServiceAttribute(controller.ServiceName);

                liteUninstaller.Uninstall();

                serviceCount++;
            }

            controllers = ServiceController.GetServices().Where(t => t.ServiceName.StartsWith(serviceName)).ToList();

            return (controllers.Count == 0);

        }

        /// <summary>
        /// Starts uninstall of all  windows services from system
        /// </summary>
        /// <param name="serviceName"></param>
        public static void StopSeviceNameStartingWith(string serviceName, bool showMessage = true, Action<string> progressMessage = null)
        {
            var controllers = ServiceController.GetServices().Where(t => t.ServiceName.StartsWith(serviceName)).ToList();

            int serviceCount = 1;

            foreach (var controller in controllers)
            {
                if (progressMessage != null)
                    progressMessage(string.Format("Stopping service ({0}/{1}) : {2}", serviceCount, controllers.Count, controller.ServiceName));

                if (controller.Status == ServiceControllerStatus.Running)
                    controller.Stop();

                serviceCount++;
            }

            if (showMessage)
                ModernDialog.ShowMessage("All Local services stopped.", "Information", MessageBoxButton.OK);
        }

        /// <summary>
        /// Check if the service with given name is running
        /// </summary>
        /// <returns></returns>
        public static bool IsServiceRunning(string serviceName)
        {
            var controller = ServiceController.GetServices().FirstOrDefault(t => t.ServiceName == serviceName);

            if (controller == null)
                return false;

            return (controller.Status == ServiceControllerStatus.Running);
        }

        /// <summary>
        /// Get entry assembly path of the service
        /// </summary>
        public LiteServiceAssemblyInstaller()
        {
            AssemblyPath = Assembly.GetEntryAssembly().Location;
        }

        /// <summary>
        /// Starts all  windows services after installation
        /// </summary>
        /// <param name="serviceName"></param>
        public static void StartAllServices(string serviceName, List<string> exceptionList = null, Action<string> progressMessage = null)
        {
            var controllers = ServiceController.GetServices().Where(t => t.ServiceName.StartsWith(serviceName)).ToList();

            if (exceptionList != null)
            {
                var obsoleteServices = controllers.Where(t => exceptionList.Contains(t.ServiceName)).ToList();

                foreach (var obsoleteService in obsoleteServices)
                {
                    controllers.Remove(obsoleteService);
                }
            }

            int serviceCount = 1;

            foreach (var controller in controllers)
            {
                if (progressMessage != null)
                    progressMessage(string.Format("Starting service ({0}/{1}) : {2}", serviceCount, controllers.Count, controller.ServiceName));

                if (controller.Status != ServiceControllerStatus.Running &&
                    controller.StartType != ServiceStartMode.Disabled)
                {
                    controller.MachineName = System.Environment.MachineName;
                    controller.Start();

                }


                serviceCount++;
            }

        }

        /// <summary>
        /// Returns a list of services which run with the name
        /// </summary>
        /// <param name="startingName"></param>
        /// <returns></returns>
        public static List<string> GetNameOfAllServices(string startingName)
        {
            return ServiceController.GetServices().Where(t => t.ServiceName.StartsWith(startingName)).Select(t => t.ServiceName).ToList();
        }

        /// <summary>
        /// Uninstalls a service with specific name
        /// </summary>
        /// <param name="serviceName"></param>
        public static void UninstallSevice(string serviceName, string assemblyPath = null)
        {
            var controller = ServiceController.GetServices().FirstOrDefault(t => t.ServiceName == serviceName);

            if (controller != null)
            {
                LiteServiceAssemblyInstaller liteUninstaller = new LiteServiceAssemblyInstaller() { ServiceName = controller.ServiceName, DisplayName = controller.DisplayName };

                if (assemblyPath != null)
                    liteUninstaller.AssemblyPath = assemblyPath;

                liteUninstaller.Configuration = new ServiceAttribute(controller.ServiceName);

                liteUninstaller.Uninstall();

            }
        }


        /// <summary>
        /// Stop a service
        /// </summary>
        /// <param name="serviceName"></param>
        public static void StopService(string serviceName)
        {
            var controller = ServiceController.GetServices().FirstOrDefault(t => t.ServiceName == serviceName);

            if (controller != null && controller.Status == ServiceControllerStatus.Running)
            {
                controller.Stop();
            }
        }

        /// <summary>
        /// Start a service
        /// </summary>
        /// <param name="serviceName"></param>
        public static bool StartService(string serviceName)
        {
            var controller = ServiceController.GetServices().FirstOrDefault(t => t.ServiceName == serviceName);

            if (controller != null)
            {
                if (controller.Status != ServiceControllerStatus.Running)
                {
                    controller.Start();
                    return true;
                }
                else
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Deactivate service , stop service and then , change mode to manual from automatic
        /// </summary>
        /// <param name="serviceName"></param>
        public static bool DeActiveService(string serviceName)
        {
            try
            {
                var serviceController = ServiceController.GetServices().FirstOrDefault(t => t.ServiceName == serviceName);
                if (serviceController == null) return false;

                if (serviceController.Status == ServiceControllerStatus.Running)
                    serviceController.Stop();

                serviceController.SetStartupMode(ServericeStartMode.Disabled);
                return true;
            }
            catch (ManagementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Activate service, change mode to automatic from manual and then start the service
        /// </summary>
        /// <param name="serviceName"></param>
        public static bool ActiveService(string serviceName)
        {
            try
            {
                var serviceController = ServiceController.GetServices().FirstOrDefault(t => t.ServiceName == serviceName);
                if (serviceController == null) return false;
                serviceController.SetStartupMode(ServericeStartMode.Automatic);

                if (serviceController.Status != ServiceControllerStatus.Running)
                    serviceController.Start();
                return true;
            }
            catch (ManagementException)
            {
                return false;
            }
        }
    }
}
