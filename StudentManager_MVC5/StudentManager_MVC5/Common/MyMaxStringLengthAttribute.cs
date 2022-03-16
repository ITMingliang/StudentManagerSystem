using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManager_MVC5.Common
{
    public class MyMaxStringLengthAttribute:ValidationAttribute
    {
        private readonly int _max;
        public MyMaxStringLengthAttribute(int max)
        {
            this._max = max;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value!=null)
            {
                string maxStr = value.ToString();
                if (_max< maxStr.Length)
                {
                    return new ValidationResult($"{maxStr}的长度超过了{_max}长度");
                }
            }
            return ValidationResult.Success;
        }
    }
}