using BuberDinner.Application.Common.Interface.Persistence;
using BuberDinner.Domain.MenuAggregate;

namespace BuberDinner.Infrastructure.Persistence.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly BuberDinnerDbContext _dbContext;

    public MenuRepository(BuberDinnerDbContext context)
    {
        _dbContext = context;
    }

    public void AddMenu(Menu menu)
    {
        _dbContext.Add(menu);
        _dbContext.SaveChanges();
    }
}
