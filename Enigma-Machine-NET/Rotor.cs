using System;
using System.Collections.Generic;
using System.Text;

namespace Enigma_Machine_NET
{
    public class Rotor
    {
        private string _wiring;
        private char _notch; 
        public int Position { get; private set; }

        public Rotor(string wiring, char notch)
        {
            _wiring = wiring;
            _notch = notch;
            Position = 0;
        }

        public bool Step()
        {
            bool atNotch = (char)('A' + Position) == _notch;
            Position = (Position + 1) % 26;
            return atNotch;
        }

        public int Forward(int input)
        {
            int contactIn = (input + Position) % 26;
            int internalOutput = _wiring[contactIn] - 'A';
            return (internalOutput - Position + 26) % 26;
        }

        public int Backward(int input)
        {
            int contactIn = (input + Position) % 26;
            char targetChar = (char)('A' + contactIn);
            int internalOutput = _wiring.IndexOf(targetChar);
            return (internalOutput - Position + 26) % 26;
        }
    }
}
