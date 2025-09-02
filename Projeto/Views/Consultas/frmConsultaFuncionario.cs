using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views
{
    public partial class frmConsultaFuncionario : Projeto.frmBaseConsulta
    {
        private FuncionarioController controller = new FuncionarioController();
        frmCadastroFuncionario oFrmCadastroFuncionario;


        public frmConsultaFuncionario() : base()
        {
            InitializeComponent();
            this.Shown += frmConsultaFuncionario_Shown;
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroFuncionario = (frmCadastroFuncionario)obj;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (FuncionarioController)ctrl;
            }
        }


        private async void btnIncluir_Click(object sender, EventArgs e)
        {
            oFrmCadastroFuncionario.modoEdicao = false;
            oFrmCadastroFuncionario.modoExclusao = false;
            oFrmCadastroFuncionario.FormClosed += async (s, args) => await CarregarFuncionarios();
            oFrmCadastroFuncionario.ShowDialog();

            /*
            frmCadastroFuncionario formCadastroFuncionario = new frmCadastroFuncionario();
            formCadastroFuncionario.FormClosed += async (s, args) => await CarregarFuncionarios();
            formCadastroFuncionario.ShowDialog();
            */
        }

        private async Task CarregarFuncionarios()
        {
            try
            {
                listView1.Items.Clear();
                List<Funcionario> funcionarios = await controller.ListarFuncionarios();

                foreach (var funcionario in funcionarios)
                {
                    ListViewItem item = new ListViewItem(funcionario.Id.ToString());
                    item.SubItems.Add(funcionario.Tipo);
                    item.SubItems.Add(funcionario.Nome);
                    item.SubItems.Add(funcionario.Apelido);
                    item.SubItems.Add(funcionario.Genero);
                    item.SubItems.Add(funcionario.Endereco);
                    item.SubItems.Add(funcionario.NumeroEndereco?.ToString() ?? "0");
                    item.SubItems.Add(funcionario.Bairro);
                    item.SubItems.Add(funcionario.Complemento);
                    item.SubItems.Add(funcionario.CEP);
                    item.SubItems.Add(funcionario.NomeCidade ?? "-"); 
                    item.SubItems.Add(funcionario.Email);
                    item.SubItems.Add(funcionario.Telefone);
                    item.SubItems.Add(funcionario.Matricula);
                    item.SubItems.Add(funcionario.Cargo);
                    item.SubItems.Add(funcionario.Salario.ToString("F2"));
                    item.SubItems.Add(funcionario.CPF_CNPJ);
                    item.SubItems.Add(funcionario.Rg);
                    item.SubItems.Add(funcionario.DataAdmissao?.ToShortDateString() ?? "");
                    item.SubItems.Add(funcionario.DataDemissao?.ToShortDateString() ?? "");
                    item.SubItems.Add(funcionario.DataNascimento?.ToShortDateString() ?? "");
                    item.SubItems.Add(funcionario.Ativo ? "Ativo" : "Inativo");

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar funcionarios: " + ex.Message);
            }
        }

        private async void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.Clear();
                string texto = txtPesquisar.Text.Trim();

                if (string.IsNullOrEmpty(texto))
                {
                    await CarregarFuncionarios();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Funcionario funcionario = await controller.BuscarPorId(id);

                    if (funcionario != null)
                    {
                        ListViewItem item = new ListViewItem(funcionario.Id.ToString());
                        item.SubItems.Add(funcionario.Tipo);
                        item.SubItems.Add(funcionario.Nome);
                        item.SubItems.Add(funcionario.Apelido);
                        item.SubItems.Add(funcionario.Genero);
                        item.SubItems.Add(funcionario.Endereco);
                        item.SubItems.Add(funcionario.NumeroEndereco?.ToString() ?? "0");
                        item.SubItems.Add(funcionario.Bairro);
                        item.SubItems.Add(funcionario.Complemento);
                        item.SubItems.Add(funcionario.CEP);
                        item.SubItems.Add(funcionario.NomeCidade ?? "-");
                        item.SubItems.Add(funcionario.Email);
                        item.SubItems.Add(funcionario.Telefone);
                        item.SubItems.Add(funcionario.Matricula);
                        item.SubItems.Add(funcionario.Cargo);
                        item.SubItems.Add(funcionario.Salario.ToString("F2"));
                        item.SubItems.Add(funcionario.CPF_CNPJ);
                        item.SubItems.Add(funcionario.Rg);
                        item.SubItems.Add(funcionario.DataAdmissao?.ToShortDateString() ?? "");
                        item.SubItems.Add(funcionario.DataDemissao?.ToShortDateString() ?? "");
                        item.SubItems.Add(funcionario.DataNascimento?.ToShortDateString() ?? "");
                        item.SubItems.Add(funcionario.Ativo ? "Ativo" : "Inativo");
                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Funcionário não encontrado.");
                    }
                }
                else
                {
                    MessageBox.Show("Digite um ID válido (número inteiro).");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar: " + ex.Message);
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Funcionario funcionario = await controller.BuscarPorId(id);

                if (funcionario != null)
                {
                    oFrmCadastroFuncionario.modoEdicao = true;
                    oFrmCadastroFuncionario.modoExclusao = false;
                    oFrmCadastroFuncionario.CarregarFuncionario(
                        funcionario.Id, funcionario.Nome, funcionario.Apelido, funcionario.CPF_CNPJ, funcionario.Telefone, funcionario.Email,
                        funcionario.Endereco, funcionario.NumeroEndereco ?? 0, funcionario.Bairro, funcionario.Complemento, funcionario.CEP,
                        funcionario.Cargo, funcionario.Salario, funcionario.Matricula, funcionario.Genero, funcionario.Tipo,
                        funcionario.NomeCidade,
                        funcionario.CidadeId ?? 0,
                        funcionario.Ativo,
                        funcionario.DataAdmissao, funcionario.DataDemissao, funcionario.DataNascimento, funcionario.Rg,
                        funcionario.DataCadastro, funcionario.DataAlteracao
                    );
                    oFrmCadastroFuncionario.FormClosed += async (s, args) => await CarregarFuncionarios();
                    oFrmCadastroFuncionario.ShowDialog();


                    /*
                    var formCadastro = new frmCadastroFuncionario();
                    formCadastro.modoEdicao = true;
                    formCadastro.CarregarFuncionario(
                        funcionario.Id, funcionario.Nome, funcionario.Apelido, funcionario.CPF_CNPJ, funcionario.Telefone, funcionario.Email,
                        funcionario.Endereco, funcionario.NumeroEndereco ?? 0, funcionario.Bairro, funcionario.Complemento, funcionario.CEP,
                        funcionario.Cargo, funcionario.Salario, funcionario.Matricula, funcionario.Genero, funcionario.Tipo,
                        funcionario.NomeCidade, 
                        funcionario.CidadeId ?? 0,
                        funcionario.Ativo,
                        funcionario.DataAdmissao, funcionario.DataDemissao, funcionario.DataNascimento, funcionario.Rg,
                        funcionario.DataCadastro, funcionario.DataAlteracao
                    );

                    formCadastro.FormClosed += async (s, args) => await CarregarFuncionarios();
                    formCadastro.ShowDialog();
                    */
                }
                else
                {
                    MessageBox.Show("Funcionário não encontrado.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um item para editar.");
            }
        }

        private async void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Funcionario funcionario = await controller.BuscarPorId(id);

                if (funcionario != null)
                {
                    oFrmCadastroFuncionario.modoExclusao = true;
                    oFrmCadastroFuncionario.modoEdicao = false;
                    oFrmCadastroFuncionario.CarregarFuncionario(
                        funcionario.Id, funcionario.Nome, funcionario.Apelido, funcionario.CPF_CNPJ, funcionario.Telefone, funcionario.Email,
                        funcionario.Endereco, funcionario.NumeroEndereco ?? 0, funcionario.Bairro, funcionario.Complemento, funcionario.CEP,
                        funcionario.Cargo, funcionario.Salario, funcionario.Matricula, funcionario.Genero, funcionario.Tipo,
                        funcionario.NomeCidade,
                        funcionario.CidadeId ?? 0,
                        funcionario.Ativo,
                        funcionario.DataAdmissao, funcionario.DataDemissao, funcionario.DataNascimento, funcionario.Rg,
                        funcionario.DataCadastro, funcionario.DataAlteracao
                    );
                    oFrmCadastroFuncionario.FormClosed += async (s, args) => await CarregarFuncionarios();
                    oFrmCadastroFuncionario.ShowDialog();

                    /*
                    var formCadastro = new frmCadastroFuncionario { modoExclusao = true };
                    formCadastro.CarregarFuncionario(
                        funcionario.Id, funcionario.Nome, funcionario.Apelido, funcionario.CPF_CNPJ, funcionario.Telefone, funcionario.Email,
                        funcionario.Endereco, funcionario.NumeroEndereco ?? 0, funcionario.Bairro, funcionario.Complemento, funcionario.CEP,
                        funcionario.Cargo, funcionario.Salario, funcionario.Matricula, funcionario.Genero, funcionario.Tipo,
                        funcionario.NomeCidade, 
                        funcionario.CidadeId ?? 0,
                        funcionario.Ativo,
                        funcionario.DataAdmissao, funcionario.DataDemissao, funcionario.DataNascimento, funcionario.Rg,
                        funcionario.DataCadastro, funcionario.DataAlteracao
                    );
                    formCadastro.FormClosed += async (s, args) => await CarregarFuncionarios();
                    formCadastro.ShowDialog();
                    */
                }
                else
                {
                    MessageBox.Show("Funcionário não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um funcionário para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmConsultaFuncionario_Shown(object sender, EventArgs e)
        {
        }

        private async void frmConsultaFuncionario_Load(object sender, EventArgs e)
        {
            await CarregarFuncionarios();

            foreach (ColumnHeader column in listView1.Columns)
            {
                switch (column.Text)
                {
                    case "ID": column.Width = 40; break;
                    case "Tipo": column.Width = 60; break;
                    case "Nome": column.Width = 200; break;
                    case "Gênero": column.Width = 60; break;
                    case "Endereço": column.Width = 200; break;
                    case "Número": column.Width = 60; break;
                    case "Bairro": column.Width = 150; break;
                    case "Complemento": column.Width = 130; break;
                    case "CEP": column.Width = 80; break;
                    case "Cidade": column.Width = 150; break;
                    case "Cargo": column.Width = 130; break;
                    case "Telefone": column.Width = 120; break;
                    case "Email": column.Width = 200; break;
                    case "Status": column.Width = 60; break;
                    case "CPF/CNPJ": column.Width = 130; break;
                    case "DataNascimento": column.Width = 120; break;
                    default: column.Width = 100; break;
                }
            }
        }
    }
}