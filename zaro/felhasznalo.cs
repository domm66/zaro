using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zaro
{
    class felhasznalo
    {
        private String felhasznaloNev;
        private String jelszo;
        private String email;
        public felhasznalo(String felhasznaloNev_,String jelszo_,String email_)
        {
            this.felhasznaloNev = felhasznaloNev_;
            this.jelszo = jelszo_;
            this.email = email_;
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
