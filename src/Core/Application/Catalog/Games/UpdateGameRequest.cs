namespace FSH.WebApi.Application.Catalog.Games;

public class UpdateGameRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
	public string? Description { get; set; }
	public DateTime DateTime { get; set; }
    
}

public class UpdateGameRequestValidator : CustomValidator<UpdateGameRequest>
{

// Important leftout the T for the translations !
    public UpdateGameRequestValidator(IRepository<Game> repository, IStringLocalizer<UpdateGameRequestValidator> localizer) =>
        RuleFor(p => p.Name)
        .NotEmpty()        
        .MustAsync(async (game, name, ct) =>
        await repository.GetBySpecAsync(new GameByNameSpec(name), ct)
        is not Game existingGame || existingGame.Id == game.Id)
        .WithMessage((_, name) => "Game {0} already Exists.");
}

public class UpdateGameRequestHandler : IRequestHandler<UpdateGameRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Game> _repository;
    private readonly IStringLocalizer<UpdateGameRequestHandler> _t;

    public UpdateGameRequestHandler(IRepositoryWithEvents<Game> repository, IStringLocalizer<UpdateGameRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateGameRequest request, CancellationToken cancellationToken)
    {
        var game = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = game
        ?? throw new NotFoundException(_t["Game {0} Not Found.", request.Id]);


        game.Update(request.Name,  request.DateTime, request.Description);

        await _repository.UpdateAsync(game, cancellationToken);

        return request.Id;
    }
}