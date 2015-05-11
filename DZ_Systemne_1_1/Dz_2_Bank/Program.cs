using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;

namespace Dz_2_Bank
{
    class Program
    {
        static void Main(string[] args)
        {

            Bank bank1 = new Bank();
            Console.WriteLine("Enter new Money");
            bank1.Money= Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("{0}", bank1.Money.ToString());
            Console.WriteLine("Enter new Percent");
            bank1.Percent = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("{0}", bank1.Percent.ToString());
            Console.WriteLine("Enter new Name");
            bank1.Name = Console.ReadLine();
            Console.WriteLine("{0}", bank1.Name.ToString());
        }
    }
    public class Bank
    {

        private Thread thCur;
        private int money;
        private string name;
        private int percent;

        public Bank() {
            this.money = 0;
            this.name = "null";
            this.percent = 0;
        }
        public int Money
        {
            get { return money; }
            set
            {
                if (money != value)
                {
                    this.money = value;
                    this.thCur = new Thread(OnPropertyChanged);
                    thCur.Start("Money");
                }
            }
        }
        public string  Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    this.name = value;
                    this.thCur = new Thread(OnPropertyChanged);
                    thCur.Start("Name");
                }
            }
        }
        public int Percent
        {
            get { return percent; }
            set
            {
                if (percent != value)
                {
                    this.percent = value;
                    this.thCur= new Thread(OnPropertyChanged);
                    thCur.Start("Percent");
                }
            }
        }
       protected void OnPropertyChanged(object name)
        {

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"WriteLines2.txt", true))
            {
                file.WriteLine(" {0} Changed property {1}  Money - {2}    Name  - {3}   Percent - {4}",DateTime.Now.ToUniversalTime().ToLocalTime() , name.ToString(), Money, this.name,this.percent);
            }
        }

    }
}
