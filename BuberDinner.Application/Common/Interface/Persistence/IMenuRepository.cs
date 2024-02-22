using BuberDinner.Domain.MenuAggregate;

namespace BuberDinner.Application.Common.Interface.Persistence;

public interface IMenuRepository
{
    void AddMenu(Menu menu);
}