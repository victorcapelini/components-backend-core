using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using Optsol.Components.Application.Result;
using Optsol.Components.Service;
using Optsol.Components.Service.Response;
using Optsol.Components.Shared.Extensions;
using Optsol.Components.Test.Shared.Logger;
using Optsol.Components.Test.Utils.Application;
using Optsol.Components.Test.Utils.Data;
using Optsol.Components.Test.Utils.Service;
using Xunit;

namespace Optsol.Components.Test.Unit.Service
{
    public class ApiControllerBaseSpec
    {
        [Fact]
        public async Task Deve_Registrar_Logs_No_ApiController()
        {
            //Given
            var model = new TestViewModel();
            var insertViewModel = new InsertTestViewModel();
            var updateViewModel = new UpdateTestViewModel();

            var entity = new TestEntity(
                new NomeValueObject("Weslley", "Carneiro"),
                new EmailValueObject("weslley.carneiro@outlook.com"));

            model.Id = entity.Id;

            var logger = new XunitLogger<ApiControllerBase<TestEntity
                , TestViewModel
                , TestViewModel
                , InsertTestViewModel
                , UpdateTestViewModel>>();

            Mock<IMapper> mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<TestViewModel>(It.IsAny<TestEntity>())).Returns(model);
            mapperMock.Setup(mapper => mapper.Map<TestEntity>(It.IsAny<TestViewModel>())).Returns(entity);

            var mockApplicationService = new Mock<ITestServiceApplication>();
            var mockResponseFactory = new Mock<IResponseFactory>();

            var controller = new TestController(logger, mockApplicationService.Object, mockResponseFactory.Object);

            //When
            await controller.GetAllAsync();
            await controller.GetByIdAsync(model.Id);
            await controller.InsertAsync(insertViewModel);
            await controller.UpdateAsync(updateViewModel);
            await controller.DeleteAsync(model.Id);

            //Then
            var msgContructor = "Inicializando Controller Base<TestEntity, Guid>";
            var msgGetById = $"Método: GetByIdAsync({{ id:{ entity.Id } }})";
            var msgGetAllAsync = "Método: GetAllAsync() Retorno: IActionResult";
            var msgInsertAsync = $"Método: InsertAsync({{ viewModel:{ insertViewModel.ToJson() } }})";
            var msgUpdateAsync = $"Método: UpdateAsync({{ viewModel:{ updateViewModel.ToJson() } }})";
            var msgDeleteAsync = $"Método: DeleteAsync({{ id:{ entity.Id } }})";

            logger.Logs.Should().HaveCount(6);
            logger.Logs.Any(a => a.Equals(msgGetById)).Should().BeTrue();
            logger.Logs.Any(a => a.Equals(msgContructor)).Should().BeTrue();
            logger.Logs.Any(a => a.Equals(msgGetAllAsync)).Should().BeTrue();
            logger.Logs.Any(a => a.Equals(msgInsertAsync)).Should().BeTrue();
            logger.Logs.Any(a => a.Equals(msgUpdateAsync)).Should().BeTrue();
            logger.Logs.Any(a => a.Equals(msgDeleteAsync)).Should().BeTrue();
        }

        [Fact]
        public async Task Nao_Deve_Serializar_Campos_Da_Classe_Notifiable()
        {
            //Given
            var model = new TestViewModel();

            var entityList = new List<TestViewModel>();
            entityList.Add(new TestViewModel { Id = Guid.NewGuid(), Nome = "Weslley", Contato = "weslley.carneiro@outlook.com", Ativo = "Ativo" });
            entityList.Add(new TestViewModel { Id = Guid.NewGuid(), Nome = "Bruno", Contato = "weslley.carneiro@outlook.com", Ativo = "Ativo" });
            entityList.Add(new TestViewModel { Id = Guid.NewGuid(), Nome = "Souza", Contato = "weslley.carneiro@outlook.com", Ativo = "Ativo" });
            entityList.Add(new TestViewModel { Id = Guid.NewGuid(), Nome = "Carneiro", Contato = "weslley.carneiro@outlook.com", Ativo = "Ativo" });

            var serviceResult = new ServiceResultList<TestViewModel>(entityList);

            var responseList = new ResponseList<TestViewModel>(serviceResult.DataList, serviceResult.Valid, serviceResult.Notifications.Select(s => s.Message));

            var logger = new XunitLogger<ApiControllerBase<TestEntity
                , TestViewModel
                , TestViewModel
                , InsertTestViewModel
                , UpdateTestViewModel>>();

            Mock<IMapper> mapperMock = new Mock<IMapper>();

            var mockApplicationService = new Mock<ITestServiceApplication>();
            mockApplicationService.Setup(services => services.GetAllAsync()).ReturnsAsync(serviceResult);

            var mockResponseFactory = new Mock<IResponseFactory>();
            mockResponseFactory.Setup(response => response.Create(It.IsAny<ServiceResultList<TestViewModel>>())).Returns(responseList);

            var controller = new TestController(logger, mockApplicationService.Object, mockResponseFactory.Object);

            //When
            var actionResult = await controller.GetAllAsync();
            var actionResultJson = actionResult.ToJson();

            //Then
            actionResult.Should().NotBeNull();
            actionResultJson.Should().NotBeNullOrEmpty();
            actionResultJson.Should().NotContain(nameof(TestViewModel.Notifications));
        }
    }
}
