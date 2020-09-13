using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prism;
using Prism.Ioc;

namespace MoneyTransferGuide.Services
{
    public class HttpOperationResult<T> where T : class
    {

        public List<T> TableResult { get; set; }

        public T Result { get; set; }

        public bool IsCancelled { get; set; }

        public bool SuccessResult { get; set; }


        public Exception Exception { get; set; }

        public HttpOperationResult()
        {
            SuccessResult = true;
            IsCancelled = false;
        }

        public void RegisterErrorState(Exception e)
        {
            SuccessResult = false;
            Exception = e;
            IsCancelled = (e is TaskCanceledException);
            TableResult = null;
            Result = null;
        }

    }
}
