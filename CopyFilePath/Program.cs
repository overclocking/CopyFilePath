using System;
namespace CopyFilePath
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {//매개변수(args)없이 실행했을때 안내문
                Console.WriteLine("[CopyFilePath.exe]는 단독실행으로는 아무런 기능이 없습니다." +
                                  "\n매개변수를 넣어서 실행해주세요.");
            }

            try
            {
                string stringArgs = "";
                //매개변수 출력
                foreach (var arg in args)
                {
                    stringArgs += arg.ToString();
                }
                Console.WriteLine(stringArgs);

                //Clipboard.SetText("Hello world"); 나만모름? 어셈블리 참조기능이 없는데

                //대안, cmd를 이용해 복사하기
                Copy2CMD(stringArgs);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        private static void Copy2CMD(string text)
        {
            System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
            start.FileName = "cmd.exe";
            start.CreateNoWindow = true; //true는 화면없이 실행
            start.UseShellExecute = false;

            start.RedirectStandardInput = true;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;


            System.Diagnostics.Process cmd = new System.Diagnostics.Process();
            cmd.StartInfo = start;
            cmd.Start();

            cmd.StandardInput.WriteLine(@"echo " + text + "| clip");
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            cmd.Close();
        }
    }
}
