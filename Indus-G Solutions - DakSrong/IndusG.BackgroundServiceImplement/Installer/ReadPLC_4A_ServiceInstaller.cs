using IndusG.ServiceFrameWork;

namespace IndusG.BackgroundServiceImplement.Installer
{
    public class ReadPLC_4A_ServiceInstaller<T> : LiteServiceInstaller<T> where T : IIndusGService
    {
        public override string ServiceName
        {
            get { return "IndusG - Read PLC Service"; }
        }

        public override string DisplayName
        {
            get { return "IndusG - Read PLC Service"; }
        }

        public ReadPLC_4A_ServiceInstaller(T IndusGService)
            : base(IndusGService)
        {
        }

        protected override void OnBeforeInstall(System.Collections.IDictionary savedState)
        {
            var assemPath = Context.Parameters["assemblypath"];

            Context.Parameters["assemblypath"] = "\"" + assemPath + "\" -ReadPLC_4AService";

            base.OnBeforeInstall(savedState);
        }

    }
}
