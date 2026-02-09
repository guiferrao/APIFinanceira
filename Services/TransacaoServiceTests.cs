using Xunit;
using Moq;
using System;
using APIFinanceira.Models;

namespace APIFinanceira.Services
{
    public class TransacaoServiceTests
    {
        [Fact]
        public void CriarTransacao_DeveSalvar_QuandoValorForPositivo()
        {
            var mockRepository = new Mock<ITransacaoRepository>();

            var service = new TransacaoService(mockRepository.Object);

            var transacaoValida = new Transacao
            {
                Id = Guid.NewGuid(),
                Valor = 100.00m,
                Data = DateTime.Now,
                Tipo = Transacao.TipoTransacao.Entrada,
                UsuarioId = 1
            };

            service.CriarTransacao(transacaoValida);

            mockRepository.Verify(r => r.Adicionar(It.IsAny<Transacao>()), Times.Once);
        }

        [Fact]
        public void CriarTransacao_DeveLancarExcecao_QuandoValorForNegativo()
        {
            var mockRepository = new Mock<ITransacaoRepository>();

            var service = new TransacaoService(mockRepository.Object);

            var transacaoInvalida = new Transacao
            {
                Id = Guid.NewGuid(),
                Valor = -50.00m,
                Data = DateTime.Now,
                Tipo = Transacao.TipoTransacao.Saida,
                UsuarioId = 1
            };

            var exception = Assert.Throws<ArgumentException>(() => service.CriarTransacao(transacaoInvalida));

            Assert.Equal("O valor da transação deve ser maior que zero.", exception.Message);

            mockRepository.Verify(r => r.Adicionar(It.IsAny<Transacao>()), Times.Never);
        }
    }
}

