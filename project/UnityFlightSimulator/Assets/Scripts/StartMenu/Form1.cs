using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using UnityEngine;

namespace MapLoader
{
    public class Form1 : Form
    {
        public float[,] m_grayLevels = null;
        public int m_mapSize = 0;
		

        public Form1()
        {
            InitializeComponent();

            setButtons(true, false, false, false);
        }


        private int readHeightmap(string fileName)  // returns '-1' if failed, '0' otherwise
        {
            if (fileName.Contains(".raw") || fileName.Contains(".RAW"))
            {
                byte[] buffer = System.IO.File.ReadAllBytes(openFileDialog1.FileName);

                if ((double)((int)Math.Sqrt(buffer.Length / 3)) == Math.Sqrt(buffer.Length / 3))
                {
                    if (Mathf.IsPowerOfTwo((int)Math.Sqrt(buffer.Length / 3)))
                    {
                        m_mapSize = (int)Math.Sqrt(buffer.Length / 3);

                        m_grayLevels = new float[m_mapSize, m_mapSize];

                        int bufferIdx = 0;
                        for (int y = 0; y < m_mapSize; y++)
                            for (int x = 0; x < m_mapSize; x++)
                            {
                                //create the grayscale level from the rgb pixel
                                m_grayLevels[x, y] = (float)((buffer[bufferIdx] * .3) + (buffer[bufferIdx + 1] * .59)
                                                          + (buffer[bufferIdx + 2] * .11)) / 255;

                                bufferIdx += 3;
                            }


                        Output.Text += "Map " + m_mapSize + " x " + m_mapSize + "  successfully loaded.\n";
                        return 0;
                    }
                    else
                    {
                        Output.Text += "Error! Heightmap size must be the power of two.\n";
                        return -1;
                    }
                }
                else
                {
                    Output.Text += "Error! Heightmap must have equal width and height.\n";
                    return -1;
                }

            }
            else if (fileName.Contains(".jpg") || fileName.Contains(".jpeg") || fileName.Contains(".bmp") ||
                     fileName.Contains(".png") || fileName.Contains(".gif") || fileName.Contains(".tiff") ||
                     fileName.Contains(".JPG") || fileName.Contains(".JPEG") || fileName.Contains(".BMP") ||
                     fileName.Contains(".PNG") || fileName.Contains(".GIF") || fileName.Contains(".TIFF"))
            {
                Bitmap bitmap = (Bitmap)Image.FromFile(openFileDialog1.FileName);

                if (bitmap.Width == bitmap.Height)
                {
                    if (Mathf.IsPowerOfTwo(bitmap.Width))
                    {
                        m_mapSize = bitmap.Width;

                        m_grayLevels = new float[m_mapSize, m_mapSize];

                        System.Drawing.Color pixel;

                        for (int x = 0; x < m_mapSize; x++)
                            for (int y = 0; y < m_mapSize; y++)
                            {
                                pixel = bitmap.GetPixel(x, y);

                                m_grayLevels[x, y] = pixel.GetBrightness();
                            }


                        bitmap.Dispose();

                        Output.Text += "Map " + m_mapSize + " x " + m_mapSize + "  successfully loaded.\n";
                        return 0;
                    }
                    else
                    {
                        Output.Text += "Error! Heightmap size must be the power of two.\n";
                        return -1;
                    }
                }
                else
                {
                    Output.Text += "Error! Heightmap must have equal width and height.\n";
                    return -1;
                }

            }
            else if (fileName.Contains(".txt") || fileName.Contains(".TXT"))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(openFileDialog1.FileName);

                int arg1 = System.Int32.Parse(file.ReadLine());     // map width
                int arg2 = System.Int32.Parse(file.ReadLine());     // map height

                if (arg1 == arg2)
                {
                    if (Mathf.IsPowerOfTwo(arg1))
                    {
                        m_mapSize = arg1;

                        m_grayLevels = new float[m_mapSize, m_mapSize];

                        for (int y = 0; y < m_mapSize; y++)
                            for (int x = 0; x < m_mapSize; x++)
                                m_grayLevels[x, y] = System.Int32.Parse(file.ReadLine());


                        file.Close();
                        Output.Text += "Map " + m_mapSize + " x " + m_mapSize + "  successfully loaded.\n";
                        return 0;
                    }
                    else
                    {
                        file.Close();
                        Output.Text += "Error! Heightmap size must be the power of two.\n";
                        return -1;
                    }
                }
                else
                {
                    file.Close();
                    Output.Text += "Error! Heightmap must have equal width and height.\n";
                    return -1;
                }

            }
            else
            {
                Output.Text += "Error! File format not supported.\n";
                return -1;
            }

        }

        private void saveHeightmap(string fileName)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
            

            file.WriteLine(m_mapSize);
            file.WriteLine(m_mapSize);

            for (int y = 0; y < m_mapSize; y++)
                for (int x = 0; x < m_mapSize; x++)
                    file.WriteLine(m_grayLevels[x, y]);


            file.Close();


            Output.Text += "Data successfully saved into file.\n";
        }

        
        private void buttonSelect_Click(object sender, EventArgs e)
        {
            setButtons(false, false, false, false);

            openFileDialog1.InitialDirectory = System.IO.Path.Combine(UnityEngine.Application.dataPath, @"resources/maps");

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog1.SafeFileName;

                if ( this.readHeightmap(fileName) == -1)
                    setButtons(true, false, false, false);
                else
                    setButtons(false, true, true, true);
            }
            else
                setButtons(true, false, false, false);

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (m_grayLevels != null)
            {
                saveFileDialog1.InitialDirectory = System.IO.Path.Combine(UnityEngine.Application.dataPath, @"resources/results");
                saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileName = saveFileDialog1.FileName;

                    this.saveHeightmap(fileName);
                }
            }
            else
                Output.Text += "There is no data to save.\n";

        }

        private void buttonUnload_Click(object sender, EventArgs e)
        {
            m_grayLevels = null;
            m_mapSize = 0;
            m_mapSize = 0;

            setButtons(true, false, false, false);

            Output.Clear();
            Output.Text += "Map unloaded.\n";
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            StartMenu.m_grayLevels = m_grayLevels;
            StartMenu.m_mapSize = m_mapSize;

            UnityEngine.Application.LoadLevel("SimulationScene");
            this.Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            UnityEngine.Application.Quit();
            this.Close();
        }


        private void setButtons(bool bSelect, bool bUnload, bool bSave, bool bStart, bool bExit = true)
        {
            buttonSelect.Enabled = bSelect;
            buttonUnload.Enabled = bUnload;
            buttonSave.Enabled = bSave;
            buttonStart.Enabled = bStart;
            buttonExit.Enabled = bExit;
        }


        //-------------------------------------------------------------------------------------------------------------
        #region Form1 Design

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
            this.buttonUnload = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
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
            this.buttonStart.Location = new System.Drawing.Point(117, 239);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(138, 41);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "Start Simulation";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(117, 296);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(138, 41);
            this.buttonExit.TabIndex = 6;
            this.buttonExit.Text = "Exit Program";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 354);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonUnload);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.Output);
            this.Name = "Form1";
            this.Text = "Unity Flight Simulator";
            this.ResumeLayout(false);

        }

        #endregion  // "Windows Form Designer generated code"

        private System.Windows.Forms.RichTextBox Output;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonUnload;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

        #endregion  // "Form1 Design"
        //-------------------------------------------------------------------------------------------------------------

    }

}
