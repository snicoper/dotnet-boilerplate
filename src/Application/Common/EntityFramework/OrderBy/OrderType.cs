using System.ComponentModel.DataAnnotations;

namespace DotnetBoilerplate.Application.Common.EntityFramework.OrderBy;

public enum OrderType
{
    [Display(Name = "None")]
    None = 0,

    [Display(Name = "ASC")]
    Asc = 1,

    [Display(Name = "DESC")]
    Desc = 2
}
