using FSH.WebApi.Application.Common.Exporters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Catalog.Games;
public class ExportGamesRequest : BaseFilter, IRequest<Stream>
{
    public DefaultIdType GameId { get; set; }
}
public class ExportProductsRequestHandler : IRequestHandler<ExportGamesRequest, Stream>
{
    private readonly IReadRepository<Game> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportProductsRequestHandler(IReadRepository<Game> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportGamesRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportProductsWithBrandsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportProductsWithBrandsSpecification : EntitiesByBaseFilterSpec<Game, GameDto>
{
    public ExportProductsWithBrandsSpecification(ExportGamesRequest request)
        : base(request) =>
        Query
            //.Include(p => p.Id)
            .Where(p => p.Id.Equals(request.GameId), request.GameId != Guid.Empty);
            
}