using System.Configuration;

namespace Projeto.Utils
{
    public static class ConfigHelper
    {
        public static bool UseApi()
        {
            // Lê o valor do App.config. Se não encontrar, assume 'false' (MySQL) como padrão seguro.
            string useApiValue = ConfigurationManager.AppSettings["UseApi"];
            return bool.TryParse(useApiValue, out bool result) && result;
        }
    }
}