using System.Threading.Channels;
using AutoMapper;
using Common;
using DataEngine.API.Contracts;
using DataEngine.API.Database;
using DataEngine.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using static StackExchange.Redis.Role;

namespace DataEngine.API.Endpoints;

public static class PersonEndpoints
{
    public static void MapPersonEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("persons", async (
            CreatePersonRequest request,
            ApplicationDbContext context,
            Channel<StorePersonCreate> channel,
            IMapper mapper,
            CancellationToken ct) =>
        {
            var item = mapper.Map<Person>(request);
            context.Add(item);
            await context.SaveChangesAsync(ct);
            var storePerson = mapper.Map<StorePersonCreate>(item);
            await channel.Writer.WriteAsync(storePerson, ct);
            return Results.Ok(item);
        });

        app.MapGet("persons", async (
            ApplicationDbContext context,
            CancellationToken ct,
            int page = 1,
            int pageSize = 10) =>
        {
            var items = await context.Persons
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return Results.Ok(items);
        });

        app.MapGet("persons/{id}", async (
            Guid id,
            ApplicationDbContext context,
            IDistributedCache cache,
            CancellationToken ct) =>
        {
            var item = await cache.GetAsync($"persons-{id}",
                async token =>
                {
                    var item = await context.Persons
                        .AsNoTracking()
                        .FirstOrDefaultAsync(p => p.Id == id, token);

                    return item;
                },
                CacheOptions.DefaultExpiration,
                ct);

            return item is null ? Results.NotFound() : Results.Ok(item);
        });

        app.MapPut("persons/{id}", async (
            Guid id,
            UpdatePersonRequest request,
            ApplicationDbContext context,
            IDistributedCache cache,
            Channel<StorePersonUpdate> channel,
            IMapper mapper,
            CancellationToken ct) =>
        {
            if (id != request.Id)
            {
                return Results.BadRequest("The ID in the URL does not match the ID in the body");
            }

            var item = await context.Persons
                .FirstOrDefaultAsync(p => p.Id == id, ct);

            if (item is null)
            {
                return Results.NotFound();
            }

            item.FirstName = request.FirstName;
            item.LastName = request.LastName;
            item.Email = request.Email;
            item.Country = request.Country;
            item.Dob = request.Dob;
            item.Bio = request.Bio;
            item.ProfileUrl = request.ProfileUrl;

            await context.SaveChangesAsync(ct);

            await cache.RemoveAsync($"persons-{id}", ct);
            var storePerson = mapper.Map<StorePersonUpdate>(item);
            await channel.Writer.WriteAsync(storePerson, ct);
            return Results.NoContent();
        });

        app.MapDelete("persons/{id}", async (
            Guid id,
            ApplicationDbContext context,
            IDistributedCache cache,
            Channel<StorePersonDelete> channel,
            IMapper mapper,
            CancellationToken ct) =>
        {
            var item = await context.Persons
                .FirstOrDefaultAsync(p => p.Id == id, ct);

            if (item is null)
            {
                return Results.NotFound();
            }

            context.Remove(item);

            await context.SaveChangesAsync(ct);

            await cache.RemoveAsync($"persons-{id}", ct);
            var storePerson = mapper.Map<StorePersonDelete>(item);
            await channel.Writer.WriteAsync(storePerson, ct);
            return Results.NoContent();
        });

        app.MapGet("persons/count", async (
            ApplicationDbContext context,
            CancellationToken ct) =>
        {
            return Results.Ok(await context.Persons.CountAsync());
        });
    }
}
