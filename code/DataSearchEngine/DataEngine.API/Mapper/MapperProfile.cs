using AutoMapper;
using Common;
using Common.Events;
using DataEngine.API.Contracts;

namespace DataEngine.API.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<CreatePersonRequest, Entities.Person>();
            CreateMap<Entities.Person, StorePersonCreate>();
            CreateMap<Entities.Person, StorePersonUpdate>();
            CreateMap<Entities.Person, StorePersonDelete>();
            CreateMap<StorePersonCreate, PersonCreatedEvent>();
            CreateMap<StorePersonUpdate, PersonUpdatedEvent>();
            CreateMap<StorePersonDelete, PersonDeletedEvent>();
        }
    }
}
