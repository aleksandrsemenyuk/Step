using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Dz_GAme_Timer
{
    class Program
    {
        static void Main(string[] args)
        {
            Game g1 = new Game();
            Thread th = new Thread(g1.Start);
            th.Start(); 
            while (!g1.CheckField())
            {
                Console.WriteLine("Enter Unit {0}", g1.Unit);
                g1.Check = Console.ReadLine();
            }
            th.Abort();
            Console.WriteLine("Ваш час {0} мсек", g1.time);
            Console.ReadLine();
        }
    }
    public class Game
    {
        public int time { get; set; }
        public Timer tim { get; set; }
        public string Unit { get; set; }
        public string Check { get; set; }

        public Game()
        {
            time = 0;
            TimerCallback callback = new TimerCallback(Run);
            this.tim = new Timer(callback);
            this.Unit = GenerateUnit();
        }

        public string GenerateUnit()
        {
            char[] chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray();
            Random r = new Random();
            int i = r.Next(chars.Length);
            return chars[i].ToString();
        }
        public void Run(object timer)
        {
            time++;
           
        }

        public bool CheckField()
        {
            if (this.Unit == this.Check)
            {
                tim.Dispose();
                return true;
            }
            else 
            {
                return false;
            }
        }

        public void Start()
        {
            tim.Change(0, 1);
        }
    }
}
