using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataType_boxing_and_unboxing
{
    class Program
    {
        /*
         * 박싱(boxing)
         * 데이터형을 최상위 object형으로 변환하여 힙(heap메모리)에 저장
         * int m = 123;
         * object obj = m;
         * 
         * 언박싱(unboxing)
         * 힙에 저장된 형식을 다시 원래의 형식으로 변환
         * int n = (int)obj;
         */

        static void Main(string[] args)
        {
            int i = 123;
            object obj = i;
            Console.WriteLine("{0}",(int)obj);

            // 박싱과 언박싱 과정에서 메모리가 공유가 발생하는지, 또는 복사가 발생하는지 확인해보자.
            int j = 123;
            object o = j;
            j = 456;
            Console.WriteLine("{0} {1}", j, (int)o);
            //공유안함.
        }
    }
}
