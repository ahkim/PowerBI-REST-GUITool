namespace PowerBI_REST_GUITool
{
    partial class MainForm
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
            this.tbPayload = new System.Windows.Forms.TextBox();
            this.tbClientID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPOST = new System.Windows.Forms.Button();
            this.btnGETDatasets = new System.Windows.Forms.Button();
            this.btnGetTables = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbPayload
            // 
            this.tbPayload.Location = new System.Drawing.Point(12, 37);
            this.tbPayload.Multiline = true;
            this.tbPayload.Name = "tbPayload";
            this.tbPayload.Size = new System.Drawing.Size(804, 715);
            this.tbPayload.TabIndex = 0;
            // 
            // tbClientID
            // 
            this.tbClientID.Location = new System.Drawing.Point(87, 9);
            this.tbClientID.Name = "tbClientID";
            this.tbClientID.Size = new System.Drawing.Size(288, 20);
            this.tbClientID.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "ClientID";
            // 
            // btnPOST
            // 
            this.btnPOST.Location = new System.Drawing.Point(385, 7);
            this.btnPOST.Name = "btnPOST";
            this.btnPOST.Size = new System.Drawing.Size(75, 23);
            this.btnPOST.TabIndex = 5;
            this.btnPOST.Text = "POST";
            this.btnPOST.UseVisualStyleBackColor = true;
            this.btnPOST.Click += new System.EventHandler(this.btnPOST_Click);
            // 
            // btnGETDatasets
            // 
            this.btnGETDatasets.Location = new System.Drawing.Point(466, 7);
            this.btnGETDatasets.Name = "btnGETDatasets";
            this.btnGETDatasets.Size = new System.Drawing.Size(92, 23);
            this.btnGETDatasets.TabIndex = 6;
            this.btnGETDatasets.Text = "GET Datasets";
            this.btnGETDatasets.UseVisualStyleBackColor = true;
            this.btnGETDatasets.Click += new System.EventHandler(this.btnGETDatasets_Click);
            // 
            // btnGetTables
            // 
            this.btnGetTables.Location = new System.Drawing.Point(564, 7);
            this.btnGetTables.Name = "btnGetTables";
            this.btnGetTables.Size = new System.Drawing.Size(92, 23);
            this.btnGetTables.TabIndex = 7;
            this.btnGetTables.Text = "GET Tables";
            this.btnGetTables.UseVisualStyleBackColor = true;
            this.btnGetTables.Click += new System.EventHandler(this.btnGetTables_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 764);
            this.Controls.Add(this.btnGetTables);
            this.Controls.Add(this.btnGETDatasets);
            this.Controls.Add(this.btnPOST);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbClientID);
            this.Controls.Add(this.tbPayload);
            this.Name = "MainForm";
            this.Text = "PowerBI REST Call";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPayload;
        private System.Windows.Forms.TextBox tbClientID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPOST;
        private System.Windows.Forms.Button btnGETDatasets;
        private System.Windows.Forms.Button btnGetTables;
    }
}

