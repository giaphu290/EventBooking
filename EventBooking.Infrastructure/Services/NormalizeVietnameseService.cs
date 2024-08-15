using EventBooking.Application.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Infrastructure.Services
{
    public class NormalizeVietnameseService : INormalizeVietnamese
    {
        public string NormalizeVietnamese(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return string.Concat(input.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
                .Normalize(NormalizationForm.FormC);
        }
    }
}
