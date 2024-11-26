using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistemas_Lineares
{
    public partial class FrmSobre : Form
    {
        #region Integrantes do Grupo
        /* Integrantes do Grupo
         * 
         * Nome: Guilherme Feruglio Nishiyama      RA: 081210018
         * Nome: Lucas Araújo dos Santos           RA: 081210009
         * Nome: Victor Nunes da Silva             RA: 081210012
         */
        #endregion

        /// <summary>
        /// Inicializador do Forms.
        /// </summary>
        #region Inicializador
        public FrmSobre()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// Evento click do botão Tela Inicial
        /// </summary>
        #region Botão Tela Inicial
        private void btnTelaInicial_Click(object sender, EventArgs e)
        {
            // Instância do forms FrmInicial
            FrmInicial inicial = new FrmInicial();

            // Oculta esse forms
            this.Hide();

            // Abre o form FrmInicial
            inicial.ShowDialog();

            // Fecha esse forms
            this.Close();
        }
        #endregion
    }
}