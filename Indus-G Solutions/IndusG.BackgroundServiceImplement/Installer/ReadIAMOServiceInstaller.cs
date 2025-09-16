using IndusG.ServiceFrameWork;

namespace IndusG.BackgroundServiceImplement.Installer
{
    public class ReadIAMOServiceInstaller<T> : LiteServiceInstaller<T> where T : IIndusGService
    {
        public override string ServiceName
        {
            get { return "IndusG - Read IAMO Service"; }
        }

        public override string DisplayName
        {
            get { return "IndusG - Read IAMO Service"; }
        }

        public ReadIAMOServiceInstaller(T IndusGService)
            : base(IndusGService)
        {
        }

        protected override void OnBeforeInstall(System.Collections.IDictionary savedState)
        {
            var assemPath = Context.Parameters["assemblypath"];

            Context.Parameters["assemblypath"] = "\"" + assemPath + "\" -ReadIAMOService";

            base.OnBeforeInstall(savedState);
        }

    }
}
