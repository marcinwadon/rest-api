using System;
using System.Collections.Generic;

namespace RestAPI.Components.CQRS
{
    public class CommandBus : ICommandBus
    {
        private readonly Dictionary<Type, Action<ICommandMessage>> _handlers = new Dictionary<Type, Action<ICommandMessage>>();

        public void RegisterHandler<TCommand>(IHandler<TCommand> handler) where TCommand : class, ICommandMessage {
            var type = typeof (TCommand);
            if (_handlers.ContainsKey(type)) {
                throw new InvalidOperationException(string.Format("Handler exists for type {0}.", type));
            }

            _handlers[type] = command => handler.Handle((TCommand)command);
        }

        public void Handle(ICommandMessage command) {
            var type = command.GetType();

            foreach (var kvp in _handlers) {
                Console.WriteLine(kvp.Key);
            }

            if (!_handlers.ContainsKey(type)) {
                return;
            }

            var handler = _handlers[type];
            handler(command);
        }
    }
}
