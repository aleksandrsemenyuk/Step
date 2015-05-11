using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace DZ_Systemne_1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> InCol = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ParameterizedThreadStart thstr = new ParameterizedThreadStart(RunMetod);
            Thread th1 = new Thread(thstr);
            th1.Start(InCol);
        }
        static void RunMetod(object InCol)
        {
            foreach (var el in InCol as List<int>)
            {
                Console.WriteLine("Запуск в потоці елемент колекції {0}", el.ToString());
            }
        }
    }

   
}
