using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Common.Constants
{
    public class ResponseCodeConstants
    {
        public const string NOT_FOUND = "Not found!";
        public const string SUCCESS = "Success!";
        public const string FAILED = "Failed!";
        public const string EXISTED = "Existed!";
        public const string DUPLICATE = "Duplicate!";
        public const string INTERNAL_SERVER_ERROR = "Internal server error!";
        public const string INVALID_INPUT = "Invalid input!";
        public const string UNAUTHORIZED = "Unauthorized!";
        public const string BADREQUEST = "Bad request!";
        public const string ERROR = "Error!";
        public const string UNAUTHENTICATED = "Unauthenticated!";
        public const string TOKEN_EXPIRED = "The token has expired.";
        public const string TOKEN_INVALID = "The token is invalid.";
        public const string UNKNOWN = "Oops! Something went wrong, please try again later.";
        public const string NOT_UNIQUE = "The resource already exists, please try another.";
        public const string VALIDATED = "Validated.";
    }
    public static class ErrorMessages
    {
        public const string NOT_FOUND = "Không tìm thấy {0}.";
        public const string SUCCESS = "Thành công!";
        public const string FAILED = "Thất bại!";
        public const string EXISTED = "{0} đã tồn tại.";
        public const string DUPLICATE = "{0} bị trùng lặp.";
        public const string INTERNAL_SERVER_ERROR = "Lỗi máy chủ nội bộ!";
        public const string INVALID_INPUT = "Dữ liệu đầu vào không hợp lệ!";
        public const string UNAUTHORIZED = "Không có quyền truy cập!";
        public const string BADREQUEST = "Yêu cầu không hợp lệ!";
        public const string ERROR = "Lỗi!";
    }
}
