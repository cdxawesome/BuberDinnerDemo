using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Contracts.Menus;
using BuberDinner.Domain.MenuAggregate;
using MenuSection = BuberDinner.Domain.MenuAggregate.Entities.MenuSection;
using MenuItem = BuberDinner.Domain.MenuAggregate.Entities.MenuItem;
using Mapster;


namespace BuberDinner.Api.Common.Mapping;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        /*
        需要注意的点是，在配置对象关系映射的时候，属性名相同且类型相同的情况下，
        Mapster会自动进行映射，不需要手动配置。但是如果属性名不同或者类型不同，
        需要手动配置映射关系。
        */
        config.NewConfig<(CreateMenuRequest request, string hostId), CreateMenuCommand>()
            .Map(dest => dest.HostId, src => src.hostId)
            .Map(dest => dest, src => src.request); // 剩下的参数mapping给request
        config.NewConfig<Menu, MenuResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.HostId, src => src.HostId.Value)
            .Map(dest => dest.DinnerIds, src => src.DinnerIds.Select(d => d.Value))
            .Map(dest => dest.MenuReviewIds, src => src.MenuReviewIds.Select(m => m.Value));
        config.NewConfig<MenuSection, MenuSectionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
        config.NewConfig<MenuItem, MenuItemResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}