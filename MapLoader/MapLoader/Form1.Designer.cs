namespace MapLoader
{
    partial class Form1
    {
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
            this.SuspendLayout();
            // 
            // Output
            // 
            this.Output.Location = new System.Drawing.Point(23, 81);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(322, 200);
            this.Output.TabIndex = 0;
            this.Output.Text = "";
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(52, 23);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(94, 41);
            this.buttonSelect.TabIndex = 1;
            this.buttonSelect.Text = "Select map file";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(226, 23);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 296);
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
    }
}

