using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public class MovieDatabase
    {
        private List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        public MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }

        public List<Movie> All { get { return movies; } }

        public List<Movie> Search(string term)
        {
            List<Movie> results = new List<Movie>();

            foreach(Movie m in movies)
            {
                if(m.Title.Contains(term, StringComparison.OrdinalIgnoreCase) || (m.Director != null && m.Director.Contains(term, StringComparison.OrdinalIgnoreCase)))
                {
                    results.Add(m);
                }
            }

            return results;
        }

        public List<Movie> FilterByMPAA(List<Movie> movies, List<string> mpaa)
        {
            List<Movie> results = new List<Movie>();

            foreach (Movie m in movies)
            {
                if (mpaa.Contains(m.MPAA_Rating))
                {
                    results.Add(m);
                }
            }
            return results;
        }

        public List<Movie> FilterByMinIMDB(List<Movie> movies, float min)
        {
            List<Movie> results = new List<Movie>();

            foreach (Movie m in movies)
            {
               if(m.IMDB_Rating != null && m.IMDB_Rating >= min)
                {
                    results.Add(m);
                }
            }
            return results;
        }
    }
}
