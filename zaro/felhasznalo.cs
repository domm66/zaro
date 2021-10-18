using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zaro
{
    class felhasznalo
    {
        public String felhasznaloNev;
        public String jelszo;
        public String email;
        public int[] jelszoKepIndexek = new int[3];
        int i=0;
        
        public felhasznalo(String felhasznaloNev_, String email_)
        {
            this.felhasznaloNev = felhasznaloNev_;
            this.email = email_;
        }

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
