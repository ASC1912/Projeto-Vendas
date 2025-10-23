using MySql.Data.MySqlClient;
using Projeto.Models;
using System;

namespace Projeto.DAO
{
    internal class DAOContasAPagar
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void SalvarManual(ContasAPagar conta)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query;

                query = @"
                    INSERT INTO ContasAPagar (
                        FornecedorId, Descricao, DataEmissao, DataVencimento, ValorVencimento,
                        Status, Ativo, DataCadastro, DataAlteracao
                        // Os campos Compra..._FK ficarão NULL por padrão
                    ) VALUES (
                        @FornecedorId, @Descricao, @DataEmissao, @DataVencimento, @ValorVencimento,
                        @Status, @Ativo, @DataCadastro, @DataAlteracao
                    )";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FornecedorId", conta.FornecedorId);
                    cmd.Parameters.AddWithValue("@Descricao", conta.Descricao);
                    cmd.Parameters.AddWithValue("@DataEmissao", conta.DataEmissao);
                    cmd.Parameters.AddWithValue("@DataVencimento", conta.DataVencimento);
                    cmd.Parameters.AddWithValue("@ValorVencimento", conta.ValorVencimento);
                    cmd.Parameters.AddWithValue("@Status", "Aberta");
                    cmd.Parameters.AddWithValue("@Ativo", true);
                    cmd.Parameters.AddWithValue("@DataCadastro", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // (Aqui no futuro vamos adicionar os métodos Listar(), Pagar(), etc.)
    }
}