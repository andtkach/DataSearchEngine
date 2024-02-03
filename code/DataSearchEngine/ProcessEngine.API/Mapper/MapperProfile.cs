using AutoMapper;
using Common.Events;
using SearchEngine.Domain;

namespace ProcessEngine.API.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<PersonCreatedEvent, Person>();
            CreateMap<PersonUpdatedEvent, Person>();
        }
    }
}
