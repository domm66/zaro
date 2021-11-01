using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace zaro
{
    public partial class Form1 : Form
    {
        int mousePosX; //szelesseg
        int mousePosY; //magassag
 
        int szamlalo=0;
        felhasznalo[] accountok = new felhasznalo[999999];
        //List<felhasznalo> accountok = new List<felhasznalo>();
        String felhJelszava;
        int currentpicIndex;
        String currentpic;
        int lastpicIndex;
        String lastpic;
        Random rand = new Random();
        String[] files = Directory.GetFiles(@"pics");
        bool ujfelhasznalo = false;
        bool meglevofelhasznalo;
        int meglevofelhindex;
        bool kattintott = false;

        int indexSzamlalo=0;
        int kepszamlalo = 0;

        int osztasMerteke = 40;

        String jsonstring;
        Rootobject[] deserializalt;
        bool letezik = false;



        public Form1()
        {
            InitializeComponent();
            Deserialize();
            //var fasz = JsonConvert.DeserializeObject<dynamic>("felhasznalok.json");
            textBox1.AppendText(deserializalt.Length.ToString());
            GombokAlap();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mousePosX = (this.PointToClient(MousePosition).X - pictureBox1.Location.X) / osztasMerteke; //szelesseg eldarabol 20x20 negyszogekre
            mousePosY = (this.PointToClient(MousePosition).Y - pictureBox1.Location.Y) / osztasMerteke; //magassag eladarabolva szinten 20x20 negyszogekre
            textBox1.AppendText("\r\n" + "X: " + mousePosX.ToString() + " Y: " + mousePosY.ToString() + "\r\n");
            felhJelszava += mousePosX.ToString() + mousePosY.ToString();
            textBox1.AppendText(felhJelszava + "\r\n");
            if (kepszamlalo == 3)
            {
                button4.Enabled = false;
            } else if (kepszamlalo<3)
            {
                button4.Enabled = true;
            } 
            //kepszamlalo++;
            //pictureBox1.Enabled = false;
        }

        public void Regisztracio() {
            
            
            ujfelhasznalo = false;

            if (kepszamlalo==3 && felhJelszava.Length >= 6 || felhJelszava != null) {
                
                if (!letezik) {
                    if(deserializalt.Length>0)
                    {
                        accountok[szamlalo].jelszo = felhJelszava;
                        textBox1.AppendText("Sikeres mentés!" + "\r\n" + accountok[szamlalo].getNev() + " " + accountok[szamlalo].getEmail() + " " + accountok[szamlalo].getJelszo() + "\r\n");
                        jsonstring = "," + JsonConvert.SerializeObject(accountok[szamlalo], Formatting.Indented);
                        File.AppendAllText("felhasznalok.json", jsonstring);
                        GombokAlap();
                        Reset();
                        MessageBox.Show("Sikeres jelszó regisztráció!");
                        szamlalo++;

                    } else
                    {
                        accountok[szamlalo].jelszo = felhJelszava;
                        textBox1.AppendText("Sikeres mentés!" + "\r\n" + accountok[szamlalo].getNev() + " " + accountok[szamlalo].getEmail() + " " + accountok[szamlalo].getJelszo() + "\r\n");
                        jsonstring = JsonConvert.SerializeObject(accountok[szamlalo], Formatting.Indented);
                        File.AppendAllText("felhasznalok.json", jsonstring);
                        GombokAlap();
                        Reset();
                        MessageBox.Show("Sikeres jelszó regisztráció!");
                        szamlalo++;

                    }
                    
                } else 
                {
                    MessageBox.Show("Ilyen felhasználónév már létezik!");
                    Reset();
                    GombokAlap();
                }
                
            } else
            {
                MessageBox.Show("Adj meg egy megfelelő hosszúságú jelszót először!");
            }
           
            label6.Text = "Maradék kép: " + (3 - kepszamlalo).ToString();
        }

        public void Jelszomegadas() {
            if(deserializalt[meglevofelhindex].jelszo == felhJelszava)
            {
                MessageBox.Show("Jelszó elfogadva!");
                Reset();
                GombokAlap();
            } else
            {
                MessageBox.Show("Rossz jelszó, próbálja meg újra!");
                Reset();
                GombokAlap();
            }
            
            label6.Text = "Maradék kép: " + (3 - kepszamlalo).ToString();
        }

        public void Deserialize()
        {
            var path = @"felhasznalok.json";
            string elolvasott = "[" + File.ReadAllText(path) + "]";
            deserializalt = JsonConvert.DeserializeObject<Rootobject[]>(elolvasott);
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Jelszomegadas();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button2.Visible = false;
            button6.Enabled = true;
            button7.Enabled = true;
            button6.Visible = true;
            button7.Visible = true;
            button4.Enabled = false;
            pictureBox1.Visible = false;
            Regisztracio();
            Deserialize();
            textBox1.AppendText(deserializalt[2].jelszoKepIndexek[0].ToString());
            foreach(var indexek in deserializalt[2].jelszoKepIndexek )
            {
                textBox1.AppendText(indexek.ToString());
            }
            //textBox1.AppendText("jelszo:" + accountok[szamlalo - 1].getJelszo().ToString());
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Text = "X: " + (((this.PointToClient(MousePosition).X - pictureBox1.Location.X)) / osztasMerteke).ToString(); //X
            label3.Text = "Y: " + (((this.PointToClient(MousePosition).Y - pictureBox1.Location.Y))/ osztasMerteke).ToString(); //Y
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                button8.Enabled = true;
            }
            else {
                button8.Enabled = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                button8.Enabled = true;
            }
            else
            {
                button8.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            kepszamlalo++;
            button4.Enabled = false;
            if (kattintott)
            {
                button4.Enabled = false;
            } else
            {
                kattintott = true;
            }
            if (ujfelhasznalo)
            {
                var szam = rand.Next(files.Length);
                currentpicIndex = szam;
                currentpic = files[szam];
                if (lastpicIndex == currentpicIndex)
                {
                    var ujszam = rand.Next(files.Length);
                    currentpicIndex = ujszam;
                    currentpic = files[ujszam];
                    textBox1.AppendText(currentpicIndex.ToString() + "\r\n");
                    accountok[szamlalo].addIndex(currentpicIndex);

                    pictureBox1.Image = Bitmap.FromFile(currentpic);

                    lastpic = currentpic;
                    lastpicIndex = currentpicIndex;
                    
                }
                else
                {
                    currentpicIndex = szam;
                    currentpic = files[szam];
                    textBox1.AppendText(currentpicIndex.ToString() + "\r\n");
                    accountok[szamlalo].addIndex(currentpicIndex);

                    pictureBox1.Image = Bitmap.FromFile(currentpic);

                    lastpic = currentpic;
                    lastpicIndex = currentpicIndex;
                    
                }

                // felhJelszava += mousePosX.ToString() + mousePosY.ToString();
                //textBox1.AppendText(felhJelszava);
                
                label6.Text = "Maradék kép: " + (3 - kepszamlalo).ToString();
                if (kepszamlalo == 3)
                {
                    //pictureBox1.Visible = true;
                    //pictureBox1.Visible = false;
                    button4.Enabled = false;
                    button3.Enabled = true;
                }
                else { pictureBox1.Enabled = true; }
                if (kepszamlalo == 3 && felhJelszava == null) { button3.Enabled = false; }
            } else if(meglevofelhasznalo)
            {
                pictureBox1.Enabled = true;
                indexSzamlalo++;
                pictureBox1.Image = Bitmap.FromFile(files[deserializalt[meglevofelhindex].jelszoKepIndexek[indexSzamlalo]]);
                textBox1.AppendText(deserializalt[meglevofelhindex].jelszoKepIndexek[indexSzamlalo].ToString() + "    " + indexSzamlalo + "\r\n");
                //felhJelszava += mousePosX.ToString() + mousePosY.ToString();
                //textBox1.AppendText(felhJelszava);
                label6.Text = "Maradék kép: " + (3 - kepszamlalo).ToString();

                if (kepszamlalo == 3 || indexSzamlalo==3)
                {
                    
                    //pictureBox1.Visible = true;

                    button4.Enabled = false;
                    button2.Visible = true;
                    button2.Enabled = true;
                    button8.Visible = false;
                }
            }       
        }

        public void Reset()
        {
                kepszamlalo = 0;
                label6.Text = "Maradék kép: " + (3 - kepszamlalo).ToString();
                jsonstring = "";
                indexSzamlalo = 0;
                felhJelszava = "";
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                pictureBox1.Enabled = true;
                button4.Enabled = false;
                button3.Visible = false;
                button2.Visible = false;
                button6.Enabled = true;
                button7.Visible = true;
                button7.Enabled = true;
                ujfelhasznalo = false;
                meglevofelhasznalo = false;
                pictureBox1.Image = null;
                letezik = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Reset();   
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Deserialize();
            textBox1.AppendText(deserializalt.Length.ToString());
            UjFelhasznalo();
        }

        public void GombokAlap() 
        {
            button6.Visible = true;
            button7.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            pictureBox1.Visible = false;
            button8.Visible = false;
        }

        public void UjFelhasznalo()
        {
            button3.Visible = true;
            button3.Enabled = false;
            button4.Visible = true;
            button5.Visible = true;
            button7.Visible = false;
            button6.Visible = false;
            button8.Visible = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;

            ujfelhasznalo = true;
        }

        public void Meglevofelhasznalo()
        {
            button7.Visible = false;
            button7.Enabled = false;
            button8.Visible = true;
            button6.Visible = false;
            button4.Visible = true;
            button3.Visible = false;
            button2.Visible = true;
            button2.Enabled = false;
            button8.Visible = true;
            button5.Visible = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;

            pictureBox1.Visible = false;
            meglevofelhasznalo = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Deserialize();
            textBox1.AppendText(deserializalt.Length.ToString());
            Meglevofelhasznalo();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            felhJelszava = null;

            if (deserializalt.Length > 0 && ujfelhasznalo)
            {
                for (int i = 0; i < deserializalt.Length; i++)
                {
                    if (deserializalt[i].felhasznaloNev == textBox2.Text)
                    {
                        //letezik = true;
                        MessageBox.Show("Ilyen felhasználó már létezik!");
                        GombokAlap();
                        Reset();
                        //szamlalo--;
                    }
                }
            }


            if (ujfelhasznalo)
            {


               


                // accountok[szamlalo] = new felhasznalo();

                accountok[szamlalo] = (new felhasznalo
                {
                  felhasznaloNev = textBox2.Text,
                  email = textBox3.Text,
                });

                //accountok[szamlalo].felhasznaloNev = textBox2.Text;
                //accountok[szamlalo].email = textBox3.Text;
                
                textBox1.AppendText("Most adjon meg egy jelszót a: " + textBox2.Text + " felhasználóhoz" + "\r\n");
                pictureBox1.Visible = true;
                button8.Visible = false;
                //button4.Enabled = true;

                var szam = rand.Next(files.Length);
                currentpicIndex = szam;
                currentpic = files[szam];
                textBox1.AppendText(currentpicIndex.ToString() + "\r\n");
                accountok[szamlalo].addIndex(currentpicIndex);
                pictureBox1.Image = Bitmap.FromFile(currentpic);

                kepszamlalo++;
                label6.Text = "Maradék kép: " + (3 - kepszamlalo).ToString();

                //textBox1.AppendText(szam.ToString());
                lastpic = currentpic;
                lastpicIndex = currentpicIndex;

                button3.Enabled = false;

            } 
            else if(meglevofelhasznalo)
            {
                pictureBox1.Visible = true;
                for (int i = 0; i < deserializalt.Length; i++)
                {
                    if (deserializalt[i].felhasznaloNev == textBox2.Text && deserializalt[i].email == textBox3.Text)
                    {
                        label7.Text = "";
                        pictureBox1.Image = Bitmap.FromFile(files[deserializalt[i].jelszoKepIndexek[indexSzamlalo]]);
                        //indexSzamlalo++;
                        kepszamlalo++;
                        MessageBox.Show("Felhasználó megtalálva, most adja meg a jelszavát!");
                        
                        meglevofelhindex = i;
                        //felhJelszava = null;
                        //button4.Enabled = true;
                        pictureBox1.Enabled = true;
                        break;
                    } else { label7.Text = "Rossz felhasználó vagy jelszó!"; }
                }
                //kepszamlalo = 0;
                label6.Text = "Maradék kép: " + (3 - kepszamlalo).ToString();
            }
            
        }

       
    }
}
