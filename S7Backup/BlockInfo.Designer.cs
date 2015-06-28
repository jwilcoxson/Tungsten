namespace S7Backup
{
    partial class BlockInfo
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
            this.lblBlock = new System.Windows.Forms.Label();
            this.txtBlock = new System.Windows.Forms.TextBox();
            this.lblLoadSize = new System.Windows.Forms.Label();
            this.txtLoadSize = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.txtFamily = new System.Windows.Forms.TextBox();
            this.lblFamily = new System.Windows.Forms.Label();
            this.txtMC7Size = new System.Windows.Forms.TextBox();
            this.lblMC7Size = new System.Windows.Forms.Label();
            this.txtChecksum = new System.Windows.Forms.TextBox();
            this.lblChecksum = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblBlock
            // 
            this.lblBlock.AutoSize = true;
            this.lblBlock.Location = new System.Drawing.Point(13, 13);
            this.lblBlock.Name = "lblBlock";
            this.lblBlock.Size = new System.Drawing.Size(34, 13);
            this.lblBlock.TabIndex = 0;
            this.lblBlock.Text = "Block";
            // 
            // txtBlock
            // 
            this.txtBlock.Location = new System.Drawing.Point(13, 30);
            this.txtBlock.Name = "txtBlock";
            this.txtBlock.ReadOnly = true;
            this.txtBlock.Size = new System.Drawing.Size(100, 20);
            this.txtBlock.TabIndex = 1;
            // 
            // lblLoadSize
            // 
            this.lblLoadSize.AutoSize = true;
            this.lblLoadSize.Location = new System.Drawing.Point(10, 53);
            this.lblLoadSize.Name = "lblLoadSize";
            this.lblLoadSize.Size = new System.Drawing.Size(54, 13);
            this.lblLoadSize.TabIndex = 2;
            this.lblLoadSize.Text = "Load Size";
            // 
            // txtLoadSize
            // 
            this.txtLoadSize.Location = new System.Drawing.Point(13, 69);
            this.txtLoadSize.Name = "txtLoadSize";
            this.txtLoadSize.ReadOnly = true;
            this.txtLoadSize.Size = new System.Drawing.Size(100, 20);
            this.txtLoadSize.TabIndex = 3;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(119, 30);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 5;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(119, 13);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Name";
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(225, 30);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.ReadOnly = true;
            this.txtAuthor.Size = new System.Drawing.Size(100, 20);
            this.txtAuthor.TabIndex = 7;
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(225, 13);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(38, 13);
            this.lblAuthor.TabIndex = 6;
            this.lblAuthor.Text = "Author";
            // 
            // txtFamily
            // 
            this.txtFamily.Location = new System.Drawing.Point(331, 30);
            this.txtFamily.Name = "txtFamily";
            this.txtFamily.ReadOnly = true;
            this.txtFamily.Size = new System.Drawing.Size(100, 20);
            this.txtFamily.TabIndex = 9;
            // 
            // lblFamily
            // 
            this.lblFamily.AutoSize = true;
            this.lblFamily.Location = new System.Drawing.Point(331, 13);
            this.lblFamily.Name = "lblFamily";
            this.lblFamily.Size = new System.Drawing.Size(36, 13);
            this.lblFamily.TabIndex = 8;
            this.lblFamily.Text = "Family";
            // 
            // txtMC7Size
            // 
            this.txtMC7Size.Location = new System.Drawing.Point(119, 69);
            this.txtMC7Size.Name = "txtMC7Size";
            this.txtMC7Size.ReadOnly = true;
            this.txtMC7Size.Size = new System.Drawing.Size(100, 20);
            this.txtMC7Size.TabIndex = 11;
            // 
            // lblMC7Size
            // 
            this.lblMC7Size.AutoSize = true;
            this.lblMC7Size.Location = new System.Drawing.Point(119, 52);
            this.lblMC7Size.Name = "lblMC7Size";
            this.lblMC7Size.Size = new System.Drawing.Size(52, 13);
            this.lblMC7Size.TabIndex = 10;
            this.lblMC7Size.Text = "MC7 Size";
            // 
            // txtChecksum
            // 
            this.txtChecksum.Location = new System.Drawing.Point(228, 69);
            this.txtChecksum.Name = "txtChecksum";
            this.txtChecksum.ReadOnly = true;
            this.txtChecksum.Size = new System.Drawing.Size(100, 20);
            this.txtChecksum.TabIndex = 13;
            // 
            // lblChecksum
            // 
            this.lblChecksum.AutoSize = true;
            this.lblChecksum.Location = new System.Drawing.Point(228, 52);
            this.lblChecksum.Name = "lblChecksum";
            this.lblChecksum.Size = new System.Drawing.Size(57, 13);
            this.lblChecksum.TabIndex = 12;
            this.lblChecksum.Text = "Checksum";
            // 
            // BlockInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 382);
            this.Controls.Add(this.txtChecksum);
            this.Controls.Add(this.lblChecksum);
            this.Controls.Add(this.txtMC7Size);
            this.Controls.Add(this.lblMC7Size);
            this.Controls.Add(this.txtFamily);
            this.Controls.Add(this.lblFamily);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtLoadSize);
            this.Controls.Add(this.lblLoadSize);
            this.Controls.Add(this.txtBlock);
            this.Controls.Add(this.lblBlock);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BlockInfo";
            this.Text = "BlockInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBlock;
        private System.Windows.Forms.TextBox txtBlock;
        private System.Windows.Forms.Label lblLoadSize;
        private System.Windows.Forms.TextBox txtLoadSize;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.TextBox txtFamily;
        private System.Windows.Forms.Label lblFamily;
        private System.Windows.Forms.TextBox txtMC7Size;
        private System.Windows.Forms.Label lblMC7Size;
        private System.Windows.Forms.TextBox txtChecksum;
        private System.Windows.Forms.Label lblChecksum;
    }
}