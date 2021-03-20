using OscarsGame.Business.Interfaces;
using OscarsGame.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OscarsGame
{
    public class MovieClient : IMovieClient
    {
        private const string BaseUrl = "https://api.themoviedb.org";
        private const string DefaultApiVersion = "3";
        private const string MoviePath = "movie";
        private const string SearchPath = "search";
        private const string JsonMediaType = "application/json";

        private string _key;

        public MovieClient(string passedKey)
        {
            _key = passedKey;

            if (client == null)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                    | SecurityProtocolType.Tls11
                    | SecurityProtocolType.Tls12
                    | SecurityProtocolType.Ssl3;

                client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        private static HttpClient client;

        public async Task<Movie> GetMovieAsync(string movieID)
        {
            string path = $"{BaseUrl}/{DefaultApiVersion}/{MoviePath}/{movieID}?api_key={_key}&append_to_response=credits";
            var response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                JObject content = await GetContent(response);
                return MapToMovie(content);
            }

            return null;
        }

        public async Task<List<Movie>> SearchMovieAsync(string searchString)
        {
            string path = String.Format("{0}/{1}/{2}/{3}?api_key={4}&query={5}", BaseUrl, DefaultApiVersion, SearchPath, MoviePath, _key, searchString);
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                JObject content = await GetContent(response);
                return MapToMovies(content);
            }
            else
            {
                throw new Exception("Something bad happened!");
            }
        }

        private async Task<JObject> GetContent(HttpResponseMessage responseMessage)
        {
            JObject content;

            if (responseMessage.Content == null)
            {
                throw new Exception("HttpResponseMessage content is null!");
            }

            if (responseMessage.Content.Headers.ContentType.MediaType != JsonMediaType)
            {
                throw new Exception($"HttpResponseMessage MediaType should be [{JsonMediaType}], but it is [{responseMessage.Content.Headers.ContentType.MediaType}] !");
            }

            return content = await responseMessage.Content.ReadAsAsync<JObject>();
        }

        private List<Movie> MapToMovies(JObject moviesContent)
        {
            JArray moviesResultContent = moviesContent["results"].Value<JArray>();

            return moviesResultContent
                .Select(item => MapToMovie(item.Value<JObject>()))
                .ToList();
        }

        private Movie MapToMovie(JObject movieContent)
        {
            Movie movie = new Movie()
            {
                Id = movieContent["id"].Value<int>(),
                Title = movieContent["title"]?.Value<string>(),
                ReleaseDate = movieContent["release_date"]?.Value<string>(),
                PosterPath = movieContent["poster_path"]?.Value<string>(),
                Overview = movieContent["overview"]?.Value<string>(),
                ImdbId = movieContent["imdb_id"] != null 
                    ? movieContent["imdb_id"].Value<string>()
                    : string.Empty,
                Credits = movieContent["credits"] != null 
                    ? MapToCredits(movieContent["credits"].Value<JObject>())
                    : null,
            };

            return movie;
        }

        private List<MovieCredit> MapToCredits(JObject creditsContent)
        {
            List<MovieCredit> cresdits = new List<MovieCredit>();

            JArray castsContent = creditsContent["cast"].Value<JArray>();

                cresdits.AddRange(
                    castsContent.Select(item =>
                        MapToCast(item.Value<JObject>())));

                JArray crewsContent = creditsContent["crew"].Value<JArray>();

                cresdits.AddRange(
                    crewsContent.Select((item, index) =>
                        MapToCrew(item.Value<JObject>(), index)));

            return cresdits;
        }

        private MovieCredit MapToCast(JObject castContent)
        {
            MovieCredit movieCredit = new MovieCredit()
            {
                Id = castContent["credit_id"].Value<string>(),
                Order = castContent["order"].Value<int>(),
                PersonId = castContent["id"].Value<int>(),
                Name = castContent["name"].Value<string>(),
                IsCast = true,
                Role = castContent["character"].Value<string>(),
                ProfilePath = castContent["profile_path"].Value<string>(),
            };

            return movieCredit;
        }

        private MovieCredit MapToCrew(JObject crewContent, int order)
        {
            MovieCredit movieCredit = new MovieCredit()
            {
                Id = crewContent["credit_id"].Value<string>(),
                Order = order,
                PersonId = crewContent["id"].Value<int>(),
                Name = crewContent["name"].Value<string>(),
                IsCast = false,
                Role = crewContent["job"].Value<string>(),
                ProfilePath = crewContent["profile_path"].Value<string>(),
            };

            return movieCredit;
        }
    }
}