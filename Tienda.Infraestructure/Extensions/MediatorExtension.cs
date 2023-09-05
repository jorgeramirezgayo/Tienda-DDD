using MediatR;
using Tienda.Domain.SeedWork;
using Tienda.Infraestructure.Data;

namespace Tienda.Infraestructure.Extensions
{
    public static class DomainEventsDispatcher
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, TiendaDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
