using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.May2020.Application.Common.Interfaces
{
    public interface ICountryFinder
    {
        Task<bool> FindAsync(string name);
    }
}
