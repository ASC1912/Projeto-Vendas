using Projeto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Services
{
    /// <summary>
    /// Define um "contrato" para qualquer serviço que gerencie a entidade Grupo.
    /// Qualquer classe que implementar esta interface será obrigada a ter esses métodos.
    /// </summary>
    public interface IGrupoService
    {
        Task Salvar(Grupo grupo);
        Task<Grupo> BuscarPorId(int id);
        Task<List<Grupo>> ListarGrupos();
        Task Excluir(int id);
    }
}