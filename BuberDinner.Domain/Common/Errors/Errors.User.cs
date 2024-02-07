using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            // 这个是自定义的错误类型，可以手动抛出
            public static Error DuplicateEmail => Error.Conflict(
                code: "User.DuplicateEmail",
                description: "此邮件已存在");
        }
    }
}
