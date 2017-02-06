using System;
using Xunit;
using Moq;

namespace RestAPI.Components.CQRS.Spec
{
    public class CommandBusTest
    {
        private ICommandBus commandBus;
        private ICommandMessage command;
        private Mock<IHandler<ICommandMessage>> handlerMock;
        private IHandler<ICommandMessage> handler;

        public CommandBusTest()
        {
            commandBus = new CommandBus();

            Mock<ICommandMessage> commandMock = new Mock<ICommandMessage>();
            commandMock.Setup(c => c.GetType()).Returns(typeof (ICommandMessage));
            command = commandMock.Object;

            handlerMock = new Mock<IHandler<ICommandMessage>>();
            handler = handlerMock.Object;
        }

        [Fact]
        public void ShouldHandleTheCommandByPreviouslyRegisteredHandler()
        {
            commandBus.RegisterHandler<ICommandMessage>(handler);
            commandBus.Handle(command);

            handlerMock.Verify(h => h.Handle(command), Times.Once);
        }

        [Fact]
        public void ShouldThrowAnInvalidOperationExceptionWhenRegisterHandlerMoreThanOnce()
        {
            commandBus.RegisterHandler<ICommandMessage>(handler);

            Assert.Throws<InvalidOperationException>(() => {
                commandBus.RegisterHandler<ICommandMessage>(handler);
            });
        }

        [Fact]
        public void ShouldNotThrowAnyExceptionWhenCommandHandlerWasNotRegistered()
        {
            var exception = Record.Exception(() => commandBus.Handle(command));

            Assert.Null(exception);
        }
    }
}
