using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Turing_Machine_C_
{
    public class TuringMachine
    {
        private string file;//Turing machine's program location
        private List<char> tape=new List<char>();
        private int headPosition;//Position of the head
        private List<string> program=new List<string>();
        private string currentState;//busena
        public bool halt;
        private Instructions[] instruction;
        private Object LockOutput;
        private int number;

        public TuringMachine(string _file,Object _lock, int _number)
        {
            LockOutput=_lock;
            file=_file;
            currentState="0";
            halt=false;
            number=_number;
            Input(ref instruction);
        }

        private void Input(ref Instructions[] instruction)
        {
            //Failo nuskaitymas / reiksmiu surasymas i kintamuosius

            program=File.ReadAllLines(file).Where(arg => !string.IsNullOrWhiteSpace(arg)).ToList();

            string _tape=program[0];
            for(int i=0;i<_tape.Length;i++)
            {
                tape.Add(_tape[i]);
            }
            headPosition=Convert.ToInt32(program[1]);
            program.RemoveAt(0);
            program.RemoveAt(0);

            //Split program(List), initilize Instructor class

            instruction= new Instructions[program.Count];
            for(int i=0;i<program.Count;i++)
            {
                for(int j=0;j<program[i].Length;j++)
                {
                    string[] n=program[i].Split(" ");
                    instruction[i]= new Instructions(n[0],n[1],n[2],n[3],n[4]);
                }
            }
        }

        private void Search()
        {
            if(halt==true)
            {
                return;
            }
            
            int counter=1;
            for(int i=0;i<program.Count;i++)
            {
                if(instruction[i].State==currentState && instruction[i].Symbol==tape[headPosition-1])
                {
                    tape[headPosition-1]=instruction[i].NewSymbol;
                    if(instruction[i].Direction=='l' || instruction[i].Direction=='L')
                    {
                        headPosition--;
                    }
                    else if(instruction[i].Direction=='r' || instruction[i].Direction=='R')
                    {
                        headPosition++;
                    }

                    currentState=instruction[i].NewState;

                    /*string tapeAsString=String.Empty;
                    foreach(char elem in tape)
                    {
                        tapeAsString+=elem.ToString();
                    }
                    lock(LockOutput)
                    {
                        Console.SetCursorPosition(0,number+1);
                        Console.Write(tapeAsString);
                    }*/

                    for(int ind=0;ind<tape.Count;ind++)
                    {
                        lock(LockOutput)
                        {
                        Console.SetCursorPosition(ind,number+1);
                        Console.Write(tape[ind]);
                        }
                    }

                    break;
                }
                counter++;
            }
            if(headPosition>tape.Count || headPosition<=0)
            {
                halt=true;
            }
            else if(counter>program.Count)
            {
                halt=true;
            }
            else if(halt==true)
            {
                return;
            }
        }

        public void Execution()
        {
            while(halt==false)
            {
                {
                    Search();
                }
            }
        }
    }
}