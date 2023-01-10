namespace Sistemas_Lineares
{
    partial class FrmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.gridSistemaLinear = new System.Windows.Forms.DataGridView();
            this.nudQtdVariaveis = new System.Windows.Forms.NumericUpDown();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.ofd_Valores = new System.Windows.Forms.OpenFileDialog();
            this.btnTelaInicial = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnExemplo20 = new System.Windows.Forms.Button();
            this.btnExemplo4 = new System.Windows.Forms.Button();
            this.lblResultados = new System.Windows.Forms.Label();
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.btnExemplo3 = new System.Windows.Forms.Button();
            this.lblVariaveis = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridSistemaLinear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQtdVariaveis)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSistemaLinear
            // 
            this.gridSistemaLinear.AllowUserToAddRows = false;
            this.gridSistemaLinear.AllowUserToDeleteRows = false;
            this.gridSistemaLinear.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSistemaLinear.Location = new System.Drawing.Point(11, 65);
            this.gridSistemaLinear.Name = "gridSistemaLinear";
            this.gridSistemaLinear.RowHeadersWidth = 51;
            this.gridSistemaLinear.Size = new System.Drawing.Size(558, 205);
            this.gridSistemaLinear.TabIndex = 0;
            // 
            // nudQtdVariaveis
            // 
            this.nudQtdVariaveis.Location = new System.Drawing.Point(626, 49);
            this.nudQtdVariaveis.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudQtdVariaveis.Name = "nudQtdVariaveis";
            this.nudQtdVariaveis.Size = new System.Drawing.Size(108, 26);
            this.nudQtdVariaveis.TabIndex = 2;
            this.nudQtdVariaveis.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudQtdVariaveis.ValueChanged += new System.EventHandler(this.nudQtdVariaveis_ValueChanged);
            // 
            // txtResultado
            // 
            this.txtResultado.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtResultado.Location = new System.Drawing.Point(11, 306);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ReadOnly = true;
            this.txtResultado.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResultado.Size = new System.Drawing.Size(885, 261);
            this.txtResultado.TabIndex = 10;
            // 
            // ofd_Valores
            // 
            this.ofd_Valores.FileName = "openFileDialog1";
            // 
            // btnTelaInicial
            // 
            this.btnTelaInicial.FlatAppearance.BorderSize = 0;
            this.btnTelaInicial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTelaInicial.Image = global::Sistemas_Lineares.Properties.Resources.botao_inicio1;
            this.btnTelaInicial.Location = new System.Drawing.Point(782, 16);
            this.btnTelaInicial.Name = "btnTelaInicial";
            this.btnTelaInicial.Size = new System.Drawing.Size(114, 46);
            this.btnTelaInicial.TabIndex = 12;
            this.btnTelaInicial.UseVisualStyleBackColor = true;
            this.btnTelaInicial.Click += new System.EventHandler(this.btnTelaInicial_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Image = global::Sistemas_Lineares.Properties.Resources.csl1;
            this.lblTitulo.Location = new System.Drawing.Point(12, 5);
            this.lblTitulo.MaximumSize = new System.Drawing.Size(150, 75);
            this.lblTitulo.MinimumSize = new System.Drawing.Size(70, 30);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(108, 57);
            this.lblTitulo.TabIndex = 11;
            // 
            // btnExemplo20
            // 
            this.btnExemplo20.FlatAppearance.BorderSize = 0;
            this.btnExemplo20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExemplo20.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExemplo20.Image = global::Sistemas_Lineares.Properties.Resources.botao_20;
            this.btnExemplo20.Location = new System.Drawing.Point(795, 97);
            this.btnExemplo20.Name = "btnExemplo20";
            this.btnExemplo20.Size = new System.Drawing.Size(85, 79);
            this.btnExemplo20.TabIndex = 9;
            this.btnExemplo20.UseVisualStyleBackColor = true;
            this.btnExemplo20.Click += new System.EventHandler(this.btnExemplo20_Click);
            // 
            // btnExemplo4
            // 
            this.btnExemplo4.FlatAppearance.BorderSize = 0;
            this.btnExemplo4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExemplo4.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExemplo4.Image = global::Sistemas_Lineares.Properties.Resources.botao_4;
            this.btnExemplo4.Location = new System.Drawing.Point(700, 97);
            this.btnExemplo4.Name = "btnExemplo4";
            this.btnExemplo4.Size = new System.Drawing.Size(85, 79);
            this.btnExemplo4.TabIndex = 8;
            this.btnExemplo4.UseVisualStyleBackColor = true;
            this.btnExemplo4.Click += new System.EventHandler(this.btnExemplo4_Click);
            // 
            // lblResultados
            // 
            this.lblResultados.AutoSize = true;
            this.lblResultados.Image = global::Sistemas_Lineares.Properties.Resources.resultados;
            this.lblResultados.Location = new System.Drawing.Point(-3, 276);
            this.lblResultados.MaximumSize = new System.Drawing.Size(150, 75);
            this.lblResultados.MinimumSize = new System.Drawing.Size(150, 30);
            this.lblResultados.Name = "lblResultados";
            this.lblResultados.Size = new System.Drawing.Size(150, 30);
            this.lblResultados.TabIndex = 7;
            // 
            // btnImportar
            // 
            this.btnImportar.FlatAppearance.BorderSize = 0;
            this.btnImportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportar.Image = global::Sistemas_Lineares.Properties.Resources.botao_importar2;
            this.btnImportar.Location = new System.Drawing.Point(605, 230);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(275, 43);
            this.btnImportar.TabIndex = 5;
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnCalcular
            // 
            this.btnCalcular.FlatAppearance.BorderSize = 0;
            this.btnCalcular.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalcular.Image = global::Sistemas_Lineares.Properties.Resources.botao_calcular1;
            this.btnCalcular.Location = new System.Drawing.Point(605, 182);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(275, 43);
            this.btnCalcular.TabIndex = 4;
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // btnExemplo3
            // 
            this.btnExemplo3.FlatAppearance.BorderSize = 0;
            this.btnExemplo3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExemplo3.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExemplo3.Image = global::Sistemas_Lineares.Properties.Resources.botao_3;
            this.btnExemplo3.Location = new System.Drawing.Point(605, 97);
            this.btnExemplo3.Name = "btnExemplo3";
            this.btnExemplo3.Size = new System.Drawing.Size(85, 79);
            this.btnExemplo3.TabIndex = 3;
            this.btnExemplo3.UseVisualStyleBackColor = true;
            this.btnExemplo3.Click += new System.EventHandler(this.btnExemplo3_Click);
            // 
            // lblVariaveis
            // 
            this.lblVariaveis.AutoSize = true;
            this.lblVariaveis.Image = global::Sistemas_Lineares.Properties.Resources.variaveis1;
            this.lblVariaveis.Location = new System.Drawing.Point(607, 14);
            this.lblVariaveis.MaximumSize = new System.Drawing.Size(500, 500);
            this.lblVariaveis.MinimumSize = new System.Drawing.Size(150, 75);
            this.lblVariaveis.Name = "lblVariaveis";
            this.lblVariaveis.Size = new System.Drawing.Size(150, 75);
            this.lblVariaveis.TabIndex = 1;
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 584);
            this.Controls.Add(this.btnTelaInicial);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.btnExemplo20);
            this.Controls.Add(this.btnExemplo4);
            this.Controls.Add(this.lblResultados);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.btnExemplo3);
            this.Controls.Add(this.nudQtdVariaveis);
            this.Controls.Add(this.lblVariaveis);
            this.Controls.Add(this.gridSistemaLinear);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FrmPrincipal";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculadora de Sistemas Lineares - Método Gauss";
            ((System.ComponentModel.ISupportInitialize)(this.gridSistemaLinear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQtdVariaveis)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridSistemaLinear;
        private System.Windows.Forms.Label lblVariaveis;
        private System.Windows.Forms.NumericUpDown nudQtdVariaveis;
        private System.Windows.Forms.Button btnExemplo3;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Label lblResultados;
        private System.Windows.Forms.Button btnExemplo4;
        private System.Windows.Forms.Button btnExemplo20;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.OpenFileDialog ofd_Valores;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnTelaInicial;
    }
}