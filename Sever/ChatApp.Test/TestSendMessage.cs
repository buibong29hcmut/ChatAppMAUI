using ChatApp.Application.Cores.Commands;
using ChatApp.Application.Requests.Messages.Commands;
using ChatApp.Share.Wrappers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Test
{
    [TestClass]   
    public class TestSendMessage:BaseTest
    {
        [TestMethod]
        public void SendMessage()
        {
            var service = serviceCollectionContainer.ServiceCollection();
            var commandbus = service.BuildServiceProvider().GetRequiredService<ICommandBus>();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var resultSendMessage = commandbus.Send<Result<Unit>>(new CreateMessageCommand()
            {   
                Content="Test Mesage",
                FromUserId= new Guid("838290d2-46c0-4afe-9710-85de03a53a31"),
                ConversationId= new Guid("04f32922-ab5d-4e87-9b0c-1e5c1c23783e"),
                SendTime=DateTime.Now
            });
            Assert.AreEqual(resultSendMessage.Result.Data != null, true);
        }
    }
}
