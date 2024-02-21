using BuberDinner.Domain.Menu;

namespace BuberDinner.Application.Common.Interface.Persistence;

public interface IMenuRepository
{
    void AddMenu(Menu menu);
}