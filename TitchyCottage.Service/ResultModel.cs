using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitchyCottage.Service
{
    public class ResultModel<T>
    {
        public bool success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
