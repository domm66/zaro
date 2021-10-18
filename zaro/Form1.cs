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

namespace zaro
{
    public partial class Form1 : Form
    {
        int mousePosX; //szelesseg
        int mousePosY; //magassag
 
        int szamlalo=0;
        felhasznalo[] accountok = new felhasznalo[999999];
        String felhJelszava;
        int currentpicIndex;
        String currentpic;
        int lastpicIndex;
        String lastpic;
        Random rand = new Random();
        String[] files = Directory.GetFiles(@"C:\Users\This dude\source\repos\zaro\zaro\pics");
        bool ujfelhasznalo = false;
        bool meglevofelhasznalo;
        int meglevofelhindex;

        int indexSzamlalo=0;
        int kepszamlalo = 0;

        public Form1()
        {
            InitializeComponent();

            button2.Visible = false;
            button3.Visible = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            pictureBox1.Visible = false;
            button8.Visible = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mousePosX = (this.PointToClient(MousePosition).X - pictureBox1.Location.X) / 40; //szelesseg eldarabol 20x20 negyszogekre
            mousePosY = (this.PointToClient(MousePosition).Y - pictureBox1.Location.Y) / 40; //magassag eladarabolva szinten 20x20 negyszogekre
            textBox1.AppendText("\r\n" + "X: " + mousePosX.ToString() + " Y: " + mousePosY.ToString() + "\r\n");
            felhJelszava += mousePosX.ToString() + mousePosY.ToString();
            textBox1.AppendText(felhJelszava + "\r\n");
            //kepszamlalo++;
            //pictureBox1.Enabled = false;
        }

        public void Regisztracio() {
            
            bool letezik=false;
            ujfelhasznalo = false;

            if (kepszamlalo==3 && felhJelszava.Length >= 6 || felhJelszava != null) {
                if (szamlalo > 0)
                {
                    for (int i = 0; i < szamlalo; i++)
                    {
                        if (accountok[i].getNev() == accountok[szamlalo].getNev())
                        {
                            letezik = true;
                            szamlalo--;
                        }
                    }
                }
                if (!letezik) {
                    accountok[szamlalo].addJelszo(felhJelszava);
                    textBox1.AppendText("Sikeres mentés!" + "\r\n" + accountok[szamlalo].getNev() + " " + accountok[szamlalo].getEmail() + " " + accountok[szamlalo].getJelszo() + "\r\n");
                    felhJelszava = "";
                    kepszamlalo = 0;

                    //accountok[szamlalo] = new felhasznalo(textBox2.Text, textBox3.Text);
                    
                    textBox2.Clear();
                    textBox3.Clear();
                    felhJelszava = null;
                    button4.Enabled = false;
                    pictureBox1.Enabled = true;
                    MessageBox.Show("Sikeres jelszó regisztráció!");
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    //kep indexek
                    //for (int i = 0; i < 3; i++)
                    //{
                    //   textBox1.AppendText(accountok[szamlalo].getIndex(i).ToString());
                    //}
                    
                } else 
                {
                    MessageBox.Show("Ilyen felhasználónév már létezik!");
                }
                szamlalo++;
            } else
            {
                MessageBox.Show("Adj meg egy megfelelő hosszúságú jelszót először!");
            }

            label6.Text = "Maradék kép: " + (3 - kepszamlalo).ToString();
        }

