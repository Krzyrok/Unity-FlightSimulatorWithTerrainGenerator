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
        int[] Pixels = null;        // for ".raw" map files
        int[,] Pixels2D = null;     // for other map files
        int MapWidth = 0;
        int MapHeight = 0;

        public Form1()
        {
            InitializeComponent();

            setButtons(true, false, false, false);
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog1.SafeFileName;

                if (fileName.Contains(".raw") || fileName.Contains(".RAW"))
                {
                    setButtons(false, true, true, true);

                    byte[] buffer = System.IO.File.ReadAllBytes(openFileDialog1.FileName);

                    Pixels = new int[buffer.Length/3];

                    int j = 0;
                    for (int i = 0; i < buffer.Length; i+=3)
                    {
                        Pixels[j] = buffer[i];
                        j++;
                    }

                    Output.Text += "Raw map successfully loaded.\n";
                }
                else if (fileName.Contains(".jpg") || fileName.Contains(".jpeg") || fileName.Contains(".bmp") ||
                         fileName.Contains(".gif") || fileName.Contains(".png") || fileName.Contains(".tiff") ||
                         fileName.Contains(".JPG") || fileName.Contains(".JPEG") || fileName.Contains(".BMP") ||
                         fileName.Contains(".GIF") || fileName.Contains(".PNG") || fileName.Contains(".TIFF"))
                {
                    setButtons(false, true, true, true);

                    Bitmap bitmap = (Bitmap)Image.FromFile(openFileDialog1.FileName);
                    
                    MapWidth = bitmap.Width;
                    MapHeight = bitmap.Height;

                    Pixels2D = new int[MapWidth, MapHeight];

                    Color pixel;

                    for (int x = 0; x < MapWidth; x++)
                        for (int y = 0; y < MapHeight; y++)
                        {
                            pixel = bitmap.GetPixel(x, y);
                            Pixels2D[x,y] = pixel.R;
                        }

                    bitmap.Dispose();

                    Output.Text += "Map " + MapWidth + " x " + MapHeight + "  successfully loaded.\n";
                }
                else
                    Output.Text += "File format not supported.\n";
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Pixels != null)
            {
                saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName);

                    for (int i = 0; i < Pixels.Length; i++)
                        file.WriteLine(Pixels[i]);

                    file.Close();

                    Output.Text += "Data successfully saved into file.\n";
                }
            }
            else if (Pixels2D != null)
            {
                saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName);

                    for (int j = 0; j < Pixels2D.GetLength(1); j++)     // MapHeight
                        for (int i = 0; i < Pixels2D.GetLength(0); i++) // MapWidth
                            file.WriteLine(Pixels2D[i, j]);

                    file.Close();

                    Output.Text += "Data successfully saved into file.\n";
                }
            }
            else
                Output.Text += "There is no data to save.\n";
        }

        private void buttonUnload_Click(object sender, EventArgs e)
        {
            Pixels = null;
            Pixels2D = null;

            setButtons(true, false, false, false);
        }

        private void setButtons(bool bSelect, bool bUnload, bool bSave, bool bStart)
        {
            buttonSelect.Enabled = bSelect;
            buttonUnload.Enabled = bUnload;
            buttonSave.Enabled = bSave;
            buttonStart.Enabled = bStart;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
