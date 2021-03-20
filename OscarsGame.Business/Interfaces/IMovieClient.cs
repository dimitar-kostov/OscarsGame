using OscarsGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OscarsGame.Business.Interfaces
{
    public interface IMovieClient
    {         
        Task<Movie> GetMovieAsync(string movieID);
       
        Task<List<Movie>> SearchMovieAsync(string searchString);       
    }
}
