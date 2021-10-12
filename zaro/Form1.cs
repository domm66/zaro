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
        
        int kepszamlalo = 0;

        public Form1()
        {
            InitializeComponent();
            var szam = rand.Next(files.Length);
            currentpicIndex = szam;
            currentpic = files[szam];
            pictureBox1.Image = Bitmap.FromFile(currentpic);
            
            //textBox1.AppendText(szam.ToString());
            lastpic = currentpic;
            lastpicIndex = currentpicIndex;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog testPic = new OpenFileDialog();
            if (testPic.ShowDialog() == DialogResult.OK) {
                pictureBox1.Image = Bitmap.FromFile(testPic.FileName);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mousePosX = (this.PointToClient(MousePosition).X - pictureBox1.Location.X) / 40; //szelesseg eldarabol 20x20 negyszogekre
            mousePosY = (this.PointToClient(MousePosition).Y - pictureBox1.Location.Y) / 40; //magassag eladarabolva szinten 20x20 negyszogekre
            textBox1.AppendText("\r\n" + "X: " + mousePosX.ToString() + " Y: " + mousePosY.ToString() + "\r\n");
            //felhJelszava += mousePosX.ToString() + mousePosY.ToString();
            kepszamlalo++;
            pictureBox1.Enabled = false;
        }

        public void Regisztracio() {
            
            bool letezik=false;

            if(kepszamlalo==3) {

                kepszamlalo = 0;

                accountok[szamlalo] = new felhasznalo(textBox2.Text, felhJelszava, textBox3.Text);

                textBox2.Clear();
                textBox3.Clear();
                felhJelszava = null;
                button4.Enabled = true;

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
                if (!letezik) { textBox1.AppendText("Sikeres mentés!" + "\r\n" + accountok[szamlalo].getNev() + " " + accountok[szamlalo].getEmail() + " " + accountok[szamlalo].getJelszo() + "\r\n"); felhJelszava = ""; }
                else { MessageBox.Show("Ilyen felhasználónév már létezik!"); }

                szamlalo++;
            } else
            {
                MessageBox.Show("Adj meg egy megfelelő hosszúságú jelszót először!");
            }
            label6.Text = "Maradék kép: " + (3 - kepszamlalo).ToString();

        }

        public void Bejelentkezes() {
            for (int i=0;i<szamlalo;i++) {
                if (accountok[i].getNev() == textBox2.Text && accountok[i].getJelszo() == felhJelszava)
                {
                    MessageBox.Show("Sikeres belépes!");
                    textBox2.Clear();
                    textBox3.Clear();
                    felhJelszava = null;
                    button4.Enabled = true;
                }
            }
            kepszamlalo = 0;
            label6.Text = "Maradék kép: " + (3 - kepszamlalo).ToString();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Bejelentkezes();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Regisztracio();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
        }

        private void Form1_Click(object sender, EventArgs e)
        {     
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {    
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            
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
                button3.Enabled = true;
            }
            else {
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                button2.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var szam = rand.Next(files.Length);
            currentpicIndex = szam;
            currentpic = files[szam];
            if (lastpicIndex == currentpicIndex)
            {
                var ujszam = rand.Next(files.Length);
                currentpicIndex = ujszam;
                currentpic = files[ujszam];
                
                pictureBox1.Image = Bitmap.FromFile(currentpic);

                lastpic = currentpic;
                lastpicIndex = currentpicIndex;

            } else
            {
                //textBox1.AppendText(szam.ToString());
                currentpicIndex = szam;
                currentpic = files[szam];

                pictureBox1.Image = Bitmap.FromFile(currentpic);

                lastpic = currentpic;
                lastpicIndex = currentpicIndex;
            }

            felhJelszava+= mousePosX.ToString() + mousePosY.ToString();
            textBox1.AppendText(felhJelszava);
            label6.Text = "Maradék kép: " + (3 - kepszamlalo).ToString();
            if (kepszamlalo==3)
            {
                pictureBox1.Enabled = false;
                button4.Enabled = false;
            }
            else { pictureBox1.Enabled = true; }
            
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
                label6.Text = "Maradék kép: " + (3-kepszamlalo).ToString();
                felhJelszava = "";
                kepszamlalo=0;
                pictureBox1.Enabled = true;
                button4.Enabled = true;
        }
    }
}
