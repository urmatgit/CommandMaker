using FSH.WebApi.Domain.Common.Events;
namespace FSH.WebApi.Application.Catalog.Games;

public class CreateGameRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
	public string? Description { get; set; }
	public DateTime DateTime { get; set; }
    public TimeSpan? Time { get; set; }
}


// Important leftout the T for the translations !

public class CreateGameRequestValidator : CustomValidator<CreateGameRequest>
{
    public CreateGameRequestValidator(IReadRepository<Game> repository, IStringLocalizer<CreateGameRequestValidator> localizer)
    {
        RuleFor(p => p.Name)
        .NotEmpty()
        .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new GameByNameSpec(name), ct) is null)
       .WithMessage((_, name) => "Game {0} already Exists.");
        RuleFor(p => p.DateTime)
            .NotEmpty()
            .InclusiveBetween( DateTime.Now, DateTime.Now.AddMonths(3))
            .WithMessage("The date can be set up to 3 months!");
    }
}

public class CreateGameRequestHandler : IRequestHandler<CreateGameRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Game> _repository;

    public CreateGameRequestHandler(IRepositoryWithEvents<Game> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateGameRequest request, CancellationToken cancellationToken)
    {
        var datetime = new DateTime(request.DateTime.Year, request.DateTime.Month, request.DateTime.Day, request.Time.Value.Hours, request.Time.Value.Minutes, request.Time.Value.Seconds);
        var game = new Game(request.Name, request.Description, datetime);
    
        await _repository.AddAsync(game, cancellationToken);

        return game.Id;
    }
}