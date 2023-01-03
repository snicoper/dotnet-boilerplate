using DotnetBoilerplate.Domain.Common;

namespace DotnetBoilerplate.Domain.Entities;

public class Person : BaseAuditableEntity
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }
}
