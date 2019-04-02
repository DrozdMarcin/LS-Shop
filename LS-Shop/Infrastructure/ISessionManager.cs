using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Shop.Infrastructure
{
    interface ISessionManager
    { 
        //pobranie elementu
        T Get<T>(string key);
        //ustawienie elementu
        void Set<T>(String name, T value);
        // usuwanie elementu
        void Abandon();
        T TryGet<T>(string key);
    }
}
