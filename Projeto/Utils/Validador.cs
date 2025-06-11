using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Utils
{
    public static class Validador
    {
        public static bool CampoObrigatorio(TextBox campo, string mensagem)
        {
            if (string.IsNullOrWhiteSpace(campo.Text))
            {
                MessageBox.Show(mensagem);
                campo.Focus();
                return false;
            }
            return true;
        }

        public static bool ValidarEmail(TextBox campo)
        {
            if (!string.IsNullOrWhiteSpace(campo.Text) &&
                !Regex.IsMatch(campo.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("E-mail inválido.");
                campo.Focus();
                return false;
            }
            return true;
        }

        public static bool ValidarNumerico(TextBox campo, string mensagem)
        {
            if (!int.TryParse(campo.Text, out _))
            {
                MessageBox.Show(mensagem);
                campo.Focus();
                return false;
            }
            return true;
        }

        public static bool ValidarData(DateTimePicker campo, string mensagem)
        {
            if (campo.Value == DateTime.MinValue)
            {
                MessageBox.Show(mensagem);
                campo.Focus();
                return false;
            }
            return true;
        }

        public static bool ValidarCPF(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            if (new string(cpf[0], 11) == cpf)
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }


        public static bool ValidarCNPJ(string cnpj)
        {
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            if (cnpj.Length != 14)
                return false;

            if (new string(cnpj[0], 14) == cnpj)
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            tempCnpj += resto.ToString();

            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digitoVerificador = tempCnpj.Substring(12, 1) + resto.ToString();

            return cnpj.EndsWith(digitoVerificador);
        }


        public static bool ComboBoxObrigatorio(ComboBox combo, string mensagem)
        {
            if (combo.SelectedIndex < 0 || combo.SelectedItem == null)
            {
                MessageBox.Show(mensagem);
                combo.Focus();
                return false;
            }
            return true;
        }

    }
}
