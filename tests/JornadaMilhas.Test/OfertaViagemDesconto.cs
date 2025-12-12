using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemDesconto
    {
        [Fact]
        public void RetornaPrecoAtualizadoQuandoAplicadoDesconto()
        {
            //Arrange
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(DateTime.Now.AddDays(10), DateTime.Now.AddDays(20));
            double precoOriginal = 2000.00;
            double desconto = 15.0; // 15%
            double precoComDesconto = precoOriginal - desconto;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

            //Act
            oferta.Desconto = desconto;

            //Assert
            Assert.Equal(precoComDesconto, oferta.Preco);
        }

        [Fact]
        public void RetornaDescontoMaximoQuandoValorDescontoMaiorQuePreco()
        {
            //Arrange
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new Periodo(DateTime.Now.AddDays(10), DateTime.Now.AddDays(20));
            double precoOriginal = 100.00;
            double desconto = 120.00; // 15%
            double precoComDesconto = 30;
            OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);

            //Act
            oferta.Desconto = desconto;

            //Assert
            Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
        }
    }
}
