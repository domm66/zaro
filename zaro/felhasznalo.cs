using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zaro
{
    class felhasznalo
    {

        
        public string felhasznaloNev { get; set; }
        public string jelszo { get; set; }
        public string email { get; set; }

        public int[] jelszoKepIndexek = new int[3];

        int i=0;

        public void addJelszo(String jelszo_)
        {
            this.jelszo = jelszo_;
        }

        public void addIndex(int szam)
        {
            jelszoKepIndexek[i++]=szam;
            //i++;
        }

        public int getIndex(int a) 
        {
            return jelszoKepIndexek[a];
        }

        public string getNev() {
            return felhasznaloNev;
        }

        public string getJelszo() {
            return jelszo;
        }

        public string getEmail()
        {
            return email;
        }
    }
}
