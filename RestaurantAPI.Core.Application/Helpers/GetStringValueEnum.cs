
namespace RestaurantAPI.Core.Application.Helpers
{
    public class GetStringValueEnum
    {
        public static string GetStringValue(Enum value)
        {
            var type = value.GetType();
            var field = type.GetField(value.ToString());
            var attribute = field.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            return attribute.Length > 0 ? attribute[0].Value : value.ToString();
        }
    }
}
