using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        int imgXmentett;
        int imgYmentett;



        public Form1()
        {
            InitializeComponent();
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
            mousePosX = (this.PointToClient(MousePosition).X - pictureBox1.Location.X) / 20; //szelesseg eldarabol 20x20 negyszogekre
            mousePosY = (this.PointToClient(MousePosition).Y - pictureBox1.Location.Y) / 20; //magassag eladarabolva szinten 20x20 negyszogekre




            if (pictureBox1.Image != null)
            {
                textBox1.AppendText(mousePosX.ToString() + " + " + mousePosY.ToString() + "\r\n");
            }


        }

        public void Regisztracio() {
            imgXmentett = mousePosX;
            imgYmentett = mousePosY;
            textBox1.AppendText("Jelszó mentve!" + "\r\n" + mousePosX.ToString() + " + " + mousePosY.ToString() + "\r\n");
        }

        public void Bejelentkezes() {
            if (imgXmentett == mousePosX && imgYmentett == mousePosY)
            {
                textBox1.AppendText("Jelszó egyezik!" + "\r\n");
                progressBar1.Value = 100;
                MessageBox.Show("Sikeres belépés!");
            }
            else
            {
                MessageBox.Show("Rossz jelszó!");  
            }
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
    }
}
