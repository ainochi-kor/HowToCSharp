using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Callback2
{
    class Archive
    {
        public delegate void CompProg(int nFile);
        public static void Compress(int nFile, CompProg Prog)
        {
            for (int i = 0; i < nFile ; i++)
            {
                Prog(i);
                System.Threading.Thread.Sleep(500);
            }
        }
    }
    class Program
    {
        public static void Progress(int nFile)
        {
            Console.WriteLine("{0}번째 파일을 압축하는 중입니다.", nFile + 1);
        }
        public static void EngProgress(int nFile)
        {
            Console.WriteLine("Now Compressing {0} File. Wait please", nFile + 1);
        }
        static void Main(string[] args)
        {
            Archive.Compress(10, Progress);
            Console.WriteLine("모든 파일을 압축했습니다.");
        }
    }
}
