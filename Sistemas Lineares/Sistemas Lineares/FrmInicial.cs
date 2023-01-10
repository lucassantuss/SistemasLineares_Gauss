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
    public partial class FrmInicial : Form
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
        public FrmInicial()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// Evento click do botão Iniciar
        /// </summary>
        #region Botão Iniciar
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            // Instância do forms FrmPrincipal
            FrmPrincipal principal = new FrmPrincipal();

            // Oculta esse forms
            this.Hide();

            // Abre o form FrmPrincipal
            principal.ShowDialog();

            // Fecha esse forms
            this.Close();
        }
        #endregion

        /// <summary>
        /// Evento click do botão Sobre
        /// </summary>
        #region Botão Sobre
        private void btnSobre_Click(object sender, EventArgs e)
        {
            // Instância do forms FrmSobre
            FrmSobre sobre = new FrmSobre();

            // Oculta esse forms
            this.Hide();

            // Abre o form FrmSobre
            sobre.ShowDialog();

            // Fecha esse forms
            this.Close();
        }
        #endregion
    }
}