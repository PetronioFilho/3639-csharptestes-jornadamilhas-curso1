using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Fact]
        public void RetornaOfertaValidaQuandoDadosValidos()
        {
            //Cenário
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(DateTime.Now.AddDays(10), DateTime.Now.AddDays(20));
            double preco = 1500.00;

            var validacao = true;

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

        [Fact]
        public void RetornaMensagemDeErroQuandoPrecoForNegativo()
        {
            //Arrange
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(DateTime.Now.AddDays(10), DateTime.Now.AddDays(20));
            double preco = -500.00;

            //Act
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Assert
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
        }
    }
}