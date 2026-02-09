namespace APIFinanceira.Models
{
    public class Transacao
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public enum TipoTransacao
        {
            Entrada = 1,
            Saida = 2
        }
        public TipoTransacao Tipo { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }

    public interface ITransacaoRepository
    {
        void Adicionar(Transacao transacao);
    }

    public class TransacaoService
    {
        private readonly ITransacaoRepository _repository;

        public TransacaoService(ITransacaoRepository repository)
        {
            _repository = repository;
        }

        public void CriarTransacao(Transacao transacao)
        {
            if(transacao.Valor <= 0)
            {
                throw new ArgumentException("O valor da transação deve ser maior que zero.");
            }

            _repository.Adicionar(transacao);
        }
    }
}