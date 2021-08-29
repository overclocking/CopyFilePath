using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using Microsoft.Win32;

namespace Installer
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        RegistryKey reg;


        public Installer1()
        {

            InitializeComponent();
        }

        public override void Commit(IDictionary savedState)
        {//설치 마무리 단계, 레지스트리 설정
            base.Commit(savedState);

            try
            {
                ProcessStartInfo info = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = "SetRegistryByCopyFilePath.exe",
                    Arguments = Environment.CurrentDirectory.ToString() + @"\CopyFilePath.exe",
                    WorkingDirectory = Environment.CurrentDirectory,
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
            ProcessStartInfo info = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = "SetRegistryByCopyFilePath.exe",
                Arguments = "delete",
                WorkingDirectory = Environment.CurrentDirectory,
                Verb = "runas"

            };
            Process.Start(info);

        }
        public override void Uninstall(IDictionary savedState)
        {//삭제시, 레지스트리 확인, 삭제
            base.Uninstall(savedState);
            ProcessStartInfo info = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = "SetRegistryByCopyFilePath.exe",
                Arguments = "delete",
                WorkingDirectory = Environment.CurrentDirectory,
                Verb = "runas"

            };
            Process.Start(info);
        }
    }
}
