using EditorsCopilot.Foundation.OpenAI.Core.Controllers.Base;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Security;
using OpenAI_API;
using System;
using System.Collections.Generic;
using EditorsCopilot.Foundation.OpenAI.Core.Internal;

namespace EditorsCopilot.Foundation.OpenAI.Core.Abstract
{
    public abstract class OpenApiConnector
    {
        protected CustomOpenAIAPI Client { get; private set; }
        protected IOpenAiCredentials Options { get; private set; }

        public OpenApiConnector(IOpenAiCredentials credentials)
        {
            if (credentials == null)
                throw new ArgumentNullException(nameof(credentials));
            Options = credentials;
            Client = new CustomOpenAIAPI(new APIAuthentication(credentials.ApiToken));
        }

        #region IoC

        private readonly Dictionary<Type, object> _registry = new Dictionary<Type, object>();

        public void Registration<T>(Func<T> func, bool overrideIfExists = true) where T
            : BaseApiController<T>
        {
            Type key = typeof(T);
            if (!overrideIfExists && _registry.ContainsKey(key))
                return;

            _registry[key] = new Lazy<T>(func);
        }


        public T GetController<T>() where T
            : BaseApiController<T>
        {
            Type key = typeof(T);
            if (!_registry.ContainsKey(key))
                throw new KeyNotFoundException($"Controller {key.Name} is not registered.");

            return ((Lazy<T>)_registry[key]).Value;
        }

        #endregion
    }
}