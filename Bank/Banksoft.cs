using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class Banksoft
    {
        string cartnum;
        string pass;

        public void cartreader(string cartnum)
        {
            this.cartnum = cartnum;
           
        }
        public void passreader(string pass)
        {
            this.pass = pass;
        }


    }
}
