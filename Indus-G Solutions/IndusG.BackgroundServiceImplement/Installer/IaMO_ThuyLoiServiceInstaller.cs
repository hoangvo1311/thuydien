using IndusG.ServiceFrameWork;

namespace IndusG.BackgroundServiceImplement.Installer
{
    public class IaMO_ThuyLoiServiceInstaller<T> : LiteServiceInstaller<T> where T : IIndusGService
    {
        public override string ServiceName
        {
            get { return "IndusG - Thuy Loi Pushing Service"; }
        }

        public override string DisplayName
        {
            get { return "IndusG - Thuy Loi Pushing Service"; }
        }

        public IaMO_ThuyLoiServiceInstaller(T IndusGService)
            : base(IndusGService)
        {
        }

        protected override void OnBeforeInstall(System.Collections.IDictionary savedState)
        {
            var assemPath = Context.Parameters["assemblypath"];

            Context.Parameters["assemblypath"] = "\"" + assemPath + "\" -RunIAMO_ThuyLoiService";

            base.OnBeforeInstall(savedState);
        }

    }
}
