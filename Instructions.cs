using System;

namespace Turing_Machine_C_
{
        public class Instructions
        {
            private string state;
            private char symbol;
            private char newSymbol;
            private char direction;
            private string newState;

            public Instructions(string _state,string _symbol,string _newSymbol,string _direction, string _newState)
            {
                state=_state;
                symbol=Convert.ToChar(_symbol);
                newSymbol=Convert.ToChar(_newSymbol);
                direction = Convert.ToChar(_direction);
                newState=_newState;
            }
            public string State
            {
                get{return state;}
            }
            public char Symbol
            {
                get{return symbol;}
            }
            public char NewSymbol
            {
                get{return newSymbol;}
            }
            public char Direction
            {
                get{return direction;}
            }
            public string NewState
            {
                get{return newState;}
            }
        }
}