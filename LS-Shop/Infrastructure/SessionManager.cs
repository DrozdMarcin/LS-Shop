using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace LS_Shop.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private HttpSessionState session;
        public SessionManager()
        {
            session = HttpContext.Current.Session;
        }

        //usuwanie sesji
        public void Abandon()
        {
            session.Abandon();
        }

        //Zwracanie sesji
        public T Get<T>(string key)
        {
            return (T)session[key];
        }

        //przypisanie wartosci sesji
        public void Set<T>(string name, T value)
        {
            session[name] = value;
        }

        public T TryGet<T>(string key)
        {
            try
            {
                return (T)session[key];
            }
            catch (NullReferenceException)
            {
                return default(T);
            }
        }
    }
}