        public void Jelszomegadas() {
            if(accountok[meglevofelhindex].getJelszo() == felhJelszava)
            {
                MessageBox.Show("Jelszó elfogadva!");
            } else
            {
                MessageBox.Show("Rossz jelszó, próbálja meg újra!");
                Reset();
            }
            meglevofelhasznalo = false;
            button2.Visible = false;
            button6.Enabled = true;
            button8.Visible = false;
            button7.Visible = true;
            button4.Enabled = false;
            textBox2.Clear();
            textBox3.Clear();
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            felhJelszava = null;
            
            pictureBox1.Visible = false;

            kepszamlalo = 0;
            label6.Text = "Maradék kép: " + (3 - kepszamlalo).ToString();
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
            textBox1.AppendText("jelszo:" + accountok[szamlalo - 1].getJelszo().ToString());
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Text = "X: " + (((this.PointToClient(MousePosition).X - pictureBox1.Location.X)) / 40).ToString(); //X
            label3.Text = "Y: " + (((this.PointToClient(MousePosition).Y - pictureBox1.Location.Y))/ 40).ToString(); //Y
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                button2.Enabled = true;
                //button3.Enabled = true;
                button8.Enabled = true;
            }
            else {
                button2.Enabled = false;
                //button3.Enabled = false;
                button8.Enabled = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                button2.Enabled = true;
                //button3.Enabled = true;
                button8.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
                //button3.Enabled = false;
                button8.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            kepszamlalo++;
            if (ujfelhasznalo)
            {
                var szam = rand.Next(files.Length);
                currentpicIndex = szam;
                currentpic = files[szam];
                if (lastpicIndex == currentpicIndex)
                {
                    var ujszam = rand.Next(files.Length);
                    //textBox1.AppendText(ujszam.ToString() + "\r\n");
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
                   
                   // pictureBox1.Visible = true;
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
                pictureBox1.Image = Bitmap.FromFile(files[accountok[meglevofelhindex].getIndex(indexSzamlalo)]);
                textBox1.AppendText(accountok[meglevofelhindex].getIndex(indexSzamlalo).ToString() + "    " + indexSzamlalo + "\r\n");
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
            if (ujfelhasznalo || meglevofelhasznalo)
            {
                kepszamlalo = 0;
                label6.Text = "Maradék kép: " + (3 - kepszamlalo).ToString();
                indexSzamlalo = 0;
                felhJelszava = "";
                textBox1.Clear();
                textBox2.Clear();
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
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Reset();   
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button3.Visible = true;
            button2.Visible = false;
            button6.Enabled = false;
            button7.Visible = false;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            button8.Visible = true;

            ujfelhasznalo = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Visible = false;
            button8.Visible = true;
            button6.Enabled = false;
            button3.Visible = false;
            button2.Visible = false;
            textBox2.Enabled = true;
            textBox3.Enabled = true;

            pictureBox1.Visible = false;
            meglevofelhasznalo = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            felhJelszava = null;

            if (ujfelhasznalo)
            {
                accountok[szamlalo] = new felhasznalo(textBox2.Text, textBox3.Text);
                textBox1.AppendText("Most adjon meg egy jelszót a: " + accountok[szamlalo].getNev().ToString() + " felhasználóhoz" + "\r\n");
                pictureBox1.Visible = true;
                button8.Visible = false;
                button4.Enabled = true;

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

            } else if(meglevofelhasznalo)
            {
                pictureBox1.Visible = true;
                for (int i = 0; i < szamlalo; i++)
                {
                    if (accountok[i].getNev() == textBox2.Text && accountok[i].getEmail() == textBox3.Text)
                    {

                        pictureBox1.Image = Bitmap.FromFile(files[accountok[i].getIndex(indexSzamlalo)]);
                        //indexSzamlalo++;
                        kepszamlalo++;
                        MessageBox.Show("Felhasználó megtalálva, most adja meg a jelszavát!");
                        
                        meglevofelhindex = i;
                        //felhJelszava = null;
                        button4.Enabled = true;
                        pictureBox1.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Nem létezik ilyen felhasználó!");
                        textBox2.Clear();
                        textBox3.Clear();
                        //felhJelszava = null;
                        button4.Enabled = true;
                        pictureBox1.Visible = false;
                    }
                }
                //kepszamlalo = 0;
                label6.Text = "Maradék kép: " + (3 - kepszamlalo).ToString();
            }
            
        }
    }
}
