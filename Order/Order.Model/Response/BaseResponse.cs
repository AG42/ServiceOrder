﻿using Order.Common.Enum;
using Order.Common.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Model.Response
{
    public class BaseResponse
    {
        private readonly List<ErrorInfo> _errorInfo = new List<ErrorInfo>();
        public List<ErrorInfo> ErrorInfo
        {
            get { return _errorInfo; }
        }

        public string SuccessMessage { get; set; }

        public ResponseStatus Status
        {
            get
            {
                if (ErrorInfo.Any())
                    return ResponseStatus.Failure;
                return ResponseStatus.Success;
            }
        }
    }
}
