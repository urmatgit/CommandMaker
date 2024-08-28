using FSH.WebApi.Domain.Common.Events;
namespace FSH.WebApi.Application.Catalog.Games;

public class CreateGameRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
	public string? Description { get; set; }
	public DateTime DateTime { get; set; }
    
}


// Important leftout the T for the translations !

public class CreateGameRequestValidator : CustomValidator<CreateGameRequest>
{ 
    public CreateGameRequestValidator(IReadRepository<Game> repository, IStringLocalizer<CreateGameRequestValidator> localizer) =>
    RuleFor(p => p.Name)
    .NotEmpty() 
    .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new GameByNameSpec(name), ct) is null) 
   .WithMessage((_, name) => "Game {0} already Exists.");
   
}

public class CreateGameRequestHandler : IRequestHandler<CreateGameRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Game> _repository;

    public CreateGameRequestHandler(IRepositoryWithEvents<Game> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateGameRequest request, CancellationToken cancellationToken)
    {
        var game = new Game(request.Name, request.Description, request.DateTime);

        await _repository.AddAsync(game, cancellationToken);

        return game.Id;
    }
}