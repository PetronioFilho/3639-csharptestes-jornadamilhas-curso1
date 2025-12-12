using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        //[Fact] // - Permite testar um método individualmente
        [Theory] // - Permite testar um método com vários cenários
        [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)]
        [InlineData("OrigemTeste", "DestinoTeste", "2024-01-10", "2024-01-15", 1500.00, true)]
        public void RetornaEhValidoDeAcordoComDadosEntrada(string origem, string destino, string dataIda, string dataVolta, double preco, bool validacao)
        {
            //Cenário
            Rota rota = new Rota(origem, destino);
            Periodo periodo = new Periodo(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            //Ação
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Validação
            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornamensagemDeErroDeRotaOuPeriodoInvalidoQuandoRotaNula()
        {
            //Cenário
            Rota rota = null;
            Periodo periodo = new Periodo(DateTime.Now.AddDays(10), DateTime.Now.AddDays(20));
            double preco = 1500.00;

            var validacao = true;

            //Ação
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Validação
            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemDeErroQuandoDataForInvalida()
        {
            //Cenário
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(DateTime.Now.AddDays(20), DateTime.Now.AddDays(5));
            double preco = 1500.00;

            var validacao = true;

            //Ação
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Validação
            Assert.Contains("Data de ida não pode ser maior que a data de volta.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        //[Fact]
        [Theory]
        [InlineData(0)]
        [InlineData(-100.00)]
        public void RetornaMensagemDeErroDePrecoInvalidoQuandoPrecoMenorQueZero(double preco)
        {
            //Arrange
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(DateTime.Now.AddDays(10), DateTime.Now.AddDays(20));

            //Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Assert
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
        }

        [Fact]
        public void RetornaTresErrosDeValidacaoQuandoRotaEPeriodoEPrecoSaoInvalidos()
        {
            //Arrange
            Rota rota = null;
            Periodo periodo = new Periodo(DateTime.Now.AddDays(20), DateTime.Now.AddDays(5));
            double preco = -500.00;
            int quantidadeErrosEsperada = 3;
            //Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Assert
            Assert.Equal(quantidadeErrosEsperada, oferta.Erros.Count());
        }
    }
}