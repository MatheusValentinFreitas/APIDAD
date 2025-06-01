using Hangfire;
using APIrest_DAD.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace APIrest_DAD.Jobs
{
    public class NotificacaoJob
    {
        private readonly INotificacaoService _notificacaoService;

        public NotificacaoJob(INotificacaoService notificacaoService)
        {
            _notificacaoService = notificacaoService;
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task ProcessarNotificacoes()
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Iniciando processamento de notificações...");

            try
            {
                await _notificacaoService.ProcessarNotificacoesPendentes();
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Processamento concluído com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] ERRO: {ex.Message}");
                throw;
            }
        }
    }
}