using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Data.Maps.Common
{
    public interface IMap
    {
        void Visit(ModelBuilder builder);
    }
}
