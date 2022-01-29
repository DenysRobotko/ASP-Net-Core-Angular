using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace PhotoAlbum
{
    /// <summary>
    /// Use for mapping models and DTOs
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Constructor, that mapps models and DTOs
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Item, ItemDTO>();
            CreateMap<ItemDTO, Item>();

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
