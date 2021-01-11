using HMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Extension;

namespace HMS.BAL.Helper
{
    public class UnityHelper:UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IHotelsRepository, HotelsRepository>();
        }
    }
}
