using BitCoinManagerModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoinManager.Services
{
    public interface ISessionHandler
    {
        T Get<T>(SessionKey key) where T : new();
        void Set(SessionKey key, object obj);

        bool GetUser(out BitCoinUser user);
        void SetUser(string user);
    }

    public class SessionHandler : ISessionHandler
    {
        private IHttpContextAccessor _contextAccessor;

        public SessionHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        private string JsonValue(SessionKey key)
        {
            return _contextAccessor.HttpContext.Session.GetString(key.ToString()); ;
        }

        private string JsonValue(string user)
        {
            return _contextAccessor.HttpContext.Request.Cookies[user];
        }

        public T Get<T>(SessionKey key) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(JsonValue(key) ?? string.Empty);
        }

        public void Set(SessionKey key, object obj)
        {
            _contextAccessor.HttpContext.Session.SetString(key.ToString(), JsonConvert.SerializeObject(obj));
        }

        public bool GetUser(out BitCoinUser user) 
        {
            user = JsonConvert.DeserializeObject<BitCoinUser>(JsonValue("user") ?? string.Empty);
            return user != null;
        }

        public void SetUser(string user)
        {
            _contextAccessor.HttpContext.Request.Cookies.Append(new KeyValuePair<string, string>("user", user));
        }
    }

    public enum SessionKey
    {
        user,
        order,
    }
}
