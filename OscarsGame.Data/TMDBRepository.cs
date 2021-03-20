using OscarsGame.Data.Interfaces;
using OscarsGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace OscarsGame.Data
{
    public class TMDBRepository: ITMDBRepository
    {
        private const string baseUrl = "https://api.themoviedb.org";
        private const string DefaltApiVersion = "3";
        private const string moviePath = "movie";
        private const string searchPath = "search";
        private string key;

        public TMDBRepository(string passedKey)
        {
            key = passedKey;

            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        private static HttpClient client;

        public async Task<Movie> GetMovieAsync(string movieID)
        {
            string path = $"{baseUrl}/{DefaltApiVersion}/{moviePath}/{movieID}?api_key={key}";
            var response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Movie movie = await response.Content.ReadAsAsync<Movie>();
                return movie;
            }
            return null;
        }

        public async Task<MoviesCollection> SearchMovieAsync(string searchString)
        {
            string path = String.Format("{0}/{1}/{2}/{3}?api_key={4}&query={5}", baseUrl, DefaltApiVersion, searchPath, moviePath, key, searchString);
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var movies = await response.Content.ReadAsAsync<MoviesCollection>();
                return movies;
            }
            else
            {
                throw new Exception("Something bad happened!");
            }
        }
    }
}
