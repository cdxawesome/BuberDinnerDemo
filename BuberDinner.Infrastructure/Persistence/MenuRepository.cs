using BuberDinner.Application.Common.Interface.Persistence;
using BuberDinner.Domain.MenuAggregate;

namespace BuberDinner.Infrastructure.Persistence;

public class MenuRepository : IMenuRepository
{
    private static readonly List<Menu> menus=new();
    public void AddMenu(Menu menu)
    {
        menus.Add(menu);
    }
}
