using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Optsol.Components.Infra.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private ILogger _logger;
        private bool _disposed = false;
        
        public DbContext Context { get; protected set; }

        public UnitOfWork(DbContext context, ILogger<UnitOfWork> logger)
        {
            _logger = logger;
            _logger?.LogInformation("Inicializando UnitOfWork");

            Context = context;
        }

        public Task<bool> CommitAsync()
        {
            _logger?.LogInformation($"Método: { nameof(CommitAsync) }() Retorno: bool");

            return Task.FromResult(Context.SaveChanges() > 0);
        }

        private void Dispose(bool disposing)
        {
            if(!_disposed)
            {
                if(disposing)
                {
                    Context.Dispose();
                }
            }            
            _disposed = true;
        }

        public void Dispose()
        {
            _logger?.LogInformation($"Método: { nameof(Dispose) }()");

            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
