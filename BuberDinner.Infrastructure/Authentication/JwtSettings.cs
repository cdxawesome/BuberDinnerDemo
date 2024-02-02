using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Infrastructure.Authentication
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        // 这个表示初始化器，它允许我们在构造函数中初始化属性，初始化为null的属性必须使用！标记
        public string Secret { get; init; } = null!;
        public string Issuer { get; init; }= null!;
        public string Audience { get; init; } = null!;
        public int ExpiryMinutes { get; init; }
    }
}
