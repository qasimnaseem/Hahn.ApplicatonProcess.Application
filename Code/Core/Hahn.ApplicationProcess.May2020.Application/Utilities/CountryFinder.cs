using Hahn.ApplicationProcess.May2020.Application.Common.Interfaces;
using Hahn.ApplicationProcess.May2020.Domain.Common;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.May2020.Application.Utilities
{
    public class CountryFinder : ICountryFinder
    {
        private readonly CountryFinderApiOpts _countryFinderApiOpts;
        public CountryFinder(IOptions<CountryFinderApiOpts> apiOpts)
        {
            _countryFinderApiOpts = apiOpts.Value;
        }

        public async Task<bool> FindAsync(string name)
        {
            var url  = $"{_countryFinderApiOpts.ApiUrl}/{name}?fullText=true";
            
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(url));
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
