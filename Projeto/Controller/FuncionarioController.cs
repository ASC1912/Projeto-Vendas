﻿using Projeto.DAO;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class FuncionarioController
    {
        private DAOFuncionario dao = new DAOFuncionario();

        public void Salvar(Funcionario funcionario)
        {
            dao.Salvar(funcionario);
        }

        public Funcionario BuscarPorId(int id)
        {
            return dao.BuscarPorId(id);
        }

        public List<Funcionario> ListarFuncionario()
        {
            return dao.ListarFuncionarios();
        }

        public void Excluir(int id)
        {
            dao.Excluir(id);
        }
    }
}
