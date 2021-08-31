using Microsoft.Win32;
using System;

namespace SetRegistryByCopyFilePath
{
    class Program
    {
        //https://blog.hexabrain.net/176 참고!

        public static RegistryKey reg;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Regedit!\n\n\n");
            string temp = "";
            foreach (var arg in args)
            {
                temp += arg.ToString();
            }
            if (temp.Equals("delete"))
            {
                DeleteRegistry();
                Console.WriteLine("레지스트리 삭제됨");
            }
            else if (temp.Equals("edit"))
            {
                Console.WriteLine("변경할 기능명을 입력해주세요!");
                string editKey = Console.ReadLine();
                EditRegistry(editKey);
            }
            else
            {
                CreateRegistry(System.Environment.CurrentDirectory + @"\CopyFilePath.exe");
            }

        }
        static void CreateRegistry(string url)
        {
            reg = Registry.ClassesRoot.CreateSubKey("*").CreateSubKey("shell").CreateSubKey("CopyFilePath");
            reg.SetValue(null, "전체주소복사하기");
            reg = Registry.ClassesRoot.CreateSubKey("*").CreateSubKey("shell").CreateSubKey("CopyFilePath").CreateSubKey("command");
            reg.SetValue(null, "\"" + url + "\"" + " \"%1\"");//%1 매개변수 넘기는거
        }

        static void EditRegistry(string text)
        {
            reg = Registry.ClassesRoot.OpenSubKey("*").OpenSubKey("shell").OpenSubKey("CopyFilePath", true);// 두번쨰 인자 true 해야 쓰기 권한 획득
            reg.SetValue(null, text);
        }

        static void DeleteRegistry()
        {
            Registry.ClassesRoot.DeleteSubKeyTree(@"*\shell\CopyFilePath");
        }

    }

}
