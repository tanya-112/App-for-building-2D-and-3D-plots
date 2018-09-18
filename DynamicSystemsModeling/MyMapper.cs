using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSystemsModeling
{
    class MyMapper : nzy3D.Plot3D.Builder.Mapper
    {
        public override double f(double x, double y)
        {
            //метод не используется более по назначению, можно удалить (но и удалить ссылки на него)
            //return 10 * Math.Sin(x / 10) * Math.Cos(y / 20) * x;
            return -77777777;
        }
    }
}
