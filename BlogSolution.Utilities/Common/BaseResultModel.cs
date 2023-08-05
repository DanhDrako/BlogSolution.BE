using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSolution.Utilities.Common
{
    public class BaseResultModel
    {
        public int Code { get; set; } = 0;
        public string Message { get; set; } = "";
        public bool IsSuccess { get; set; } = true;
        public Object? Data { get; set; }

    }
}
