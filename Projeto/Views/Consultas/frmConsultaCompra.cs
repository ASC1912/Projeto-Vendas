using Projeto.Controller;
using Projeto.Models;
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
    public partial class frmConsultaCompra : Projeto.frmBaseConsulta
    {
        private CompraController controller = new CompraController();
        internal Marca MarcaSelecionado { get; private set; }
        private frmCadastroCompra oFrmCadastroCompra;
        public frmConsultaCompra()
        {
            InitializeComponent();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroCompra = (frmCadastroCompra)obj;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (CompraController)ctrl;
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            //oFrmCadastroCompra.LimparTxt();
            oFrmCadastroCompra.ShowDialog();
        }
    }
}
