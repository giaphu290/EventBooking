using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Common.Behaviors
{
    public class ValidationErrorBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Tạo danh sách để chứa các kết quả lỗi
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(request, serviceProvider: null, items: null);

            // Kiểm tra tính hợp lệ của đối tượng yêu cầu
            if (!Validator.TryValidateObject(request, validationContext, validationResults, validateAllProperties: true))
            {
                // Ném ra ValidationException nếu có lỗi
                var errors = validationResults.Select(vr => vr.ErrorMessage).ToList();
                throw new ValidationException(string.Join(", ", errors));
            }

            // Nếu không có lỗi, tiếp tục với delegate tiếp theo
            return await next();
        }
    }
}
