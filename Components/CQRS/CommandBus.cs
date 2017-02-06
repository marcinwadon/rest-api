using System;
using System.Collections.Generic;

namespace RestAPI.Components.CQRS
{
    public class CommandBus : ICommandBus
    {
        private readonly Dictionary<Type, Action<ICommandMessage>> _handlers = new Dictionary<Type, Action<ICommandMessage>>();

        public void registerHandler<TCommand>(IHandler<TCommand> handler) where TCommand : class, ICommandMessage {
            var type = typeof (TCommand);
            if (_handlers.ContainsKey(type)) {
                throw new InvalidOperationException(string.Format("Handler exists for type {0}.", type));
            }

            Console.WriteLine("register {0}", handler);

            _handlers[type] = command => handler.handle((TCommand)command);
        }

        public void handle(ICommandMessage command) {
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
