using Promax.NetCore.Domain.Entities.Abstraction;

namespace Promax.NetCore.Domain.Entities
{
    public class Teste : Entity
    {
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int Valor { get; set; }
    }
}
