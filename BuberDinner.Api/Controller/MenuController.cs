using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Contracts.Menus;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controller;

[Route("hosts/{hostId}/menus")]
public class MenuController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public MenuController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenu(CreateMenuRequest request,
                                    string hostId)
    {
        var command=_mapper.Map<CreateMenuCommand>((request,hostId));
        var createMenuResult=await _mediator.Send(command);
        return Ok(request);
    }
}