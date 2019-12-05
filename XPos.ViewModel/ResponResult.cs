using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPos.ViewModel
{
    public class ResponResult
    {
        public ResponResult()
        {
            Success = true;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Entity { get; set; }
    }

    public class ResultOrder : ResponResult
    {
        public string Reference { get; set; }
    }
}
