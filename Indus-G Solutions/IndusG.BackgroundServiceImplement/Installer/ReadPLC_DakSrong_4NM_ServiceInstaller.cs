using IndusG.ServiceFrameWork;

namespace IndusG.BackgroundServiceImplement.Installer
{
    public class ReadPLCDakSrong_4NM_ServiceInstaller<T> : LiteServiceInstaller<T> where T : IIndusGService
    {
        public override string ServiceName
        {
            get { return "IndusG - Read PLC Service"; }
        }

        public override string DisplayName
        {
            get { return "IndusG - Read PLC Service"; }
        }

        public ReadPLCDakSrong_4NM_ServiceInstaller(T IndusGService)
            : base(IndusGService)
        {
        }

        protected override void OnBeforeInstall(System.Collections.IDictionary savedState)
        {
            var assemPath = Context.Parameters["assemblypath"];

            Context.Parameters["assemblypath"] = "\"" + assemPath + "\" -ReadPLC_DakSrong_4NM_Service";

            base.OnBeforeInstall(savedState);
        }

    }
}
