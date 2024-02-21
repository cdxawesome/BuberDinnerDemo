using BuberDinner.Application.Common.Interface.Persistence;
using BuberDinner.Domain.HostAggregate.ValueObjects;
using BuberDinner.Domain.Menu;
using BuberDinner.Domain.MenuAggregate.Entities;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    private readonly IMenuRepository _menuRepository;

    public CreateMenuCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<Menu>> Handle(
        CreateMenuCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Create Menu
        var menu=Menu.Create(request.Name,
                             request.Description,
                             HostId.Create(request.HostId),
                             request.Sections.Select(section=>MenuSection.Create(
                                section.Name,
                                section.Description,
                                section.Items.Select(item => MenuItem.Create(
                                    item.Name,
                                    item.Description)).ToList())).ToList());
        // Persist Menu
        _menuRepository.AddMenu(menu);
        // Return Menu
        return menu;
    }
}