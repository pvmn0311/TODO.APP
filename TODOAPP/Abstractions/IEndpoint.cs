using TODO_APP.Application.Service;
using TODO_APP.Domain;

namespace TODO_APP.Api.Abstractions
{
    public interface IEndpoint
    {
        void MapEndpoint(IEndpointRouteBuilder app);
    }
}
