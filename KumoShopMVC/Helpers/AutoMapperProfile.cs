using AutoMapper;
using KumoShopMVC.Data;
using KumoShopMVC.ViewModels;

namespace KumoShopMVC.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile() {
			CreateMap<RegisterVM, User>();
		}
	}
}
