using Projeto.Controller; // Adicionado
using Projeto.Views.Cadastros; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Projeto.Views.Consultas 
{
    public partial class frmConsultasContasAPagar : Projeto.frmBaseConsulta
    {
        private frmCadastroContasAPagar oFrmCadastroContasAPagar;

        private ContasAPagarController controller = new ContasAPagarController();

        public frmConsultasContasAPagar()
        {
            InitializeComponent();

            // Renomeia os botões para fazer sentido neste contexto
            btnIncluir.Text = "Lançar Conta"; // Para contas manuais (luz)
            btnEditar.Text = "Pagar/Baixar";  // Para pagar
            btnDeletar.Text = "Estornar";     // Para reverter um pagamento
        }

        // Método padrão do seu projeto para "ligar" o cadastro
        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroContasAPagar = (frmCadastroContasAPagar)obj;
            }
        }

        // Evento do botão "Lançar Conta" (novo)
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            // (Aqui você pode adicionar 'modoInclusao = true' no oFrmCadastroContasAPagar)
            oFrmCadastroContasAPagar.LimparTxt();
            oFrmCadastroContasAPagar.ShowDialog();
            // (Aqui chamaremos o CarregarContas() depois)
        }
    }
}