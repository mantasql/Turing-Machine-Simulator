using System;
using System.Threading;
using System.IO;

namespace Turing_Machine_C_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title="Turing Machine";//Pavadinimas
            StartProgram();
        }

        public static void StartProgram()
        {
            Console.Clear();
            object LockExecution = new object();

            Console.WriteLine("How many turing machines do you want to run ?");
            int n=0;
            while(true)
            {
                var m=Console.ReadLine();
                if(int.TryParse(m,out int l))
                {
                    if(Convert.ToInt32(m)>0)
                    {
                        n=Convert.ToInt32(m);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input");
                }
            }
            TuringMachine[] TM = new TuringMachine[n];
            Thread[] Threads = new Thread[n];
            int index=0;
            while(index < n)
            { 
                Console.Write("Write programs file name: ");
                string filename=Console.ReadLine();
                if (File.Exists(filename))
                {
                    TM[index]=new TuringMachine(filename,LockExecution,index*2);
                    index++;
                }
                else
                {
                    Console.WriteLine($"Theres no file named {filename}");
                }
            }

            Console.Clear();

            for(int i=0;i<n;i++)
            {
                Threads[i] = new Thread(new ThreadStart(TM[i].Execution));

                Threads[i].Start();
            }

            while(true)
            {
                if(Console.ReadKey(true).Key==ConsoleKey.Spacebar)
                {
                    for(int i=0;i<n;i++)
                    {
                        TM[i].halt=true;
                    }
                    break;
                }
                else if(Console.ReadKey(true).Key==ConsoleKey.Backspace)
                {
                    for(int i=0;i<n;i++)
                    {
                        TM[i].halt=true;
                    }
                    
                    while(true)
                    {
                        int c=0;
                        for(int i=0;i<Threads.Length;i++)
                        {
                            if(Threads[i].IsAlive==false)
                            {
                                c++;
                            }
                        }
                        if(c==Threads.Length)
                        {
                            break;
                        }
                    }

                    StartProgram();
                }
            }

        }
    }
}
