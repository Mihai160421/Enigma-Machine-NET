using System;
using System.Collections.Generic;
using System.Text;

namespace Enigma_Machine_NET
{
    public class Plugboard
    {
        private Dictionary<char, char> conn = new Dictionary<char, char>();

        public void AddConnection(char a, char b)
        {
            RemoveConnection(a);
            RemoveConnection(b);

            if (a != b)
            {
                conn[a] = b;
                conn[b] = a;
            }
        }

        public void RemoveConnection(char litera)
        {
            if (conn.ContainsKey(litera))
            {
                char partener = conn[litera];
                conn.Remove(litera);
                conn.Remove(partener);
            }
        }

        public char Process(char intrare)
        {
            if (conn.ContainsKey(intrare))
            {
                return conn[intrare];
            }
            return intrare;
        }

        public void ClearAll() => conn.Clear();
    }
}
