using AutoMapper;
using web.api.todo.Models.DB;

namespace web.api.todo.Models.Mapper {

    public class UserMapper :Profile {

        public UserMapper() {
            CreateMap<ViewUser, Person>();
            CreateMap<Person, ViewUser>()
                .ForMember(
                    view => view.CustomName,
                    opt => opt.MapFrom(s => "Name:" + s.Name + " - Id:" + s.Id)
                );
        }
    }
}
