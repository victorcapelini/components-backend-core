using FluentAssertions;
using Optsol.Components.Infra.Data;
using Xunit;

namespace Optsol.Components.Test.Unit.Infra.Data
{
    public class RequestSearchSpec
    {
        [Fact]
        public void Deve_Retornar_Pagina_Hum_Se_Valor_Informado_Igual_Zero()
        {
            //Given
            var requestSearch = new RequestSearch();

            //When
            requestSearch.Page = 0;

            //Then
            requestSearch.Page.Should().Be(1);
        }

        [Fact]
        public void Deve_Retornar_Pagina_Hum()
        {
            //Given
            var requestSearch = new RequestSearch();

            //When
            requestSearch.Page = 1;

            //Then
            requestSearch.Page.Should().Be(1);
        }

        [Fact]
        public void Deve_Retornar_Pagina_Dois()
        {
            //Given
            var requestSearch = new RequestSearch();

            //When
            requestSearch.Page = 2;

            //Then
            requestSearch.Page.Should().Be(2);
        }
    }
}
