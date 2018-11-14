using AutoMapper;
using Microsoft.Owin;
using Owin;
using System.Configuration;

using ef = MyTurn.Db;
using dto = MyTurn.Web.Models;

[assembly: OwinStartupAttribute(typeof(MyTurn.Web.Startup))]
namespace MyTurn.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
            Mapper.Initialize(c => {
                c.CreateMap<ef.Person, dto.Person>().ReverseMap();
            });
        }
    }
}
