using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapLoader
{
    public partial class Form1 : Form
    {
        int[] Pixels = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap bitmap = (Bitmap)Image.FromFile(openFileDialog1.FileName);

                int width = bitmap.Width;
                int height = bitmap.Height;

                Pixels = null;
                Pixels = new int[width * height + 2];
                Pixels[0] = width;
                Pixels[1] = height;

                Color pixel;


                for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    //for (int x = 0; x < width; x++)
                    {
                        pixel = bitmap.GetPixel(x, y);
                        Pixels[x + y + 2] = pixel.R;
                    }

                        bitmap.Dispose();

                string text = "Map " + width + " x " + height + "  successfully loaded.\n";
                Output.Text += text;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(Pixels == null)
                Output.Text += "There is no data to save.\n";
            else
            {
                string filePath = "../../../../Data/map.txt";
                System.IO.StreamWriter file = new System.IO.StreamWriter(filePath);

                for (int i = 0; i < Pixels.Length; i++)
                    file.WriteLine(Pixels[i]);

                file.Close();

                Output.Text += "Data successfully saved into file map.txt.\n";
            }
        }
    }
}
