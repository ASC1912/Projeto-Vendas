using Projeto.Controller;
using Projeto.DAO;
using Projeto.Models;
using Projeto.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views
{
    public partial class frmCadastroEstado : Projeto.frmBase
    {
        frmConsultaPais oFrmConsultaPais;
        Estado oEstado;
        private PaisController paisController = new PaisController();
        private EstadoController controller = new EstadoController();
        private int paisSelecionadoId = -1;
        public bool modoEdicao = false;
        public bool modoExclusao = false;

        public frmCadastroEstado() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;

            this.txtIdPais.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null)
            {
                oEstado = (Estado)obj;
            }
            if (ctrl != null)
            {
                controller = (EstadoController)ctrl;
            }
        }
        public void setFrmConsultaPais(object obj)
        {
            if (obj != null)
            {
                oFrmConsultaPais = (frmConsultaPais)obj;
            }
        }

        public override void CarregaTxt()
        {
            txtCodigo.Text = oEstado.Id.ToString();
            txtNome.Text = oEstado.NomeEstado;
            txtUF.Text = oEstado.UF;
            txtIdPais.Text = oEstado.PaisId.ToString(); 
            txtPais.Text = oEstado.PaisNome;
            paisSelecionadoId = oEstado.PaisId;
            chkInativo.Checked = !oEstado.Ativo;
            lblDataCriacao.Text = oEstado.DataCadastro.HasValue ? $"Criado em: {oEstado.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = oEstado.DataAlteracao.HasValue ? $"Modificado em: {oEstado.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";
        }

        public override void BloquearTxt()
        {
            txtNome.Enabled = false;
            txtUF.Enabled = false;
            txtIdPais.Enabled = false; 
            btnBuscar.Enabled = false;
            chkInativo.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            txtNome.Enabled = true;
            txtUF.Enabled = true;
            txtIdPais.Enabled = true; 
            btnBuscar.Enabled = true;
            chkInativo.Enabled = true;
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            txtNome.Clear();
            txtUF.Clear();
            txtIdPais.Clear(); // Adicionado
            txtPais.Clear();
            paisSelecionadoId = -1;
            chkInativo.Checked = false;

            DateTime agora = DateTime.Now;
            lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
            lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
        }

        private void frmCadastroEstado_Load(object sender, EventArgs e)
        {

        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este Estado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        await controller.Excluir(id);
                        MessageBox.Show("Estado excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Cannot delete or update a parent row"))
                        {
                            MessageBox.Show("Não é possível excluir este item, pois existem registros vinculados a ele.", "Erro ao excluir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro ao excluir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                if (!Validador.CampoObrigatorio(txtNome, "O nome do estado é obrigatório.")) return;
                if (!Validador.ValidarIdSelecionado(paisSelecionadoId, "Selecione um país!")) return;

                try
                {
                    int id = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
                    string nome = txtNome.Text;

                    List<Estado> estados = await controller.ListarEstado();

                    if (Validador.VerificarDuplicidade(estados, item =>
                        item.NomeEstado.Trim().Equals(nome, StringComparison.OrdinalIgnoreCase)
                        && item.PaisId == paisSelecionadoId
                        && item.Id != id, "Já existe um estado com este nome cadastrado para este país."))
                    {
                        txtNome.Focus();
                        return;
                    }

                    string uf = txtUF.Text.Trim().ToUpper();
                    if (string.IsNullOrWhiteSpace(uf) || uf.Length != 2)
                    {
                        MessageBox.Show("Informe a sigla UF com 2 letras.");
                        return;
                    }

                    DateTime dataCriacao = id == 0 ? DateTime.Now : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));
                    DateTime dataModificacao = DateTime.Now;

                    Estado estado = new Estado
                    {
                        Id = id,
                        NomeEstado = nome,
                        UF = uf,
                        Ativo = !chkInativo.Checked,
                        DataCadastro = dataCriacao,
                        DataAlteracao = dataModificacao,

                        PaisId = paisSelecionadoId,

                        oPais = new Pais { Id = paisSelecionadoId }
                    };

                    await controller.Salvar(estado);
                    MessageBox.Show("Estado salvo com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar estado: " + ex.Message);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            oFrmConsultaPais.ModoSelecao = true;

            var resultado = oFrmConsultaPais.ShowDialog();

            if (resultado == DialogResult.OK && oFrmConsultaPais.PaisSelecionado != null)
            {
                txtPais.Text = oFrmConsultaPais.PaisSelecionado.NomePais;
                txtIdPais.Text = oFrmConsultaPais.PaisSelecionado.Id.ToString(); 
                paisSelecionadoId = oFrmConsultaPais.PaisSelecionado.Id;
            }
        }

        private async void txtIdPais_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdPais.Text))
            {
                txtPais.Text = "";
                paisSelecionadoId = -1;
                return;
            }

            if (!int.TryParse(txtIdPais.Text, out int id))
            {
                MessageBox.Show("O campo ID País deve conter apenas números.");
                txtPais.Text = "";
                paisSelecionadoId = -1;
                return;
            }

            var pais = await paisController.BuscarPorId(id);

            if (pais != null)
            {
                txtPais.Text = pais.NomePais;
                paisSelecionadoId = pais.Id;
            }
            else
            {
                MessageBox.Show("País não encontrado.");
                txtPais.Text = "";
                paisSelecionadoId = -1;
            }
        }
    }
}