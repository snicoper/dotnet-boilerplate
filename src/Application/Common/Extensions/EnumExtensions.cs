using System.ComponentModel.DataAnnotations;

namespace DotnetBoilerplate.Application.Common.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Obtener el valor de [Display(Name = "")] en un Enum.
    /// </summary>
    public static string EnumDisplayNameFor(this Enum value)
    {
        var enumType = value.GetType();
        var enumValue = Enum.GetName(enumType, value);

        if (enumValue is null)
        {
            return string.Empty;
        }

        var member = enumType.GetMember(enumValue)[0];
        var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
        var outString = ((DisplayAttribute)attrs[0]).Name;

        if (((DisplayAttribute)attrs[0]).ResourceType is not null)
        {
            outString = ((DisplayAttribute)attrs[0]).GetName();
        }

        return outString;
    }
}
