namespace BuberDinner.Domain.Common.Models
{
    public abstract class ValueObject: IEquatable<ValueObject>
    {
        // 这个方法是返回一个IEnumerable<object>，这个方法是用来比较两个对象是否相等的
        public abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            var valueObject = (ValueObject)obj;
            // 判断两个对象是否相等
            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        // 定义==和!=操作符
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            // 如果对象不为空，返回对象的哈希值，否则返回0
            // 这里使用了异或操作符
            // 当两个对象相等时，hashcode也相
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public bool Equals(ValueObject? other)
        {
            return Equals((object?)other);
        }
    }
    public class Price : ValueObject
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        public Price(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}