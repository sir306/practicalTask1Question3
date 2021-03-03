using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TicketingSystem
{
    class Program
    {
        public static Queue<int> customers = new Queue<int>();
        static void Main(string[] args)
        {
            Timer tmrTimersTimer = new Timer();
            tmrTimersTimer.Interval = 5000; //set the interval to 5 seconds
            tmrTimersTimer.Elapsed += new ElapsedEventHandler(TmrTimersTimer_Elapsed); //run the code in the tmrTimersTimer_Elapsed every 5 seconds until keypress
            tmrTimersTimer.Start();
            Console.Read();
        }
        private static void TmrTimersTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("Sales Assistant is ready to see the next customer.");
            Console.WriteLine();
            //check to see if customers in queue and dequeue as when sales assisstant is ready
            //the rest of the code to disply the next customer in line, what number ticket is next and list all customers in a queue
            if (customers.Count != 0)
            {
                Console.WriteLine("The customer with ticket number {0} will be seen.", customers.Peek());
                Customers.DequeueCustomer();
                Console.WriteLine();
                //check to see if no customers after dequeue
                if (customers.Count != 0)
                {
                    string currentQueue = "";
                    foreach (int item in customers)
                    {
                        if (item == customers.Last())
                        {
                            currentQueue += item.ToString();
                        }
                        else
                        {
                            currentQueue += item.ToString() + ",";
                        }
                    }
                    Console.WriteLine("The customers with the following tickets are in the queue: [{0}]",currentQueue);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("There are no customers to see.");
                    Console.WriteLine();
                }

            }
            else
            {
                Console.WriteLine("There are no customers to see.");
            }


            //to start timer for customers arriving and add to queue
            Timer tmrTimersTimer2 = new Timer
            {
                Interval = 3000 //set the interval to 3 seconds
            };
            tmrTimersTimer2.Elapsed += new ElapsedEventHandler(TmrTimersTimer_Elapsed2); //run the code in the tmrTimersTimer_Elapsed every 3 seconds until keypress
            tmrTimersTimer2.Start();
        }
        private static void TmrTimersTimer_Elapsed2(object sender, ElapsedEventArgs e)
        {
            //Console.WriteLine("New Customer");
            Customers.IncreaseCustomerNum();
            Customers.AddCustomerToQueue();
            Console.WriteLine("Customer with ticket {0} is added to the queue.", Customers.GetCustomerNum());
        }
    }

    class Customers
    {

        private static int CustomerNum { get; set; }
        public static int GetCustomerNum() => CustomerNum;
        public static void IncreaseCustomerNum() => ++CustomerNum;
        public static void AddCustomerToQueue()
        {
            Program.customers.Enqueue(Customers.GetCustomerNum());
        }
        public static void DequeueCustomer() => Program.customers.Dequeue();
    }
}
