using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Engine.Entities;

namespace Engine.Systems
{
    public class System
    {        
        public virtual void Update(int dt, ref List<string> componentEntityList){}
        //public virtual void Init() { }

        public System()
        {
            
        }
    }
}
