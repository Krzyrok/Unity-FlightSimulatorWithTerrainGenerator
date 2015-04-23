using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using UnityEngine;

namespace MapLoader
{
    public class Form1 : Form
    {
		//-------------------------------------------------------------------------------------------------------------
		// Form1 Design:

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		
		#region Windows Form Designer generated code
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Output = new System.Windows.Forms.RichTextBox();
			this.buttonSelect = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.buttonUnload = new System.Windows.Forms.Button();
			this.buttonStart = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Output
			// 
			this.Output.Location = new System.Drawing.Point(23, 81);
			this.Output.Name = "Output";
			this.Output.Size = new System.Drawing.Size(322, 142);
			this.Output.TabIndex = 0;
			this.Output.Text = "";
			// 
			// buttonSelect
			// 
			this.buttonSelect.Location = new System.Drawing.Point(23, 23);
			this.buttonSelect.Name = "buttonSelect";
			this.buttonSelect.Size = new System.Drawing.Size(94, 41);
			this.buttonSelect.TabIndex = 1;
			this.buttonSelect.Text = "Select map file";
			this.buttonSelect.UseVisualStyleBackColor = true;
			this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(251, 23);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(94, 41);
			this.buttonSave.TabIndex = 3;
			this.buttonSave.Text = "Save data";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// buttonUnload
			// 
			this.buttonUnload.Location = new System.Drawing.Point(137, 23);
			this.buttonUnload.Name = "buttonUnload";
			this.buttonUnload.Size = new System.Drawing.Size(94, 41);
			this.buttonUnload.TabIndex = 4;
			this.buttonUnload.Text = "Unload map";
			this.buttonUnload.UseVisualStyleBackColor = true;
			this.buttonUnload.Click += new System.EventHandler(this.buttonUnload_Click);
			// 
			// buttonStart
			// 
			this.buttonStart.Location = new System.Drawing.Point(117, 243);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(138, 41);
			this.buttonStart.TabIndex = 5;
			this.buttonStart.Text = "Start Simulation";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(372, 296);
			this.Controls.Add(this.buttonStart);
			this.Controls.Add(this.buttonUnload);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.buttonSelect);
			this.Controls.Add(this.Output);
			this.Name = "Form1";
			this.Text = "Map Loader";
			this.ResumeLayout(false);
			
		}
		
		#endregion
		
		private System.Windows.Forms.RichTextBox Output;
		private System.Windows.Forms.Button buttonSelect;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button buttonUnload;
		private System.Windows.Forms.Button buttonStart;

		//-------------------------------------------------------------------------------------------------------------


        int[] Pixels = null;        // for ".raw" map files
        int[,] Pixels2D = null;     // for other map files
        int MapWidth = 0;
        int MapHeight = 0;

        public Form1()
        {
            InitializeComponent();

            //setButtons(true, false, false, false);
			setButtons(true, false, false);
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog1.SafeFileName;

                if (fileName.Contains(".raw") || fileName.Contains(".RAW"))
                {
                    //setButtons(false, true, true, true);
					setButtons(false, true, true);

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
                    //setButtons(false, true, true, true);
					setButtons(false, true, true);

                    Bitmap bitmap = (Bitmap)Image.FromFile(openFileDialog1.FileName);
                    
                    MapWidth = bitmap.Width;
                    MapHeight = bitmap.Height;

                    Pixels2D = new int[MapWidth, MapHeight];

                    System.Drawing.Color pixel;

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
                string fileName = "height_values.txt";
                System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);

                for (int i = 0; i < Pixels.Length; i++)
                    file.WriteLine(Pixels[i]);

                file.Close();

                Output.Text += "Data successfully saved into file map.txt.\n";
            }
            else if (Pixels2D != null)
            {
                string fileName = "height_values.txt";
                System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);

                for (int j = 0; j < Pixels2D.GetLength(1); j++)     // MapHeight
                    for (int i = 0; i < Pixels2D.GetLength(0); i++) // MapWidth
                        file.WriteLine(Pixels2D[i,j]);

                file.Close();

                Output.Text += "Data successfully saved into file map.txt.\n";
            }
            else
                Output.Text += "There is no data to save.\n";
        }

        private void buttonUnload_Click(object sender, EventArgs e)
        {
            Pixels = null;
            Pixels2D = null;

            //setButtons(true, false, false, false);
			setButtons(true, false, false);
        }

        //private void setButtons(bool bSelect, bool bUnload, bool bSave, bool bStart)
		private void setButtons(bool bSelect, bool bUnload, bool bSave, bool bStart = true)
        {
            buttonSelect.Enabled = bSelect;
            buttonUnload.Enabled = bUnload;
            buttonSave.Enabled = bSave;
            buttonStart.Enabled = bStart;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
			UnityEngine.Application.LoadLevel("SimulationScene");
            this.Close();
        }
    }
}
