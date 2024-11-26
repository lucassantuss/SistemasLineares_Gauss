using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Sistemas_Lineares
{
    public partial class FrmPrincipal : Form
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
        public FrmPrincipal()
        {
            InitializeComponent();

            // Adiciona 3 linhas e 4 colunas no gridSistemaLinear
            AddLinhaColunaGrid(3);
            // Altera o valor do NumericUpDown para 3
            nudQtdVariaveis.Value = 3;

            // Impede o usuário de adicionar linhas no gridSistemaLinear
            gridSistemaLinear.AllowUserToAddRows = false;

            // Impede o usuário de deletar linhas no gridSistemaLinear
            gridSistemaLinear.AllowUserToDeleteRows = false;
        }
        #endregion

        /// <summary>
        /// StringBuilder para concatenar strings que serão exibidas em txtResultados.
        /// </summary>
        #region Variável StringBuilder
        StringBuilder sb = new StringBuilder();
        #endregion

        /// <summary>
        /// Variável para verificar se usou ou não o arquivo texto para os cálculos
        /// se usou o arquivo texto (clicou no botão "importar e calcular", não será limpo o txtResultado (TextBox) a cada cálculo)
        /// se não usou o arquivo texto, será limpo o txtResultado (TextBox) a cada cálculo).
        /// </summary>
        #region Variável boolean
        public bool UsouArquivoTxt = false;
        #endregion

        /// <summary>
        /// Este método adiciona colunas e linhas no gridSistemaLinear, de acordo com a quantidade de variáveis escolhida pelo usuário.
        /// </summary>
        /// <param name="QtdeVariaveis">Quantidade de variáveis escolhida pelo usuário.</param>
        #region Adiciona Linha e Coluna no gridSistemaLinear
        private void AddLinhaColunaGrid(int QtdeVariaveis)
        {
            // Limpa as linhas e colunas do gridSistemaLinear
            gridSistemaLinear.Rows.Clear();
            gridSistemaLinear.Columns.Clear();

            // Adiciona as linhas e colunas, de acordo com a quantidade de variáveis escolhida pelo usuário
            for (int i = 0; i < QtdeVariaveis; i++)
            {
                // Adiciona coluna Xn no gridSistemaLinear
                gridSistemaLinear.Columns.Add($"colX{i+1}", $"X{i+1}");
                // Altera o valor da largura da coluna Xn para 70
                gridSistemaLinear.Columns[$"colX{i+1}"].Width = 70;
                // Adiciona linha no gridSistemaLinear
                gridSistemaLinear.Rows.Add();

                if (i == QtdeVariaveis - 1)
                {
                    // Adiciona coluna R no gridSistemaLinear
                    gridSistemaLinear.Columns.Add("colR", "R");
                    // Altera o valor da largura da coluna R para 80
                    gridSistemaLinear.Columns["colR"].Width = 80;
                }
            }
        }
        #endregion

        /// <summary>
        /// Este método exibe as colunas Xn e R no txtResultado, de acordo com a quantidade de variáveis escolhida pelo usuário.
        /// </summary>
        /// <param name="QtdeVariaveis">Quantidade de variáveis escolhida pelo usuário.</param>
        #region Exibe as colunas no txtResultado
        private void ExibirColunasNoTxt(int QtdeVariaveis)
        {
            // Exibe as colunas Xn e R no txtResultado, de acordo com a quantidade de variáveis escolhida pelo usuário.
            for (int i = 0; i < QtdeVariaveis; i++)
            {
                // Exibe a coluna Xn, com uso do alinhamento PadLeft(12)
                // txtResultado.Text += $"X{i + 1}".PadLeft(12);
                sb.Append($"X{i + 1}".PadLeft(12));

                if (i == QtdeVariaveis - 1)
                {
                    // Exibe a coluna R, com uso do alinhamento PadLeft(12)
                    // txtResultado.Text += "R".PadLeft(12) + Environment.NewLine;
                    sb.Append("R".PadLeft(12) + Environment.NewLine);
                }
            }
        }
        #endregion

        /// <summary>
        /// Evento click do botão Calcular
        /// </summary>
        #region Botão Calcular
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            // Verificar se foi utilizado o arquivo texto ou não, para limpar ou não o txtResultado após cada cálculo feito.
            if (UsouArquivoTxt == false)
            {
                // Limpa o txtResultado
                // txtResultado.Text = "";
                sb.Clear();
            }

            int QtdeVariaveis = int.Parse(nudQtdVariaveis.Value.ToString());       // Quantidade de variáveis escolhidas pelo usuário
            int QtdeColunasGrid = QtdeVariaveis + 1;                               // Quantidade de colunas no gridSistemaLinear
                                                                                   
            double[] soma = new double[QtdeVariaveis];                             // Vetor em que ficará armazenado a soma dos resultados obtidos na prova
            double[,] sistema = new double[QtdeVariaveis, QtdeColunasGrid];        // Matriz do sistema linear
            double[,] prova = new double[QtdeVariaveis, QtdeColunasGrid];          // Vetor em que ficará armazenado os resultados obtidos na prova 
            double[] solucao = new double[QtdeVariaveis];                          // Vetor em que ficará armazenado as raizes obtidas durante o cálculo
            double[] coef = new double[QtdeVariaveis];                             // Vetor em que ficará armazenado os coeficientes de triangularização
                                                                                   
            try
            {
                // Exibe a Matriz Aumentada do Sistema Linear no txtResultado
                ExibeMatrizAumentada(QtdeVariaveis, QtdeColunasGrid, sistema);

                // Verificar se o pivô do Sistema Linear é 0
                if (sistema[0, 0] == 0)
                {
                    // Mensagem de aviso, informando que o pivô não pode ser 0
                    // txtResultado.Text += "O pivô não pode ser 0";
                    // txtResultado.Text += Environment.NewLine + "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -" + Environment.NewLine;
                    sb.Append(Environment.NewLine + "O pivô não pode ser 0");
                    sb.Append(Environment.NewLine + "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -" + Environment.NewLine);
                }
                else
                {
                    // Calcula o Sistema Linear
                    CalculaSistemaLinear(QtdeVariaveis, QtdeColunasGrid, sistema, coef);

                    // Calcula as raizes do Sistema Linear (X1, X2, Xn)
                    CalculaRaizes(QtdeVariaveis, sistema, solucao);

                    // Exibe as raizes do Sistema Linear (X1, X2, Xn) no txtResultado
                    ExibeRaizes(QtdeVariaveis, solucao);

                    // Calcula a prova do Sistema Linear, para verificar se os cálculos realizados anteriormente estão corretos
                    CalculaProva(QtdeVariaveis, QtdeColunasGrid, sistema, prova, soma, solucao);

                    // Exibe a prova do Sistema Linear no txtResultado
                    ExibeProva(QtdeVariaveis, QtdeColunasGrid, sistema, prova);
                }
            }
            catch
            {
                // Mensagem de aviso, informando que faltou o preenchimento de algum campo do sistema linear
                // txtResultado.Text = "Faltou o preenchimento de algum campo do sistema linear" + Environment.NewLine;
                // txtResultado.Text += Environment.NewLine + "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -" + Environment.NewLine;
                sb.Append(Environment.NewLine + Environment.NewLine + "Faltou o preenchimento de algum campo do sistema linear" + Environment.NewLine);
                sb.Append(Environment.NewLine + "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -" + Environment.NewLine);
            }

            // Exibir no txtResultado todo o valor que foi armazenado no StringBuilder durante o programa.
            txtResultado.Text = sb.ToString();
        }
        #endregion

        /// <summary>
        /// Exibe a Matriz Aumentada do Sistema Linear no txtResultado.
        /// </summary>
        /// <param name="QtdeVariaveis">Quantidade de variáveis escolhida pelo usuário.</param>
        /// <param name="QtdeColunasGrid">Quantidade de colunas no gridSistemaLinear.</param>
        /// <param name="sistema">Vetor contendo os valores em cada posição do sistema.</param>
        #region Exibe a Matriz Aumentada
        private void ExibeMatrizAumentada(int QtdeVariaveis, int QtdeColunasGrid, double[,] sistema)
        {
            // Exibe no txtResultado, uma mensagem indicando que abaixo será mostrada a Matriz Aumentada do Sistema Linear
            // txtResultado.Text += "Matriz aumentada" + Environment.NewLine + Environment.NewLine;
            sb.Append("Matriz aumentada" + Environment.NewLine + Environment.NewLine);

            // Exibe as colunas Xn e R no txtResultado, de acordo com a quantidade de variáveis escolhida pelo usuário.
            ExibirColunasNoTxt(QtdeVariaveis);

            // Dois for's, o mais externo lê linhas, o interno colunas.
            for (int i = 0; i < QtdeVariaveis; i++)
            {
                for (int j = 0; j < QtdeColunasGrid; j++)
                {
                    if (j == QtdeColunasGrid - 1)
                    {
                        // Preenche o vetor sistema[i, j] com o valor correspondente a posição dele no gridSistemaLinear
                        sistema[i, j] = double.Parse(gridSistemaLinear.Rows[i].Cells[$"colR"].Value.ToString());
                    }
                    else
                    {
                        // Preenche o vetor sistema[i, j] com o valor correspondente a posição dele no gridSistemaLinear
                        sistema[i, j] = double.Parse(gridSistemaLinear.Rows[i].Cells[$"colX{j + 1}"].Value.ToString());
                    }

                    // Exibe os valores do Sistema Linear no txtResultado, com uso do alinhamento PadLeft(12)
                    // txtResultado.Text += $"{sistema[i, j]:f3}".PadLeft(12);
                    sb.Append($"{sistema[i, j]:f3}".PadLeft(12));
                }

                // Pula linha no txtResultado
                // txtResultado.Text += Environment.NewLine;
                sb.Append(Environment.NewLine);
            }
        }
        #endregion

        /// <summary>
        /// Calcula o Sistema Linear, através do Método de Gauss, com a utilização de etapas de triangularização.
        /// </summary>
        /// <param name="QtdeVariaveis">Quantidade de variáveis escolhida pelo usuário.</param>
        /// <param name="QtdeColunasGrid">Quantidade de colunas no gridSistemaLinear.</param>
        /// <param name="sistema">Vetor contendo os valores em cada posição do sistema.</param>
        /// <param name="coef">Vetor em que ficará armazenado os coeficientes de triangularização</param>
        #region Calcula o Sistema Linear
        private void CalculaSistemaLinear(int QtdeVariaveis, int QtdeColunasGrid, double[,] sistema, double[] coef)
        {
            // Variáveis utilizadas
            int k, i, j;
            int cont = 0;

            do
            {
                // Etapa de triangularização:
                // Calcula os coeficientes de triangularização respectivos aos elementos da coluna em questão do sistema.

                for (k = cont; k < QtdeVariaveis - 1; k++)
                {
                    coef[k] = -1.0 * sistema[k + 1, cont] / sistema[cont, cont];

                    for (j = 0; j < QtdeColunasGrid; j++)
                    {
                        sistema[k + 1, j] += coef[k] * sistema[cont, j];
                    }
                }

                // Impressão da matriz equivalente para Xn:
                // Percorre a matriz do sistema e a imprime.

                // Exibe no txtResultado, uma mensagem indicando que abaixo será mostrada a Matriz equivalente para Xn
                /* txtResultado.Text += Environment.NewLine + $"Matriz equivalente para X{cont + 1}" +
                                     Environment.NewLine + Environment.NewLine; */
                sb.Append(Environment.NewLine + $"Matriz equivalente para X{cont + 1}" +
                          Environment.NewLine + Environment.NewLine);

                // Exibe as colunas Xn e R no txtResultado, de acordo com a quantidade de variáveis escolhida pelo usuário.
                ExibirColunasNoTxt(QtdeVariaveis);

                // Dois for's, o mais externo lê linhas, o interno colunas.
                for (i = 0; i < QtdeVariaveis; i++)
                {
                    for (j = 0; j < QtdeColunasGrid; j++)
                    {
                        // Exibe os valores calculados do Sistema Linear no txtResultado, com uso do alinhamento PadLeft(12)
                        // txtResultado.Text += $"{sistema[i, j]:f3}".PadLeft(12);
                        sb.Append($"{sistema[i, j]:f3}".PadLeft(12));
                    }

                    // Pula linha no txtResultado
                    // txtResultado.Text += Environment.NewLine;
                    sb.Append(Environment.NewLine);
                }

                // Acrescenta +1 no contador;
                cont++;
            } while (cont < QtdeVariaveis - 1);

            // Rascunho

            // Segunda etapa de triangularização:
            // Calcula o coeficiente de triangularização respectivo aos elementos da segunda coluna do sistema.

            // coef_21 = -1.0 * sistema[2, 1] / sistema[1, 1];

            /*
            coef[2] = -1.0 * sistema[2, 1] / sistema[1, 1];

            for (j = 1; j < QtdeColunasGrid; j++)
            {
                sistema[2, j] += coef[2] * sistema[1, j];
            }
            */
        }
        #endregion

        /// <summary>
        /// Calcula as raízes do Sistema Linear (X1, X2, X3, Xn).
        /// </summary>
        /// <param name="QtdeVariaveis">Quantidade de variáveis escolhida pelo usuário.</param>
        /// <param name="sistema">Vetor contendo os valores em cada posição do sistema.</param>
        /// <param name="solucao">Vetor contendo os valores de X1, X2, X3, Xn.</param>
        #region Calcula as raízes
        private void CalculaRaizes(int QtdeVariaveis, double[,] sistema, double[] solucao)
        {
            // Calculando as raízes
            for (int n = QtdeVariaveis - 1; n >= 0; n--)
            {
                solucao[n] = sistema[n, QtdeVariaveis];

                for (int k = QtdeVariaveis - 1; k > n; k--)
                {
                    solucao[n] -= (solucao[k] * sistema[n, k]);
                }

                solucao[n] /= sistema[n, n];
            }

            // Rascunho

            // Para o cálculo da parte de cima do código, foi observado esse padrão de cálculos representado nesse rascunho abaixo.

            /*
            switch (QtdeVariaveis)
            {
                case 3:
                    solucao[2] = (sistema[2, 3]) / sistema[2, 2];
                    solucao[1] = (sistema[1, 3] - (solucao[2] * sistema[1, 2])) / sistema[1, 1];
                    solucao[0] = (sistema[0, 3] - (solucao[2] * sistema[0, 2]) - (solucao[1] * sistema[0, 1])) / sistema[0, 0];
                    break;
                case 4:
                    solucao[3] = (sistema[3, 4]) / sistema[3, 3];
                    solucao[2] = (sistema[2, 4] - (solucao[3] * sistema[2, 3])) / sistema[2, 2];
                    solucao[1] = (sistema[1, 4] - (solucao[3] * sistema[1, 3]) - (solucao[2] * sistema[1, 2])) / sistema[1, 1];
                    solucao[0] = (sistema[0, 4] - (solucao[3] * sistema[0, 3]) - (solucao[2] * sistema[0, 2]) - solucao[1] * sistema[0, 1]) / sistema[0, 0];
                    break;
                case 5:
                    solucao[4] = (sistema[4, 5]) / sistema[4, 4];
                    solucao[3] = (sistema[3, 5] - (solucao[4] * sistema[3, 4])) / sistema[3, 3];
                    solucao[2] = (sistema[2, 5] - (solucao[4] * sistema[2, 4]) - (solucao[3] * sistema[2, 3])) / sistema[2, 2];
                    solucao[1] = (sistema[1, 5] - (solucao[4] * sistema[1, 4]) - (solucao[3] * sistema[1, 3]) - (solucao[2] * sistema[1, 2])) / sistema[1, 1];
                    solucao[0] = (sistema[0, 5] - (solucao[4] * sistema[0, 4]) - (solucao[3] * sistema[0, 3]) - (solucao[2] * sistema[0, 2]) - (solucao[1] * sistema[0, 1])) / sistema[0, 0];
                    break;
            }
            */
        }
        #endregion

        /// <summary>
        /// Este método exibe as raízes obtidas, X1, X2, X3, Xn.
        /// </summary>
        /// <param name="QtdeVariaveis">Quantidade de variáveis escolhida pelo usuário.</param>
        /// <param name="solucao">Vetor contendo os valores de X1, X2, X3, Xn.</param>
        #region Exibe as raízes
        private void ExibeRaizes(int QtdeVariaveis, double[] solucao)
        {
            // Exibe no txtResultado, uma mensagem indicando que abaixo serão mostradas as raízes do Sistema Linear
            // txtResultado.Text += Environment.NewLine + $"*** RAÍZES ***" + Environment.NewLine + Environment.NewLine;
            sb.Append(Environment.NewLine + $"*** RAÍZES ***" + Environment.NewLine + Environment.NewLine);

            for (int i = 0; i < QtdeVariaveis; i++)
            {
                // Exibe no txtResultado, as raízes obtidas, X1, X2, X3, Xn.
                // txtResultado.Text += $"X{i + 1} = {Math.Round(solucao[i], 3)}" + Environment.NewLine;
                sb.Append($"X{i + 1} = {Math.Round(solucao[i], 3)}" + Environment.NewLine);
            }
        }
        #endregion

        /// <summary>
        /// Este método calcula a prova final, multiplicando o valor com o seu respectivo Xn,
        /// e no final realizando a soma de todos os valores da linha.
        /// </summary>
        /// <param name="QtdeVariaveis">Quantidade de variáveis escolhida pelo usuário.</param>
        /// <param name="QtdeColunasGrid">Quantidade de colunas no gridSistemaLinear.</param>
        /// <param name="sistema">Vetor contendo os valores em cada posição do sistema.</param>
        /// <param name="prova">Vetor que será armazenado o resultado obtido depois de calcular a prova.</param>
        /// <param name="soma">Vetor para armazenar os valores somados de cada linha do sistema linear.</param>
        /// <param name="solucao">Vetor contendo os valores de X1, X2, X3, Xn.</param>
        #region Calcula a Prova
        private void CalculaProva(int QtdeVariaveis, int QtdeColunasGrid, double[,] sistema, double[,] prova, double[] soma, double[] solucao)
        {
            // Calculando a prova

            // Multiplicando os valores do Sistema Linear com o seu respectivo Xn,
            // e no final realizando a soma de todos os valores da linha.
            for (int i = 0; i < QtdeVariaveis; i++)
            {
                for (int j = 0; j < QtdeColunasGrid; j++)
                {
                    if (j == QtdeColunasGrid - 1)
                    {
                        sistema[i, j] = double.Parse(gridSistemaLinear.Rows[i].Cells[$"colR"].Value.ToString());
                        prova[i, j] = Math.Round(soma[i], 3);
                    }
                    else
                    {
                        sistema[i, j] = double.Parse(gridSistemaLinear.Rows[i].Cells[$"colX{j + 1}"].Value.ToString());
                        prova[i, j] = sistema[i, j] * solucao[j];
                        soma[i] += sistema[i, j] * solucao[j];
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Este método exibe a prova final, mostrando se os valores estão OK ou NOK
        /// </summary>
        /// <param name="QtdeVariaveis">Quantidade de variáveis escolhida pelo usuário.</param>
        /// <param name="QtdeColunasGrid">Quantidade de colunas no gridSistemaLinear.</param>
        /// <param name="sistema">Vetor contendo os valores em cada posição do sistema.</param>
        /// <param name="prova">Vetor contendo os resultados obtidos no cálculo da prova.</param>
        #region Exibe a Prova
        private void ExibeProva(int QtdeVariaveis, int QtdeColunasGrid, double[,] sistema, double[,] prova)
        {
            // Impressão da PROVA:
            // Percorre a matriz do sistema e a imprime.

            // Exibe no txtResultado, uma mensagem indicando que abaixo será mostrada a prova do Sistema Linear
            /* txtResultado.Text += Environment.NewLine + "*** PROVA ***" +
                                 Environment.NewLine + Environment.NewLine; */
            sb.Append(Environment.NewLine + "*** PROVA ***" +
                     Environment.NewLine + Environment.NewLine);

            for (int i = 0; i < QtdeVariaveis; i++)
            {
                // Exibe a coluna Xn, com uso do alinhamento PadLeft(12) no txtResultado
                // txtResultado.Text += $"X{i + 1}".PadLeft(12);
                sb.Append($"X{i + 1}".PadLeft(12));

                if (i == QtdeVariaveis - 1)
                {
                    // Exibe as colunas SOMA e RESULTADO, com uso do alinhamento PadLeft(12) no txtResultado
                    /* txtResultado.Text += "SOMA".PadLeft(12) +
                                         "RESULTADO".PadLeft(12) + Environment.NewLine; */
                    sb.Append("SOMA".PadLeft(12) +
                     "RESULTADO".PadLeft(12) + Environment.NewLine);
                }
            }

            // Dois for's, o mais externo lê linhas, o interno colunas.
            for (int i = 0; i < QtdeVariaveis; i++)
            {
                for (int j = 0; j < QtdeColunasGrid; j++)
                {
                    // Exibe os valores da prova do Sistema Linear no txtResultado, com uso do alinhamento PadLeft(12)
                    // txtResultado.Text += $"{prova[i, j]:f3}".PadLeft(12);
                    sb.Append($"{prova[i, j]:f3}".PadLeft(12));

                    if (j == QtdeColunasGrid - 1)
                    {
                        // Verifica se o R (soma) da prova realizada é igual ao R (soma) do sistema linear inicial
                        if (prova[i, j] == double.Parse($"{sistema[i, j]:f3}"))
                        {
                            // Exibe OK no txtResultado, com uso do alinhamento PadLeft(12)
                            // txtResultado.Text += "OK".PadLeft(12);
                            sb.Append("OK".PadLeft(12));
                        }
                        else
                        {
                            // Exibe NOK no txtResultado, com uso do alinhamento PadLeft(12)
                            // txtResultado.Text += "NOK".PadLeft(12);
                            sb.Append("NOK".PadLeft(12));
                        }
                    }
                }

                // Pula linha no txtResultado
                // txtResultado.Text += Environment.NewLine;
                sb.Append(Environment.NewLine);
            }

            // Barras de separação entre esse Sistema Linear calculado e o próximo sistema linear que será calculado futuramente
            // txtResultado.Text += Environment.NewLine + "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -" + Environment.NewLine;
            sb.Append(Environment.NewLine + "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -" + Environment.NewLine);
        }
        #endregion

        /// <summary>
        /// Evento do nudQtdVariaveis (NumericUpDown) para quando for alterado o valor deste componente.
        /// </summary>
        #region Caso alterar algum valor no NumericUpDown
        private void nudQtdVariaveis_ValueChanged(object sender, EventArgs e)
        {
            // Adiciona linhas e colunas no gridSistemaLinear, de acordo com a quantidade de variáveis escolhida pelo usuário
            AddLinhaColunaGrid(int.Parse(nudQtdVariaveis.Value.ToString()));
        }
        #endregion

        /// <summary>
        /// Evento click do botão Exemplo com 3 variáveis.
        /// </summary>
        #region Botão Exemplo 3 variáveis
        private void btnExemplo3_Click(object sender, EventArgs e)
        {
            // Adiciona 3 linhas e 4 colunas no gridSistemaLinear
            AddLinhaColunaGrid(3);
            // Altera o valor do NumericUpDown para 3
            nudQtdVariaveis.Value = 3;

            // Preenche gridSistemaLinear - Exemplo com 3 variáveis

            #region 1° Linha do Sistema Linear
            gridSistemaLinear.Rows[0].Cells[$"colX1"].Value = 2;
            gridSistemaLinear.Rows[0].Cells[$"colX2"].Value = 3;
            gridSistemaLinear.Rows[0].Cells[$"colX3"].Value = -1;
            gridSistemaLinear.Rows[0].Cells[$"colR"].Value = 5;
            #endregion

            #region 2° Linha do Sistema Linear
            gridSistemaLinear.Rows[1].Cells[$"colX1"].Value = 4;
            gridSistemaLinear.Rows[1].Cells[$"colX2"].Value = 4;
            gridSistemaLinear.Rows[1].Cells[$"colX3"].Value = -3;
            gridSistemaLinear.Rows[1].Cells[$"colR"].Value = 3;
            #endregion

            #region 3° Linha do Sistema Linear
            gridSistemaLinear.Rows[2].Cells[$"colX1"].Value = 2;
            gridSistemaLinear.Rows[2].Cells[$"colX2"].Value = -3;
            gridSistemaLinear.Rows[2].Cells[$"colX3"].Value = 1;
            gridSistemaLinear.Rows[2].Cells[$"colR"].Value = -1;
            #endregion
        }
        #endregion

        /// <summary>
        /// Evento click do botão Exemplo com 4 variáveis.
        /// </summary>
        #region Botão Exemplo 4 variáveis
        private void btnExemplo4_Click(object sender, EventArgs e)
        {
            // Adiciona 4 linhas e 5 colunas no gridSistemaLinear
            AddLinhaColunaGrid(4);
            // Altera o valor do NumericUpDown para 4
            nudQtdVariaveis.Value = 4;

            // Preenche gridSistemaLinear - Exemplo com 4 variáveis

            #region 1° Linha do Sistema Linear
            gridSistemaLinear.Rows[0].Cells[$"colX1"].Value = 3;
            gridSistemaLinear.Rows[0].Cells[$"colX2"].Value = 5;
            gridSistemaLinear.Rows[0].Cells[$"colX3"].Value = 2;
            gridSistemaLinear.Rows[0].Cells[$"colX4"].Value = 4;
            gridSistemaLinear.Rows[0].Cells[$"colR"].Value = -1.1;
            #endregion

            #region 2° Linha do Sistema Linear
            gridSistemaLinear.Rows[1].Cells[$"colX1"].Value = -1;
            gridSistemaLinear.Rows[1].Cells[$"colX2"].Value = 6;
            gridSistemaLinear.Rows[1].Cells[$"colX3"].Value = 3;
            gridSistemaLinear.Rows[1].Cells[$"colX4"].Value = 5;
            gridSistemaLinear.Rows[1].Cells[$"colR"].Value = -6.3;
            #endregion

            #region 3° Linha do Sistema Linear
            gridSistemaLinear.Rows[2].Cells[$"colX1"].Value = 4;
            gridSistemaLinear.Rows[2].Cells[$"colX2"].Value = 2;
            gridSistemaLinear.Rows[2].Cells[$"colX3"].Value = 1;
            gridSistemaLinear.Rows[2].Cells[$"colX4"].Value = 2;
            gridSistemaLinear.Rows[2].Cells[$"colR"].Value = 1.9;
            #endregion

            #region 4° Linha do Sistema Linear
            gridSistemaLinear.Rows[3].Cells[$"colX1"].Value = -5;
            gridSistemaLinear.Rows[3].Cells[$"colX2"].Value = 7;
            gridSistemaLinear.Rows[3].Cells[$"colX3"].Value = 4;
            gridSistemaLinear.Rows[3].Cells[$"colX4"].Value = 3;
            gridSistemaLinear.Rows[3].Cells[$"colR"].Value = -4.6;
            #endregion
        }
        #endregion

        /// <summary>
        /// Evento click do botão Exemplo com 20 variáveis.
        /// </summary>
        #region Botão Exemplo 20 variáveis
        private void btnExemplo20_Click(object sender, EventArgs e)
        {
            // Adiciona 20 linhas e 21 colunas no gridSistemaLinear
            AddLinhaColunaGrid(20);
            // Altera o valor do NumericUpDown para 20
            nudQtdVariaveis.Value = 20;

            // Preenche gridSistemaLinear - Exemplo com 20 variáveis

            #region 1° Linha do Sistema Linear
            gridSistemaLinear.Rows[0].Cells[$"colX1"].Value = 1;
            gridSistemaLinear.Rows[0].Cells[$"colX2"].Value = 3;
            gridSistemaLinear.Rows[0].Cells[$"colX3"].Value = 8;
            gridSistemaLinear.Rows[0].Cells[$"colX4"].Value = 1;
            gridSistemaLinear.Rows[0].Cells[$"colX5"].Value = -4;
            gridSistemaLinear.Rows[0].Cells[$"colX6"].Value = 2;
            gridSistemaLinear.Rows[0].Cells[$"colX7"].Value = 10;
            gridSistemaLinear.Rows[0].Cells[$"colX8"].Value = 9;
            gridSistemaLinear.Rows[0].Cells[$"colX9"].Value = 6;
            gridSistemaLinear.Rows[0].Cells[$"colX10"].Value = 6;
            gridSistemaLinear.Rows[0].Cells[$"colX11"].Value = 9;
            gridSistemaLinear.Rows[0].Cells[$"colX12"].Value = 1;
            gridSistemaLinear.Rows[0].Cells[$"colX13"].Value = 10;
            gridSistemaLinear.Rows[0].Cells[$"colX14"].Value = -4;
            gridSistemaLinear.Rows[0].Cells[$"colX15"].Value = 9;
            gridSistemaLinear.Rows[0].Cells[$"colX16"].Value = 7;
            gridSistemaLinear.Rows[0].Cells[$"colX17"].Value = 11;
            gridSistemaLinear.Rows[0].Cells[$"colX18"].Value = -3;
            gridSistemaLinear.Rows[0].Cells[$"colX19"].Value = 7;
            gridSistemaLinear.Rows[0].Cells[$"colX20"].Value = 2;
            gridSistemaLinear.Rows[0].Cells[$"colR"].Value = 313;
            #endregion

            #region 2° Linha do Sistema Linear
            gridSistemaLinear.Rows[1].Cells[$"colX1"].Value = 5;
            gridSistemaLinear.Rows[1].Cells[$"colX2"].Value = 6;
            gridSistemaLinear.Rows[1].Cells[$"colX3"].Value = 1;
            gridSistemaLinear.Rows[1].Cells[$"colX4"].Value = 1;
            gridSistemaLinear.Rows[1].Cells[$"colX5"].Value = 9;
            gridSistemaLinear.Rows[1].Cells[$"colX6"].Value = 5;
            gridSistemaLinear.Rows[1].Cells[$"colX7"].Value = 8;
            gridSistemaLinear.Rows[1].Cells[$"colX8"].Value = 5;
            gridSistemaLinear.Rows[1].Cells[$"colX9"].Value = 2;
            gridSistemaLinear.Rows[1].Cells[$"colX10"].Value = 4;
            gridSistemaLinear.Rows[1].Cells[$"colX11"].Value = 5;
            gridSistemaLinear.Rows[1].Cells[$"colX12"].Value = 6;
            gridSistemaLinear.Rows[1].Cells[$"colX13"].Value = 9;
            gridSistemaLinear.Rows[1].Cells[$"colX14"].Value = 2;
            gridSistemaLinear.Rows[1].Cells[$"colX15"].Value = 7;
            gridSistemaLinear.Rows[1].Cells[$"colX16"].Value = 7;
            gridSistemaLinear.Rows[1].Cells[$"colX17"].Value = 10;
            gridSistemaLinear.Rows[1].Cells[$"colX18"].Value = 5;
            gridSistemaLinear.Rows[1].Cells[$"colX19"].Value = 2;
            gridSistemaLinear.Rows[1].Cells[$"colX20"].Value = 11;
            gridSistemaLinear.Rows[1].Cells[$"colR"].Value = 541;
            #endregion

            #region 3° Linha do Sistema Linear
            gridSistemaLinear.Rows[2].Cells[$"colX1"].Value = 6;
            gridSistemaLinear.Rows[2].Cells[$"colX2"].Value = 1;
            gridSistemaLinear.Rows[2].Cells[$"colX3"].Value = 6;
            gridSistemaLinear.Rows[2].Cells[$"colX4"].Value = 3;
            gridSistemaLinear.Rows[2].Cells[$"colX5"].Value = -4;
            gridSistemaLinear.Rows[2].Cells[$"colX6"].Value = 9;
            gridSistemaLinear.Rows[2].Cells[$"colX7"].Value = 8;
            gridSistemaLinear.Rows[2].Cells[$"colX8"].Value = 6;
            gridSistemaLinear.Rows[2].Cells[$"colX9"].Value = 6;
            gridSistemaLinear.Rows[2].Cells[$"colX10"].Value = 3;
            gridSistemaLinear.Rows[2].Cells[$"colX11"].Value = 10;
            gridSistemaLinear.Rows[2].Cells[$"colX12"].Value = 3;
            gridSistemaLinear.Rows[2].Cells[$"colX13"].Value = 5;
            gridSistemaLinear.Rows[2].Cells[$"colX14"].Value = -5;
            gridSistemaLinear.Rows[2].Cells[$"colX15"].Value = 4;
            gridSistemaLinear.Rows[2].Cells[$"colX16"].Value = 5;
            gridSistemaLinear.Rows[2].Cells[$"colX17"].Value = 10;
            gridSistemaLinear.Rows[2].Cells[$"colX18"].Value = -4;
            gridSistemaLinear.Rows[2].Cells[$"colX19"].Value = 10;
            gridSistemaLinear.Rows[2].Cells[$"colX20"].Value = 5;
            gridSistemaLinear.Rows[2].Cells[$"colR"].Value = 385;
            #endregion

            #region 4° Linha do Sistema Linear
            gridSistemaLinear.Rows[3].Cells[$"colX1"].Value = 9;
            gridSistemaLinear.Rows[3].Cells[$"colX2"].Value = 11;
            gridSistemaLinear.Rows[3].Cells[$"colX3"].Value = 9;
            gridSistemaLinear.Rows[3].Cells[$"colX4"].Value = 5;
            gridSistemaLinear.Rows[3].Cells[$"colX5"].Value = 6;
            gridSistemaLinear.Rows[3].Cells[$"colX6"].Value = 6;
            gridSistemaLinear.Rows[3].Cells[$"colX7"].Value = 3;
            gridSistemaLinear.Rows[3].Cells[$"colX8"].Value = 3;
            gridSistemaLinear.Rows[3].Cells[$"colX9"].Value = 5;
            gridSistemaLinear.Rows[3].Cells[$"colX10"].Value = 2;
            gridSistemaLinear.Rows[3].Cells[$"colX11"].Value = 9;
            gridSistemaLinear.Rows[3].Cells[$"colX12"].Value = 3;
            gridSistemaLinear.Rows[3].Cells[$"colX13"].Value = 2;
            gridSistemaLinear.Rows[3].Cells[$"colX14"].Value = 9;
            gridSistemaLinear.Rows[3].Cells[$"colX15"].Value = 2;
            gridSistemaLinear.Rows[3].Cells[$"colX16"].Value = 10;
            gridSistemaLinear.Rows[3].Cells[$"colX17"].Value = 9;
            gridSistemaLinear.Rows[3].Cells[$"colX18"].Value = 3;
            gridSistemaLinear.Rows[3].Cells[$"colX19"].Value = 11;
            gridSistemaLinear.Rows[3].Cells[$"colX20"].Value = 6;
            gridSistemaLinear.Rows[3].Cells[$"colR"].Value = 619;
            #endregion

            #region 5° Linha do Sistema Linear
            gridSistemaLinear.Rows[4].Cells[$"colX1"].Value = 7;
            gridSistemaLinear.Rows[4].Cells[$"colX2"].Value = 6;
            gridSistemaLinear.Rows[4].Cells[$"colX3"].Value = 6;
            gridSistemaLinear.Rows[4].Cells[$"colX4"].Value = 1;
            gridSistemaLinear.Rows[4].Cells[$"colX5"].Value = -2;
            gridSistemaLinear.Rows[4].Cells[$"colX6"].Value = 6;
            gridSistemaLinear.Rows[4].Cells[$"colX7"].Value = 4;
            gridSistemaLinear.Rows[4].Cells[$"colX8"].Value = 7;
            gridSistemaLinear.Rows[4].Cells[$"colX9"].Value = 7;
            gridSistemaLinear.Rows[4].Cells[$"colX10"].Value = 7;
            gridSistemaLinear.Rows[4].Cells[$"colX11"].Value = 9;
            gridSistemaLinear.Rows[4].Cells[$"colX12"].Value = 5;
            gridSistemaLinear.Rows[4].Cells[$"colX13"].Value = 10;
            gridSistemaLinear.Rows[4].Cells[$"colX14"].Value = 0;
            gridSistemaLinear.Rows[4].Cells[$"colX15"].Value = 4;
            gridSistemaLinear.Rows[4].Cells[$"colX16"].Value = 2;
            gridSistemaLinear.Rows[4].Cells[$"colX17"].Value = 3;
            gridSistemaLinear.Rows[4].Cells[$"colX18"].Value = -7;
            gridSistemaLinear.Rows[4].Cells[$"colX19"].Value = 6;
            gridSistemaLinear.Rows[4].Cells[$"colX20"].Value = 4;
            gridSistemaLinear.Rows[4].Cells[$"colR"].Value = 443;
            #endregion

            #region 6° Linha do Sistema Linear
            gridSistemaLinear.Rows[5].Cells[$"colX1"].Value = 10;
            gridSistemaLinear.Rows[5].Cells[$"colX2"].Value = 10;
            gridSistemaLinear.Rows[5].Cells[$"colX3"].Value = 9;
            gridSistemaLinear.Rows[5].Cells[$"colX4"].Value = 5;
            gridSistemaLinear.Rows[5].Cells[$"colX5"].Value = 12;
            gridSistemaLinear.Rows[5].Cells[$"colX6"].Value = 2;
            gridSistemaLinear.Rows[5].Cells[$"colX7"].Value = 8;
            gridSistemaLinear.Rows[5].Cells[$"colX8"].Value = 10;
            gridSistemaLinear.Rows[5].Cells[$"colX9"].Value = 3;
            gridSistemaLinear.Rows[5].Cells[$"colX10"].Value = 3;
            gridSistemaLinear.Rows[5].Cells[$"colX11"].Value = 1;
            gridSistemaLinear.Rows[5].Cells[$"colX12"].Value = 1;
            gridSistemaLinear.Rows[5].Cells[$"colX13"].Value = 6;
            gridSistemaLinear.Rows[5].Cells[$"colX14"].Value = 9;
            gridSistemaLinear.Rows[5].Cells[$"colX15"].Value = 2;
            gridSistemaLinear.Rows[5].Cells[$"colX16"].Value = 6;
            gridSistemaLinear.Rows[5].Cells[$"colX17"].Value = 8;
            gridSistemaLinear.Rows[5].Cells[$"colX18"].Value = 7;
            gridSistemaLinear.Rows[5].Cells[$"colX19"].Value = 10;
            gridSistemaLinear.Rows[5].Cells[$"colX20"].Value = 3;
            gridSistemaLinear.Rows[5].Cells[$"colR"].Value = 741;
            #endregion

            #region 7° Linha do Sistema Linear
            gridSistemaLinear.Rows[6].Cells[$"colX1"].Value = 4;
            gridSistemaLinear.Rows[6].Cells[$"colX2"].Value = 8;
            gridSistemaLinear.Rows[6].Cells[$"colX3"].Value = 6;
            gridSistemaLinear.Rows[6].Cells[$"colX4"].Value = 10;
            gridSistemaLinear.Rows[6].Cells[$"colX5"].Value = -7;
            gridSistemaLinear.Rows[6].Cells[$"colX6"].Value = 6;
            gridSistemaLinear.Rows[6].Cells[$"colX7"].Value = 4;
            gridSistemaLinear.Rows[6].Cells[$"colX8"].Value = 5;
            gridSistemaLinear.Rows[6].Cells[$"colX9"].Value = 6;
            gridSistemaLinear.Rows[6].Cells[$"colX10"].Value = 9;
            gridSistemaLinear.Rows[6].Cells[$"colX11"].Value = 10;
            gridSistemaLinear.Rows[6].Cells[$"colX12"].Value = 8;
            gridSistemaLinear.Rows[6].Cells[$"colX13"].Value = 11;
            gridSistemaLinear.Rows[6].Cells[$"colX14"].Value = 0;
            gridSistemaLinear.Rows[6].Cells[$"colX15"].Value = 10;
            gridSistemaLinear.Rows[6].Cells[$"colX16"].Value = 9;
            gridSistemaLinear.Rows[6].Cells[$"colX17"].Value = 2;
            gridSistemaLinear.Rows[6].Cells[$"colX18"].Value = -4;
            gridSistemaLinear.Rows[6].Cells[$"colX19"].Value = 5;
            gridSistemaLinear.Rows[6].Cells[$"colX20"].Value = 3;
            gridSistemaLinear.Rows[6].Cells[$"colR"].Value = 389;
            #endregion

            #region 8° Linha do Sistema Linear
            gridSistemaLinear.Rows[7].Cells[$"colX1"].Value = 1;
            gridSistemaLinear.Rows[7].Cells[$"colX2"].Value = 9;
            gridSistemaLinear.Rows[7].Cells[$"colX3"].Value = 5;
            gridSistemaLinear.Rows[7].Cells[$"colX4"].Value = 5;
            gridSistemaLinear.Rows[7].Cells[$"colX5"].Value = 4;
            gridSistemaLinear.Rows[7].Cells[$"colX6"].Value = 5;
            gridSistemaLinear.Rows[7].Cells[$"colX7"].Value = 6;
            gridSistemaLinear.Rows[7].Cells[$"colX8"].Value = 8;
            gridSistemaLinear.Rows[7].Cells[$"colX9"].Value = 11;
            gridSistemaLinear.Rows[7].Cells[$"colX10"].Value = 7;
            gridSistemaLinear.Rows[7].Cells[$"colX11"].Value = 10;
            gridSistemaLinear.Rows[7].Cells[$"colX12"].Value = 10;
            gridSistemaLinear.Rows[7].Cells[$"colX13"].Value = 2;
            gridSistemaLinear.Rows[7].Cells[$"colX14"].Value = 12;
            gridSistemaLinear.Rows[7].Cells[$"colX15"].Value = 5;
            gridSistemaLinear.Rows[7].Cells[$"colX16"].Value = 10;
            gridSistemaLinear.Rows[7].Cells[$"colX17"].Value = 2;
            gridSistemaLinear.Rows[7].Cells[$"colX18"].Value = 3;
            gridSistemaLinear.Rows[7].Cells[$"colX19"].Value = 9;
            gridSistemaLinear.Rows[7].Cells[$"colX20"].Value = 10;
            gridSistemaLinear.Rows[7].Cells[$"colR"].Value = 648;
            #endregion

            #region 9° Linha do Sistema Linear
            gridSistemaLinear.Rows[8].Cells[$"colX1"].Value = 2;
            gridSistemaLinear.Rows[8].Cells[$"colX2"].Value = 5;
            gridSistemaLinear.Rows[8].Cells[$"colX3"].Value = 3;
            gridSistemaLinear.Rows[8].Cells[$"colX4"].Value = 10;
            gridSistemaLinear.Rows[8].Cells[$"colX5"].Value = -4;
            gridSistemaLinear.Rows[8].Cells[$"colX6"].Value = 5;
            gridSistemaLinear.Rows[8].Cells[$"colX7"].Value = 10;
            gridSistemaLinear.Rows[8].Cells[$"colX8"].Value = 7;
            gridSistemaLinear.Rows[8].Cells[$"colX9"].Value = 1;
            gridSistemaLinear.Rows[8].Cells[$"colX10"].Value = 4;
            gridSistemaLinear.Rows[8].Cells[$"colX11"].Value = 9;
            gridSistemaLinear.Rows[8].Cells[$"colX12"].Value = 3;
            gridSistemaLinear.Rows[8].Cells[$"colX13"].Value = 3;
            gridSistemaLinear.Rows[8].Cells[$"colX14"].Value = -6;
            gridSistemaLinear.Rows[8].Cells[$"colX15"].Value = 4;
            gridSistemaLinear.Rows[8].Cells[$"colX16"].Value = 9;
            gridSistemaLinear.Rows[8].Cells[$"colX17"].Value = 10;
            gridSistemaLinear.Rows[8].Cells[$"colX18"].Value = 2;
            gridSistemaLinear.Rows[8].Cells[$"colX19"].Value = 7;
            gridSistemaLinear.Rows[8].Cells[$"colX20"].Value = 1;
            gridSistemaLinear.Rows[8].Cells[$"colR"].Value = 295;
            #endregion

            #region 10° Linha do Sistema Linear
            gridSistemaLinear.Rows[9].Cells[$"colX1"].Value = 6;
            gridSistemaLinear.Rows[9].Cells[$"colX2"].Value = 9;
            gridSistemaLinear.Rows[9].Cells[$"colX3"].Value = 4;
            gridSistemaLinear.Rows[9].Cells[$"colX4"].Value = 11;
            gridSistemaLinear.Rows[9].Cells[$"colX5"].Value = 10;
            gridSistemaLinear.Rows[9].Cells[$"colX6"].Value = 10;
            gridSistemaLinear.Rows[9].Cells[$"colX7"].Value = 8;
            gridSistemaLinear.Rows[9].Cells[$"colX8"].Value = 6;
            gridSistemaLinear.Rows[9].Cells[$"colX9"].Value = 4;
            gridSistemaLinear.Rows[9].Cells[$"colX10"].Value = 7;
            gridSistemaLinear.Rows[9].Cells[$"colX11"].Value = 8;
            gridSistemaLinear.Rows[9].Cells[$"colX12"].Value = 3;
            gridSistemaLinear.Rows[9].Cells[$"colX13"].Value = 10;
            gridSistemaLinear.Rows[9].Cells[$"colX14"].Value = 11;
            gridSistemaLinear.Rows[9].Cells[$"colX15"].Value = 11;
            gridSistemaLinear.Rows[9].Cells[$"colX16"].Value = 6;
            gridSistemaLinear.Rows[9].Cells[$"colX17"].Value = 6;
            gridSistemaLinear.Rows[9].Cells[$"colX18"].Value = 7;
            gridSistemaLinear.Rows[9].Cells[$"colX19"].Value = 8;
            gridSistemaLinear.Rows[9].Cells[$"colX20"].Value = 3;
            gridSistemaLinear.Rows[9].Cells[$"colR"].Value = 780;
            #endregion

            #region 11° Linha do Sistema Linear
            gridSistemaLinear.Rows[10].Cells[$"colX1"].Value = 6;
            gridSistemaLinear.Rows[10].Cells[$"colX2"].Value = 10;
            gridSistemaLinear.Rows[10].Cells[$"colX3"].Value = 7;
            gridSistemaLinear.Rows[10].Cells[$"colX4"].Value = 5;
            gridSistemaLinear.Rows[10].Cells[$"colX5"].Value = 0;
            gridSistemaLinear.Rows[10].Cells[$"colX6"].Value = 7;
            gridSistemaLinear.Rows[10].Cells[$"colX7"].Value = 8;
            gridSistemaLinear.Rows[10].Cells[$"colX8"].Value = 6;
            gridSistemaLinear.Rows[10].Cells[$"colX9"].Value = 8;
            gridSistemaLinear.Rows[10].Cells[$"colX10"].Value = 11;
            gridSistemaLinear.Rows[10].Cells[$"colX11"].Value = 1;
            gridSistemaLinear.Rows[10].Cells[$"colX12"].Value = 5;
            gridSistemaLinear.Rows[10].Cells[$"colX13"].Value = 2;
            gridSistemaLinear.Rows[10].Cells[$"colX14"].Value = -7;
            gridSistemaLinear.Rows[10].Cells[$"colX15"].Value = 8;
            gridSistemaLinear.Rows[10].Cells[$"colX16"].Value = 7;
            gridSistemaLinear.Rows[10].Cells[$"colX17"].Value = 8;
            gridSistemaLinear.Rows[10].Cells[$"colX18"].Value = -7;
            gridSistemaLinear.Rows[10].Cells[$"colX19"].Value = 10;
            gridSistemaLinear.Rows[10].Cells[$"colX20"].Value = 3;
            gridSistemaLinear.Rows[10].Cells[$"colR"].Value = 541;
            #endregion

            #region 12° Linha do Sistema Linear
            gridSistemaLinear.Rows[11].Cells[$"colX1"].Value = 5;
            gridSistemaLinear.Rows[11].Cells[$"colX2"].Value = 9;
            gridSistemaLinear.Rows[11].Cells[$"colX3"].Value = 10;
            gridSistemaLinear.Rows[11].Cells[$"colX4"].Value = 8;
            gridSistemaLinear.Rows[11].Cells[$"colX5"].Value = 7;
            gridSistemaLinear.Rows[11].Cells[$"colX6"].Value = 2;
            gridSistemaLinear.Rows[11].Cells[$"colX7"].Value = 5;
            gridSistemaLinear.Rows[11].Cells[$"colX8"].Value = 4;
            gridSistemaLinear.Rows[11].Cells[$"colX9"].Value = 2;
            gridSistemaLinear.Rows[11].Cells[$"colX10"].Value = 7;
            gridSistemaLinear.Rows[11].Cells[$"colX11"].Value = 5;
            gridSistemaLinear.Rows[11].Cells[$"colX12"].Value = 4;
            gridSistemaLinear.Rows[11].Cells[$"colX13"].Value = 7;
            gridSistemaLinear.Rows[11].Cells[$"colX14"].Value = 8;
            gridSistemaLinear.Rows[11].Cells[$"colX15"].Value = 7;
            gridSistemaLinear.Rows[11].Cells[$"colX16"].Value = 2;
            gridSistemaLinear.Rows[11].Cells[$"colX17"].Value = 5;
            gridSistemaLinear.Rows[11].Cells[$"colX18"].Value = 6;
            gridSistemaLinear.Rows[11].Cells[$"colX19"].Value = 1;
            gridSistemaLinear.Rows[11].Cells[$"colX20"].Value = 5;
            gridSistemaLinear.Rows[11].Cells[$"colR"].Value = 599;
            #endregion

            #region 13° Linha do Sistema Linear
            gridSistemaLinear.Rows[12].Cells[$"colX1"].Value = 11;
            gridSistemaLinear.Rows[12].Cells[$"colX2"].Value = 7;
            gridSistemaLinear.Rows[12].Cells[$"colX3"].Value = 11;
            gridSistemaLinear.Rows[12].Cells[$"colX4"].Value = 2;
            gridSistemaLinear.Rows[12].Cells[$"colX5"].Value = -4;
            gridSistemaLinear.Rows[12].Cells[$"colX6"].Value = 11;
            gridSistemaLinear.Rows[12].Cells[$"colX7"].Value = 7;
            gridSistemaLinear.Rows[12].Cells[$"colX8"].Value = 5;
            gridSistemaLinear.Rows[12].Cells[$"colX9"].Value = 2;
            gridSistemaLinear.Rows[12].Cells[$"colX10"].Value = 2;
            gridSistemaLinear.Rows[12].Cells[$"colX11"].Value = 6;
            gridSistemaLinear.Rows[12].Cells[$"colX12"].Value = 10;
            gridSistemaLinear.Rows[12].Cells[$"colX13"].Value = 5;
            gridSistemaLinear.Rows[12].Cells[$"colX14"].Value = -1;
            gridSistemaLinear.Rows[12].Cells[$"colX15"].Value = 8;
            gridSistemaLinear.Rows[12].Cells[$"colX16"].Value = 7;
            gridSistemaLinear.Rows[12].Cells[$"colX17"].Value = 2;
            gridSistemaLinear.Rows[12].Cells[$"colX18"].Value = -6;
            gridSistemaLinear.Rows[12].Cells[$"colX19"].Value = 5;
            gridSistemaLinear.Rows[12].Cells[$"colX20"].Value = 8;
            gridSistemaLinear.Rows[12].Cells[$"colR"].Value = 501;
            #endregion

            #region 14° Linha do Sistema Linear
            gridSistemaLinear.Rows[13].Cells[$"colX1"].Value = 2;
            gridSistemaLinear.Rows[13].Cells[$"colX2"].Value = 7;
            gridSistemaLinear.Rows[13].Cells[$"colX3"].Value = 3;
            gridSistemaLinear.Rows[13].Cells[$"colX4"].Value = 8;
            gridSistemaLinear.Rows[13].Cells[$"colX5"].Value = 5;
            gridSistemaLinear.Rows[13].Cells[$"colX6"].Value = 2;
            gridSistemaLinear.Rows[13].Cells[$"colX7"].Value = 7;
            gridSistemaLinear.Rows[13].Cells[$"colX8"].Value = 9;
            gridSistemaLinear.Rows[13].Cells[$"colX9"].Value = 10;
            gridSistemaLinear.Rows[13].Cells[$"colX10"].Value = 6;
            gridSistemaLinear.Rows[13].Cells[$"colX11"].Value = 5;
            gridSistemaLinear.Rows[13].Cells[$"colX12"].Value = 9;
            gridSistemaLinear.Rows[13].Cells[$"colX13"].Value = 1;
            gridSistemaLinear.Rows[13].Cells[$"colX14"].Value = 6;
            gridSistemaLinear.Rows[13].Cells[$"colX15"].Value = 9;
            gridSistemaLinear.Rows[13].Cells[$"colX16"].Value = 3;
            gridSistemaLinear.Rows[13].Cells[$"colX17"].Value = 3;
            gridSistemaLinear.Rows[13].Cells[$"colX18"].Value = 4;
            gridSistemaLinear.Rows[13].Cells[$"colX19"].Value = 5;
            gridSistemaLinear.Rows[13].Cells[$"colX20"].Value = 11;
            gridSistemaLinear.Rows[13].Cells[$"colR"].Value = 631;
            #endregion

            #region 15° Linha do Sistema Linear
            gridSistemaLinear.Rows[14].Cells[$"colX1"].Value = 5;
            gridSistemaLinear.Rows[14].Cells[$"colX2"].Value = 11;
            gridSistemaLinear.Rows[14].Cells[$"colX3"].Value = 8;
            gridSistemaLinear.Rows[14].Cells[$"colX4"].Value = 9;
            gridSistemaLinear.Rows[14].Cells[$"colX5"].Value = -4;
            gridSistemaLinear.Rows[14].Cells[$"colX6"].Value = 3;
            gridSistemaLinear.Rows[14].Cells[$"colX7"].Value = 3;
            gridSistemaLinear.Rows[14].Cells[$"colX8"].Value = 4;
            gridSistemaLinear.Rows[14].Cells[$"colX9"].Value = 11;
            gridSistemaLinear.Rows[14].Cells[$"colX10"].Value = 9;
            gridSistemaLinear.Rows[14].Cells[$"colX11"].Value = 1;
            gridSistemaLinear.Rows[14].Cells[$"colX12"].Value = 6;
            gridSistemaLinear.Rows[14].Cells[$"colX13"].Value = 11;
            gridSistemaLinear.Rows[14].Cells[$"colX14"].Value = -8;
            gridSistemaLinear.Rows[14].Cells[$"colX15"].Value = 2;
            gridSistemaLinear.Rows[14].Cells[$"colX16"].Value = 8;
            gridSistemaLinear.Rows[14].Cells[$"colX17"].Value = 7;
            gridSistemaLinear.Rows[14].Cells[$"colX18"].Value = -3;
            gridSistemaLinear.Rows[14].Cells[$"colX19"].Value = 2;
            gridSistemaLinear.Rows[14].Cells[$"colX20"].Value = 4;
            gridSistemaLinear.Rows[14].Cells[$"colR"].Value = 378;
            #endregion

            #region 16° Linha do Sistema Linear
            gridSistemaLinear.Rows[15].Cells[$"colX1"].Value = 11;
            gridSistemaLinear.Rows[15].Cells[$"colX2"].Value = 6;
            gridSistemaLinear.Rows[15].Cells[$"colX3"].Value = 8;
            gridSistemaLinear.Rows[15].Cells[$"colX4"].Value = 1;
            gridSistemaLinear.Rows[15].Cells[$"colX5"].Value = 7;
            gridSistemaLinear.Rows[15].Cells[$"colX6"].Value = 11;
            gridSistemaLinear.Rows[15].Cells[$"colX7"].Value = 3;
            gridSistemaLinear.Rows[15].Cells[$"colX8"].Value = 5;
            gridSistemaLinear.Rows[15].Cells[$"colX9"].Value = 10;
            gridSistemaLinear.Rows[15].Cells[$"colX10"].Value = 8;
            gridSistemaLinear.Rows[15].Cells[$"colX11"].Value = 2;
            gridSistemaLinear.Rows[15].Cells[$"colX12"].Value = 10;
            gridSistemaLinear.Rows[15].Cells[$"colX13"].Value = 5;
            gridSistemaLinear.Rows[15].Cells[$"colX14"].Value = 11;
            gridSistemaLinear.Rows[15].Cells[$"colX15"].Value = 4;
            gridSistemaLinear.Rows[15].Cells[$"colX16"].Value = 6;
            gridSistemaLinear.Rows[15].Cells[$"colX17"].Value = 2;
            gridSistemaLinear.Rows[15].Cells[$"colX18"].Value = 9;
            gridSistemaLinear.Rows[15].Cells[$"colX19"].Value = 2;
            gridSistemaLinear.Rows[15].Cells[$"colX20"].Value = 10;
            gridSistemaLinear.Rows[15].Cells[$"colR"].Value = 731;
            #endregion

            #region 17° Linha do Sistema Linear
            gridSistemaLinear.Rows[16].Cells[$"colX1"].Value = 3;
            gridSistemaLinear.Rows[16].Cells[$"colX2"].Value = 5;
            gridSistemaLinear.Rows[16].Cells[$"colX3"].Value = 10;
            gridSistemaLinear.Rows[16].Cells[$"colX4"].Value = 3;
            gridSistemaLinear.Rows[16].Cells[$"colX5"].Value = -7;
            gridSistemaLinear.Rows[16].Cells[$"colX6"].Value = 8;
            gridSistemaLinear.Rows[16].Cells[$"colX7"].Value = 6;
            gridSistemaLinear.Rows[16].Cells[$"colX8"].Value = 6;
            gridSistemaLinear.Rows[16].Cells[$"colX9"].Value = 4;
            gridSistemaLinear.Rows[16].Cells[$"colX10"].Value = 8;
            gridSistemaLinear.Rows[16].Cells[$"colX11"].Value = 6;
            gridSistemaLinear.Rows[16].Cells[$"colX12"].Value = 6;
            gridSistemaLinear.Rows[16].Cells[$"colX13"].Value = 6;
            gridSistemaLinear.Rows[16].Cells[$"colX14"].Value = -3;
            gridSistemaLinear.Rows[16].Cells[$"colX15"].Value = 10;
            gridSistemaLinear.Rows[16].Cells[$"colX16"].Value = 2;
            gridSistemaLinear.Rows[16].Cells[$"colX17"].Value = 2;
            gridSistemaLinear.Rows[16].Cells[$"colX18"].Value = -8;
            gridSistemaLinear.Rows[16].Cells[$"colX19"].Value = 11;
            gridSistemaLinear.Rows[16].Cells[$"colX20"].Value = 10;
            gridSistemaLinear.Rows[16].Cells[$"colR"].Value = 475;
            #endregion

            #region 18° Linha do Sistema Linear
            gridSistemaLinear.Rows[17].Cells[$"colX1"].Value = 8;
            gridSistemaLinear.Rows[17].Cells[$"colX2"].Value = 9;
            gridSistemaLinear.Rows[17].Cells[$"colX3"].Value = 8;
            gridSistemaLinear.Rows[17].Cells[$"colX4"].Value = 11;
            gridSistemaLinear.Rows[17].Cells[$"colX5"].Value = 9;
            gridSistemaLinear.Rows[17].Cells[$"colX6"].Value = 3;
            gridSistemaLinear.Rows[17].Cells[$"colX7"].Value = 8;
            gridSistemaLinear.Rows[17].Cells[$"colX8"].Value = 9;
            gridSistemaLinear.Rows[17].Cells[$"colX9"].Value = 6;
            gridSistemaLinear.Rows[17].Cells[$"colX10"].Value = 8;
            gridSistemaLinear.Rows[17].Cells[$"colX11"].Value = 5;
            gridSistemaLinear.Rows[17].Cells[$"colX12"].Value = 6;
            gridSistemaLinear.Rows[17].Cells[$"colX13"].Value = 5;
            gridSistemaLinear.Rows[17].Cells[$"colX14"].Value = 8;
            gridSistemaLinear.Rows[17].Cells[$"colX15"].Value = 7;
            gridSistemaLinear.Rows[17].Cells[$"colX16"].Value = 5;
            gridSistemaLinear.Rows[17].Cells[$"colX17"].Value = 1;
            gridSistemaLinear.Rows[17].Cells[$"colX18"].Value = 4;
            gridSistemaLinear.Rows[17].Cells[$"colX19"].Value = 10;
            gridSistemaLinear.Rows[17].Cells[$"colX20"].Value = 4;
            gridSistemaLinear.Rows[17].Cells[$"colR"].Value = 767;
            #endregion

            #region 19° Linha do Sistema Linear
            gridSistemaLinear.Rows[18].Cells[$"colX1"].Value = 1;
            gridSistemaLinear.Rows[18].Cells[$"colX2"].Value = 10;
            gridSistemaLinear.Rows[18].Cells[$"colX3"].Value = 6;
            gridSistemaLinear.Rows[18].Cells[$"colX4"].Value = 3;
            gridSistemaLinear.Rows[18].Cells[$"colX5"].Value = 2;
            gridSistemaLinear.Rows[18].Cells[$"colX6"].Value = 6;
            gridSistemaLinear.Rows[18].Cells[$"colX7"].Value = 11;
            gridSistemaLinear.Rows[18].Cells[$"colX8"].Value = 9;
            gridSistemaLinear.Rows[18].Cells[$"colX9"].Value = 9;
            gridSistemaLinear.Rows[18].Cells[$"colX10"].Value = 3;
            gridSistemaLinear.Rows[18].Cells[$"colX11"].Value = 6;
            gridSistemaLinear.Rows[18].Cells[$"colX12"].Value = 2;
            gridSistemaLinear.Rows[18].Cells[$"colX13"].Value = 10;
            gridSistemaLinear.Rows[18].Cells[$"colX14"].Value = -5;
            gridSistemaLinear.Rows[18].Cells[$"colX15"].Value = 3;
            gridSistemaLinear.Rows[18].Cells[$"colX16"].Value = 7;
            gridSistemaLinear.Rows[18].Cells[$"colX17"].Value = 1;
            gridSistemaLinear.Rows[18].Cells[$"colX18"].Value = -8;
            gridSistemaLinear.Rows[18].Cells[$"colX19"].Value = 9;
            gridSistemaLinear.Rows[18].Cells[$"colX20"].Value = 9;
            gridSistemaLinear.Rows[18].Cells[$"colR"].Value = 475;
            #endregion

            #region 20° Linha do Sistema Linear
            gridSistemaLinear.Rows[19].Cells[$"colX1"].Value = 5;
            gridSistemaLinear.Rows[19].Cells[$"colX2"].Value = 6;
            gridSistemaLinear.Rows[19].Cells[$"colX3"].Value = 11;
            gridSistemaLinear.Rows[19].Cells[$"colX4"].Value = 2;
            gridSistemaLinear.Rows[19].Cells[$"colX5"].Value = 2;
            gridSistemaLinear.Rows[19].Cells[$"colX6"].Value = 10;
            gridSistemaLinear.Rows[19].Cells[$"colX7"].Value = 8;
            gridSistemaLinear.Rows[19].Cells[$"colX8"].Value = 3;
            gridSistemaLinear.Rows[19].Cells[$"colX9"].Value = 5;
            gridSistemaLinear.Rows[19].Cells[$"colX10"].Value = 11;
            gridSistemaLinear.Rows[19].Cells[$"colX11"].Value = 6;
            gridSistemaLinear.Rows[19].Cells[$"colX12"].Value = 11;
            gridSistemaLinear.Rows[19].Cells[$"colX13"].Value = 4;
            gridSistemaLinear.Rows[19].Cells[$"colX14"].Value = 9;
            gridSistemaLinear.Rows[19].Cells[$"colX15"].Value = 10;
            gridSistemaLinear.Rows[19].Cells[$"colX16"].Value = 3;
            gridSistemaLinear.Rows[19].Cells[$"colX17"].Value = 11;
            gridSistemaLinear.Rows[19].Cells[$"colX18"].Value = 5;
            gridSistemaLinear.Rows[19].Cells[$"colX19"].Value = 9;
            gridSistemaLinear.Rows[19].Cells[$"colX20"].Value = 9;
            gridSistemaLinear.Rows[19].Cells[$"colR"].Value = 805;
            #endregion
        }
        #endregion

        /// <summary>
        /// Evento click do botão Importar e Calcular
        /// </summary>
        #region Botão Importar e Calcular
        private void btnImportar_Click(object sender, EventArgs e)
        {
            // Variáveis utilizadas
            int cont = 0;
            int contLinhas = 0;
            string[] conteudo = { "", "" };
            string[] dadosLinha = { "", "" };

            // Buscar e setar a pasta inicial de abertura da aba aberta pelo OpenFileDialog
            string path = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            path = System.IO.Path.GetDirectoryName(path);
            ofd_Valores.InitialDirectory = path;

            // Filtra os arquivos permitidos da aba aberta pelo OpenFileDialog, para permitir somente a seleção e abertura de arquivo texto (.txt)
            ofd_Valores.Filter = "Arquivo texto |*.txt;";

            // Altera o título da aba aberta pelo OpenFileDialog
            ofd_Valores.Title = "Escolha o arquivo texto para importar";

            // Nome inicial padrão do arquivo texto selecionado quando é aberta a aba pelo OpenFileDialog
            ofd_Valores.FileName = "dados.txt";

            // Verifica se o arquivo foi selecionado e clicado no botão OK (Abrir) na aba aberta
            if (ofd_Valores.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Armazena na variável conteudo, todas as linhas do arquivo texto selecionado
                    conteudo = File.ReadAllLines(ofd_Valores.FileName);

                    // Limpa o txtResultado
                    // txtResultado.Text = "";
                    sb.Clear();

                    foreach (string linha in conteudo)
                    {
                        // Verifica se a quantidade de linhas lidas até então é igual a quantidade de variáveis do sistema linear em questão
                        if (contLinhas == cont - 1)
                            contLinhas = 0;

                        dadosLinha = linha.Split(';');
                        cont = dadosLinha.Length;

                        // Verifica se a quantidade de linhas lidas até então é igual a 0
                        if (contLinhas == 0)
                        {
                            // Adiciona linhas e colunas no gridSistemaLinear, de acordo com a quantidade de variáveis
                            AddLinhaColunaGrid(cont - 1);
                            // Altera o valor do NumericUpDown para o valor correspondente a quantidade de variáveis
                            nudQtdVariaveis.Value = cont - 1;
                        }

                        for (int i = 0; i < cont; i++)
                        {
                            // Preenche a coluna R do gridSistemaLinear na linha indicada pelo ContLinhas
                            if (i == cont - 1)
                                gridSistemaLinear.Rows[contLinhas].Cells[$"colR"].Value = dadosLinha[i];
                            // Preenche a coluna Xn do gridSistemaLinear na linha indicada pelo ContLinhas
                            else
                                gridSistemaLinear.Rows[contLinhas].Cells[$"colX{i + 1}"].Value = dadosLinha[i];
                        }

                        // Verifica se a quantidade de linhas lidas até então é diferente da quantidade de variáveis do sistema linear em questão
                        if (contLinhas != cont - 1)
                            contLinhas++;
                        else
                            contLinhas = 0;

                        // Verifica se a quantidade de linhas lidas até então é igual a quantidade de variáveis do sistema linear em questão
                        if (contLinhas == cont - 1)
                        {
                            // Altera a variável booleana para UsouArquivoTxt = true, para não limpar o txtResultado,
                            // e sim continuar apresentando os próximos sistemas lineares em sequência no txtResultado
                            UsouArquivoTxt = true;

                            // Executa o evento click do botão Calcular
                            btnCalcular_Click(sender, e);

                            // Altera a variável booleana para UsouArquivoTxt = false
                            UsouArquivoTxt = false;
                        }
                    }
                }
                catch
                {
                    // Adiciona 3 linhas e 4 colunas no gridSistemaLinear
                    AddLinhaColunaGrid(3);

                    // Altera o valor do NumericUpDown para 3
                    nudQtdVariaveis.Value = 3;

                    // Mensagem de alerta, informando que o conteúdo do arquivo escolhido não está no formato correto para o cálculo de um sistema linear
                    MessageBox.Show("O conteúdo do arquivo escolhido não está no formato correto para o cálculo de um sistema linear", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                // Mensagem de alerta, informando que falta escolher um arquivo texto
                MessageBox.Show("Escolha um arquivo texto!!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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