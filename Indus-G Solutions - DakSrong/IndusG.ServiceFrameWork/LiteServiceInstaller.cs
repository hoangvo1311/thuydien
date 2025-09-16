using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;

namespace IndusG.ServiceFrameWork
{
    /// <summary>
    /// A generic windows service installer
    /// </summary>
    [RunInstaller(true)]
    public abstract class LiteServiceInstaller<T> : LiteServiceAssemblyInstaller where T : IIndusGService
    {
        /// <summary>
        /// Creates a windows service installer using the type specified.
        /// </summary>
        /// <param name="IndusGService">The type of the windows service to install.</param>
        protected LiteServiceInstaller(T IndusGService)
        {
            var _IndusGService = IndusGService;

            if (_IndusGService != null)
            {

                var attribute = _IndusGService.GetType().GetAttribute<ServiceAttribute>();

                if (attribute == null)
                {
                    throw new ArgumentException("Type to install must be marked with a ServiceAttribute.",
                                                "IndusGService");
                }

                Configuration = attribute;
            }
        }

        protected LiteServiceInstaller(Type serviceType)
        {

            var attribute = serviceType.GetAttribute<ServiceAttribute>();

            if (attribute == null)
            {
                throw new ArgumentException("Type to install must be marked with a ServiceAttribute.",
                                            "serviceType");
            }

            Configuration = attribute;
        }

    }
}