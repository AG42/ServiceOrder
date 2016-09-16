﻿using Order.Common;
using Order.Common.Error;
using Order.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Order.BusinessLayer
{
    public static class InputValidation
    {
        public static bool ValidateCompanyCode(string companyCode, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(companyCode))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CompanyCodeRequiredMessage ));
            }            

            return response.ErrorInfo.Any();
        }
        public static bool ValidateCustomerName(string customerName, BaseResponse response)
        {
            if (customerName.Length > 35)
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CustomerNameLengthErrorMessage));
            }

            return response.ErrorInfo.Any();
        }

        public static bool ValidateCustomerCode(string customerCode, BaseResponse response)
        {
            if (string.IsNullOrWhiteSpace(customerCode))
            {
                response.ErrorInfo.Add(new ErrorInfo(Constants.CusotmerCodeIsRequiredMessage));
            }

            return response.ErrorInfo.Any();
        }
    }
}
