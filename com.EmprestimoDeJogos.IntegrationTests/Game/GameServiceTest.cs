using com.EmprestimoDeJogos.Core.Interfaces;
using com.EmprestimoDeJogos.IntegrationTests._Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using com.EmprestimoDeJogos.Core.Entities;

namespace com.EmprestimoDeJogos.IntegrationTests.Game
{
    public class GameServiceTest : StartupDI
    {
        private readonly IGameService _gameService;

        public GameServiceTest()
        {
            _gameService = ServiceProvider.GetService<IGameService>();
        }

        [Fact]
        public void TestGetGames()
        {
            var result = _gameService.GetGames();
            Assert.True(result != null);
            Assert.True(result.Count() >= 1);
        }

        [Fact]
        public void TestCreateGame()
        {
            var objTest = new GameEntity() { Name = "Name test" };

            var result = _gameService.CreateGame(objTest);

            Assert.True(result != null);
            Assert.True(result.Id != default);
            Assert.True(result.Name == objTest.Name);
        }

        [Fact]
        public void TestGetGame()
        {
            var id = 1;
            var result = _gameService.GetGame(id);

            Assert.True(result != null);
            Assert.True(result.Id == id);
        }

        [Fact]
        public void TestUpdateGame()
        {
            var id = 1;
            var objTest = _gameService.GetGame(id);
            objTest.Name = "Name test updated";

            var result = _gameService.Update(objTest);
            Assert.Equal(result.Name, objTest.Name);
            Assert.Equal(result.Id, objTest.Id);
        }

        [Fact]
        public void TestDeleteGames()
        {
            var id = 1;
            var objTest = _gameService.GetGame(id);

            var idResult = _gameService.Delete(objTest);

            Assert.Equal(idResult, objTest.Id);
        }
    }
}
