namespace Sistemas_Lineares
{
    partial class FrmSobre
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSobre));
            this.btnTelaInicial = new System.Windows.Forms.Button();
            this.lblIntegrantes = new System.Windows.Forms.Label();
            this.lblSobre = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTelaInicial
            // 
            this.btnTelaInicial.BackgroundImage = global::Sistemas_Lineares.Properties.Resources.plano_fundo_claro;
            this.btnTelaInicial.FlatAppearance.BorderSize = 0;
            this.btnTelaInicial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTelaInicial.Image = global::Sistemas_Lineares.Properties.Resources.botao_inicio1;
            this.btnTelaInicial.Location = new System.Drawing.Point(534, 46);
            this.btnTelaInicial.Name = "btnTelaInicial";
            this.btnTelaInicial.Size = new System.Drawing.Size(117, 51);
            this.btnTelaInicial.TabIndex = 13;
            this.btnTelaInicial.UseVisualStyleBackColor = true;
            this.btnTelaInicial.Click += new System.EventHandler(this.btnTelaInicial_Click);
            // 
            // lblIntegrantes
            // 
            this.lblIntegrantes.AutoSize = true;
            this.lblIntegrantes.BackColor = System.Drawing.Color.Transparent;
            this.lblIntegrantes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblIntegrantes.Image = global::Sistemas_Lineares.Properties.Resources.integrantes;
            this.lblIntegrantes.Location = new System.Drawing.Point(133, 160);
            this.lblIntegrantes.MaximumSize = new System.Drawing.Size(500, 500);
            this.lblIntegrantes.MinimumSize = new System.Drawing.Size(350, 250);
            this.lblIntegrantes.Name = "lblIntegrantes";
            this.lblIntegrantes.Size = new System.Drawing.Size(350, 250);
            this.lblIntegrantes.TabIndex = 1;
            // 
            // lblSobre
            // 
            this.lblSobre.AutoSize = true;
            this.lblSobre.BackColor = System.Drawing.Color.Transparent;
            this.lblSobre.Font = new System.Drawing.Font("Century Gothic", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSobre.Image = global::Sistemas_Lineares.Properties.Resources.sobre;
            this.lblSobre.Location = new System.Drawing.Point(159, 33);
            this.lblSobre.MaximumSize = new System.Drawing.Size(500, 500);
            this.lblSobre.MinimumSize = new System.Drawing.Size(300, 100);
            this.lblSobre.Name = "lblSobre";
            this.lblSobre.Size = new System.Drawing.Size(300, 100);
            this.lblSobre.TabIndex = 0;
            // 
            // FrmSobre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Sistemas_Lineares.Properties.Resources.plano_fundo_claro;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.btnTelaInicial);
            this.Controls.Add(this.lblIntegrantes);
            this.Controls.Add(this.lblSobre);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "FrmSobre";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sobre";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSobre;
        private System.Windows.Forms.Label lblIntegrantes;
        private System.Windows.Forms.Button btnTelaInicial;
    }
}