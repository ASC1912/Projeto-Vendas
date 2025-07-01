using System;

namespace Projeto.Models
{
    internal class Veiculo
    {
        public int Id { get; set; }
        public int TransportadoraId { get; set; }
        public int? IdMarca { get; set; } 
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public int? AnoFabricacao { get; set; }
        public decimal? CapacidadeCargaKg { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public string NomeTransportadora { get; set; }
        public string NomeMarca { get; set; } 
    }
}