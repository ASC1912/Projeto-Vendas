using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroMesa : frmBase
    {
        private Mesa aMesa;
        private MesaController controller = new MesaController();
        public bool modoEdicao = false;
        public bool modoExclusao = false;

        public frmCadastroMesa()
        {
            InitializeComponent();

            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);
            this.txtQtdCadeiras.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null)
            {
                aMesa = (Mesa)obj;
            }
            if (ctrl != null)
            {
                controller = (MesaController)ctrl;
            }
        }

        public override void BloquearTxt()
        {
            txtCodigo.Enabled = false;
            txtQtdCadeiras.Enabled = false;
            txtLocalizacao.Enabled = false;
            cbStatus.Enabled = false;
            chkInativo.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            txtCodigo.Enabled = !modoEdicao;
            txtQtdCadeiras.Enabled = true;
            txtLocalizacao.Enabled = true;
            cbStatus.Enabled = true;
            chkInativo.Enabled = true;
        }

        public override void LimparTxt()
        {
            txtQtdCadeiras.Clear();
            txtLocalizacao.Clear();
            cbStatus.SelectedIndex = 0;
            chkInativo.Checked = false;

            base.LimparTxt();
            txtCodigo.Text = "";
            txtCodigo.Enabled = true;
        }

        public override void CarregaTxt()
        {
            txtCodigo.Text = aMesa.NumeroMesa.ToString();
            txtQtdCadeiras.Text = aMesa.QuantidadeCadeiras.ToString();
            txtLocalizacao.Text = aMesa.Localizacao;

            switch (aMesa.StatusMesa)
            {
                case 'O':
                    cbStatus.SelectedItem = "OCUPADO";
                    break;
                case 'R':
                    cbStatus.SelectedItem = "RESERVADO";
                    break;
                default:
                    cbStatus.SelectedItem = "LIVRE";
                    break;
            }

            chkInativo.Checked = !aMesa.Ativo;
            lblDataCriacao.Text = aMesa.DataCadastro.HasValue ? $"Criado em: {aMesa.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = aMesa.DataAlteracao.HasValue ? $"Modificado em: {aMesa.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";

            txtCodigo.Enabled = false;
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir esta mesa?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int numeroMesa = int.Parse(txtCodigo.Text);
                        await controller.Excluir(numeroMesa);
                        MessageBox.Show("Mesa excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir a mesa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else 
            {
                if (!Validador.CampoObrigatorio(txtCodigo, "O número da mesa é obrigatório.")) return;
                if (!Validador.ValidarNumerico(txtCodigo, "O número da mesa deve ser um valor numérico.")) return;
                if (!Validador.CampoObrigatorio(txtQtdCadeiras, "A quantidade de cadeiras é obrigatória.")) return;
                if (!Validador.ValidarNumerico(txtQtdCadeiras, "A quantidade de cadeiras deve ser um valor numérico.")) return;

                try
                {
                    char status;
                    switch (cbStatus.SelectedItem.ToString())
                    {
                        case "OCUPADA":
                            status = 'O';
                            break;
                        case "RESERVADA":
                            status = 'R';
                            break;
                        default:
                            status = 'L';
                            break;
                    }

                    Mesa mesa = new Mesa
                    {
                        NumeroMesa = int.Parse(txtCodigo.Text),
                        QuantidadeCadeiras = int.Parse(txtQtdCadeiras.Text),
                        Localizacao = txtLocalizacao.Text,
                        StatusMesa = status,
                        Ativo = !chkInativo.Checked
                    };

                    if (modoEdicao)
                    {
                        mesa.DataCadastro = aMesa.DataCadastro;
                        mesa.DataAlteracao = DateTime.Now;
                    }
                    else
                    {
                        mesa.DataCadastro = DateTime.Now;
                        mesa.DataAlteracao = DateTime.Now;
                    }

                    await controller.Salvar(mesa);
                    MessageBox.Show("Mesa salva com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar a mesa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}