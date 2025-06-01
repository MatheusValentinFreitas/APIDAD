using APIrest_DAD.Models;
using System.Threading.Tasks;

namespace APIrest_DAD.Services.Interfaces
{
    public interface INotificacaoService
    {
        Task ProcessarNotificacoesPendentes();
    }
}