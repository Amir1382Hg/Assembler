using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    class Mano
    {
        public int pc;
        public char[,] memory = new char[64000, 16];
        public char[] IR;
        public int N;
        public int Z;
        public int P;
        public Dictionary<string, char[]> map;

        public Mano()
        {
            pc = N = Z = P = 0;
            IR = new char[16];
            for(int i = 0; i < 16; i++)
            {
                IR[i]='0';
            }
            map = new Dictionary<string, char[]>();
            InitializeMap();
        }

        private void InitializeMap()
        {
            char[] temp = new char[16];
            map.Add("000", temp);
            map.Add("001", temp);
            map.Add("010", temp);
            map.Add("011", temp);
            map.Add("100", temp);
            map.Add("101", temp);
            map.Add("110", temp);
            map.Add("111", temp);
        }

    }
}
