using AutoMapper;
using Diplom.BLL.DTO;
using Diplom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.BLL.Infrastructure
{
  public class BLLAutoMapperConfig
  {
    public static void RegisterMappings()
    {
      Mapper.CreateMap<RoleDTO, Role>();
      Mapper.CreateMap<Role, RoleDTO>();
      Mapper.CreateMap<UserDTO, User>();
      Mapper.CreateMap<User, UserDTO>();
      Mapper.CreateMap<DAL.Entities.Profile, ProfileDTO>();
      Mapper.CreateMap<ProfileDTO, DAL.Entities.Profile>();

    }
  }
}
