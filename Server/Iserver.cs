using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface Iserver
    {
        void Register<T>(T type) where T : class;

    }
}
