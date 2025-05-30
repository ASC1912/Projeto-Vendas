﻿using MySqlX.XDevAPI;
using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Projeto.Views
{
    public partial class frmConsultaCidade : Projeto.frmBaseConsulta
    {
        private CidadeController controller = new CidadeController();
        public bool ModoSelecao { get; set; } = false;
        internal Cidade CidadeSelecionado { get; private set; }

        public frmConsultaCidade() : base()
        {
            InitializeComponent();
        }

        private void frmConsultaCidade_Load(object sender, EventArgs e)
        {
            btnSelecionar.Visible = ModoSelecao;
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroCidade formCadastroCidade = new frmCadastroCidade();
            formCadastroCidade.ShowDialog();
        }

        private void CarregarCidades()
        {
            try
            {
                listView1.Items.Clear();
                List<Cidade> cidades = controller.ListarCidade();

                foreach (var cidade in cidades)
                {
                    ListViewItem item = new ListViewItem(cidade.Id.ToString());
                    item.SubItems.Add(cidade.Nome);
                    item.SubItems.Add(cidade.EstadoNome);
                    item.SubItems.Add(cidade.Status ? "Ativo" : "Inativo");

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar cidades: " + ex.Message);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.Clear();
                string texto = txtPesquisar.Text.Trim();

                if (string.IsNullOrEmpty(texto))
                {
                    CarregarCidades();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Cidade cidade = new CidadeController().BuscarPorId(id);

                    if (cidade != null)
                    {
                        ListViewItem item = new ListViewItem(cidade.Id.ToString());
                        item.SubItems.Add(cidade.Nome);
                        item.SubItems.Add(cidade.EstadoNome);
                        item.SubItems.Add(cidade.Status ? "Ativo" : "Inativo");

                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Cidade não encontrada.");
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


        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Cidade cidade = controller.BuscarPorId(id);

                if (cidade != null)
                {
                    var formCadastro = new frmCadastroCidade();
                    formCadastro.CarregarCidade(
                        cidade.Id,
                        cidade.Nome,
                        cidade.EstadoNome,
                        cidade.Status,
                        cidade.DataCriacao,
                        cidade.DataModificacao
                    );

                    formCadastro.FormClosed += (s, args) => CarregarCidades();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Cidade não encontrada.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um item para editar.");
            }
        }


        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir essa Cidade?", "Confirmação", MessageBoxButtons.YesNo);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        controller.Excluir(id);
                        listView1.Items.Remove(itemSelecionado);
                        MessageBox.Show("Cidade excluída com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma cidade para excluir.");
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                string nome = itemSelecionado.SubItems[1].Text;
                string estado = itemSelecionado.SubItems[2].Text;

                CidadeSelecionado = new Cidade
                {
                    Id = id,
                    Nome = nome,
                    EstadoNome = estado,
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um nome.");
            }
        }
    }
}
