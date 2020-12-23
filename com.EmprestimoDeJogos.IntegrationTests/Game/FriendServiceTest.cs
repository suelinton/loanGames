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

namespace com.EmprestimoDeJogos.IntegrationTests.Friend
{
    public class FriendServiceTest : StartupDI
    {
        private readonly IFriendService _friendService;

        public FriendServiceTest()
        {
            _friendService = ServiceProvider.GetService<IFriendService>();
        }

        [Fact]
        public void TestGetFriends()
        {
            var result = _friendService.GetFriends();
           
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void TestCreateFriend()
        {
            var objTest = new FriendEntity() { Name = "Name test" };

            var result = _friendService.CreateFriend(objTest);

            Assert.NotNull(result);
            Assert.True(result.Id != default);
            Assert.Equal(result.Name,objTest.Name);
        }

        [Fact]
        public void TestGetFriend()
        {
            var id = 2;
            var result = _friendService.GetFriend(id);

            Assert.NotNull(result);
            Assert.Equal(result.Id, id);
        }

        [Fact]
        public void TestUpdateFriend()
        {
            var id = 2;
            var objTest = _friendService.GetFriend(id);
            objTest.Name = "Name test updated";

            var success = _friendService.Update(objTest);
            Assert.True(success);
        }

        [Fact]
        public void TestDeleteFriends()
        {
            var id = 2;
            var objTest = _friendService.GetFriend(id);

            var success= _friendService.Delete(objTest);

            Assert.True(success);
        }
    }
}
