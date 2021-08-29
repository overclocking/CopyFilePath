using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace Installer.NetFramework
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {


        public Installer1()
        {

            InitializeComponent();
        }

        public override void Commit(IDictionary savedState)
        {//설치 마무리 단계, 레지스트리 설정
            base.Commit(savedState);
            string installPath = InstallPath();
            try
            {
                ProcessStartInfo info = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = "SetRegistryByCopyFilePath.exe",
                    //Arguments = Environment.CurrentDirectory.ToString() + @"\CopyFilePath.exe", 윈폼이여서 그런가 다른위치임
                    //Arguments = installPath + @"CopyFilePath.exe",
                    WorkingDirectory = installPath,
                    Verb = "runas"

                };
                Process.Start(info);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public override void Rollback(IDictionary savedState)
        {//설치 취소시, 레지스트리 확인, 삭제
            base.Rollback(savedState);
            string installPath = InstallPath();
            ProcessStartInfo info = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = "SetRegistryByCopyFilePath.exe",
                Arguments = "delete",
                WorkingDirectory = installPath,
                Verb = "runas"

            };
            Process p = Process.Start(info);
            p.WaitForExit(1000);

        }
        public override void Uninstall(IDictionary savedState)
        {//삭제시, 레지스트리 확인, 삭제
            base.Uninstall(savedState);

            string installPath = InstallPath();
            ProcessStartInfo info = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = "SetRegistryByCopyFilePath.exe",
                Arguments = "delete",
                WorkingDirectory = installPath,
                Verb = "runas"

            };
            Process p = Process.Start(info);
            p.WaitForExit(1000);


        }
        public string InstallPath()
        {//https://stackoverflow.com/questions/17329289/how-to-get-installation-path-from-setup-project
            string assemblyPath = Context.Parameters["assemblyPath"];//설치위치+파일이름.DLL
            return (assemblyPath.Substring(0, assemblyPath.Length - 26));//설치위치만
        }
    }
}
