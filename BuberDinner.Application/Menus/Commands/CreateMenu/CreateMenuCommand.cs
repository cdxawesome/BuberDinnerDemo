using BuberDinner.Domain.Menu;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Menus.Commands.CreateMenu;

/*
关于createMenu的属性，在项目中创建了三个基本都是一样属性的类：
1. CreateMenuCommand
2. CreateMenuRequest
3. MenuAggregate
这样做是因为，在项目开发初期，可能会频繁的改动CreateMenu的属性。创建好了上述三个
类之后，不管如何改动，我们只需要考虑数据如何在不同层中传递和转换即可，API到Command
可以自由的变动，而Handler和Validation是不会变动的。(视频教程是这样说，但我还是没
理解，需要实践才能理解) 
*/

public record CreateMenuCommand(
    string HostId,
    string Name,
    string Description,
    List<MenuSectionCommand> Sections) : IRequest<ErrorOr<Menu>>;

public record MenuSectionCommand(
    string Name,
    string Description,
    List<MenuItemCommand> Items);

public record MenuItemCommand(
    string Name,
    string Description);

