using APIrest_DAD.Models;
using APIrest_DAD.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APIrest_DAD.Services
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly AppDbContext _context;

        public NotificacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task ProcessarNotificacoesPendentes()
        {
            var notificacoes = await _context.notificacao
                .Where(n => n.dataNotificacao <= DateTime.Now && n.status == 0)
                .ToListAsync();

            foreach (var notificacao in notificacoes)
            {
                Console.WriteLine($"Processando notificação ID: {notificacao.codigoNotificacao} | " +
                               $"Usuário: {notificacao.nome} | " +
                               $"Data: {notificacao.dataNotificacao}");

                notificacao.status = 1;
            }

            if (notificacoes.Any())
            {
                await _context.SaveChangesAsync();
                Console.WriteLine($"{notificacoes.Count} notificações atualizadas.");
            }
        }
    }
}