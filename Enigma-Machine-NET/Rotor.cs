using System;
using System.Collections.Generic;
using System.Text;

namespace Enigma_Machine_NET
{
    public class Rotor
    {
        private string _wiring;
        public int Position { get; private set; }

        public Rotor(string wiring)
        {
            _wiring = wiring;
            Position = 0;
        }

        public bool Step()
        {
            Position += 1;
            bool atNotch = Position > 25;
            Position %= 26;

            return atNotch;
        }

        public void SetPosition(int position)
        {
            Position = position;
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
