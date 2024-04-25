using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static thunder_streaming_space.Deserializers.ItemMovies;

namespace thunder_streaming_space.Database
{
    internal class DataSet
    {
        private List<Movies> movies;
        public DataSet() => this.movies = new List<Movies>();    
        
        public void Insert(Movies movie) => movies.Add(movie);        
        public List<Movies> Select() => movies;
        public void Delete() => movies.Clear();        
    }
}
