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
    public static class MovieDatabase
    {
        private static List<Movie> movies;

        public static List<Movie> All {
            get
            {
                if (movies == null)
                {
                    using (StreamReader file = System.IO.File.OpenText("movies.json"))
                    {
                        string json = file.ReadToEnd();
                        movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                    }
                }
                return movies;
            }
        }

        public static List<Movie> Search(List<Movie> list, string term)
        {
            List<Movie> results = new List<Movie>();

            foreach(Movie m in list)
            {
                if(m.Title.Contains(term, StringComparison.OrdinalIgnoreCase) || (m.Director != null && m.Director.Contains(term, StringComparison.OrdinalIgnoreCase)))
                {
                    results.Add(m);
                }
            }

            return results;
        }

        public static List<Movie> FilterByMPAA(List<Movie> movies, List<string> mpaa)
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

        public static List<Movie> FilterByMinIMDB(List<Movie> movies, float min)
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
