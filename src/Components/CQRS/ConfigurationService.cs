using System;
using System.Reflection;

namespace RestAPI.Components.CQRS
{
    public class ConfigurationService
    {
        public static void RegisterHandlers(ICommandBus commandBus, Assembly entryAssembly)
        {
            foreach (Type type in entryAssembly.GetTypes()) {
                if (type.Namespace.EndsWith("Command.Handler")) {
                    var handler = Activator.CreateInstance(type);

                    Type[] handlerInterfaces = handler.GetType().GetInterfaces();
                    if (handlerInterfaces.Length == 1 && handlerInterfaces[0].GenericTypeArguments.Length == 1) {
                        Type commandType = handler.GetType().GetInterfaces()[0].GenericTypeArguments[0];

                        MethodInfo registerHandlerMethod = commandBus.GetType().GetMethod("RegisterHandler");
                        MethodInfo genericRegisterHandlerMethod = registerHandlerMethod.MakeGenericMethod(commandType);

                        genericRegisterHandlerMethod.Invoke(commandBus, new object[] { handler });
                    }
                }
            }
        }
    }
}
