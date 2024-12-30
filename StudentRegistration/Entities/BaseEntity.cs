using MassTransit;

namespace StudentRegistration.Entities;

public class BaseEntity
{
    public Guid Id { get; set; } = NewId.Next().ToGuid();
}