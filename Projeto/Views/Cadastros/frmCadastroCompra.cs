using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroCompra : Projeto.frmBase
    {
        private Compra aCompra; 
        private CompraController controller = new CompraController(); 

        private List<Control> parte1Controles;
        private List<Control> parte2Controles;
        private List<Control> parte3Controles;
        public frmCadastroCompra()
        {
            InitializeComponent();
            AgruparControles();
            ConfigurarEstadoInicial(); 
        }

        private void AgruparControles()
        {
            parte1Controles = new List<Control>
        {
            txtCodigo, // Campo "Modelo"
            txtSerie,
            txtNumero,
            txtIDFornecedor,
            txtFornecedor,
            btnPesquisar,
            dtpEmissao,      
            dtpChegada       
        };

            parte2Controles = new List<Control>
        {
            txtProduto,
            btnPesquisarProduto,
            txtQuantidade,
            txtValorUnitario,
            txtTotal,
            btnAdicionarProduto,
            btnEditarProduto,
            btnRemoverProduto,
            btnLimparProduto,
            listViewProdutos
        };

            parte3Controles = new List<Control>
        {
            txtFrete,
            txtSeguro,
            txtDespesas,
            txtValorTotal,
            txtCondPgto,
            btnAdicionarCondPgto, 
            btnLimparCondPgto, 
            listViewCondPgto
        };
        }

        private void ConfigurarEstadoInicial()
        {
            HabilitarControles(parte1Controles, true);
            HabilitarControles(parte2Controles, false);
            HabilitarControles(parte3Controles, false);
            btnSalvar.Enabled = false;
        }

        private void HabilitarControles(List<Control> controles, bool habilitar)
        {
            foreach (var control in controles)
            {
                control.Enabled = habilitar;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null)
            {
                aCompra = (Compra)obj; 
            }

            if (ctrl != null)
            {
                controller = (CompraController)ctrl; 
            }
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            txtSerie.Clear();
            txtNumero.Clear();
            txtIDFornecedor.Clear();
            txtFornecedor.Clear();
            dtpEmissao.Value = DateTime.Now;
            dtpChegada.Value = DateTime.Now;

            txtProduto.Clear();
            txtQuantidade.Clear();
            txtValorUnitario.Clear();
            txtTotal.Clear();
            listViewProdutos.Items.Clear();

            txtFrete.Clear();
            txtSeguro.Clear();
            txtDespesas.Clear();
            txtValorTotal.Clear();
            txtCondPgto.Clear();
            listViewCondPgto.Items.Clear();

            ConfigurarEstadoInicial();
        }

        public override void CarregaTxt()
        {
         
        }

        public override void BloquearTxt()
        {
            HabilitarControles(parte1Controles, false);
            HabilitarControles(parte2Controles, false);
            HabilitarControles(parte3Controles, false);
        }

        public override void DesbloquearTxt()
        {
            HabilitarControles(parte1Controles, true);
            HabilitarControles(parte2Controles, true);
            HabilitarControles(parte3Controles, true);
        }

    }
}
