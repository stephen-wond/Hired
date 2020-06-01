using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackAJobAssessments
{
    public class StarWars
	{
		static public string Run(string film, string character)
		{
			string filmsAndCharacters =  $"{film}: ";
            filmsAndCharacters += ListOfCharacter(film);
            filmsAndCharacters += $"; {character}: ";
            filmsAndCharacters += ListOfFilms(character);
            return filmsAndCharacters;
		}

        static public string ListOfCharacter(string film)
        {
            
            Task<string> result = GetResponseString($"https://challenges.hackajob.co/swapi/api/films/?search={film}&format=json");
            var jsonResult = result.Result;
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonResult);

            if((int)jsonObject.count == 0)
            {
                return "none";
            }

            var charList = new List<string>();
            foreach (var character in jsonObject.results[0].characters)
            {
                charList.Add((string)GetObjectFromUrl(character.Value).name);
            }

            charList.Sort();
            return string.Join(", ", charList);
        }

        private static string ListOfFilms(string character)
        {
            
            Task<string> result = GetResponseString($"https://challenges.hackajob.co/swapi/api/people/?search={character}&format=json");
            var jsonResult = result.Result;
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonResult);

            if ((int)jsonObject.count == 0)
            {
                return "none";
            }

            var filmList = new List<string>();
            foreach (var film in jsonObject.results[0].films)
            {
                filmList.Add((string)GetObjectFromUrl(film.Value).title);
            }

            filmList.Sort();
            return string.Join(", ", filmList);
        }

        static public async Task<string> GetResponseString(string query)
		{
			var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(query);
			var contents = await response.Content.ReadAsStringAsync();

			return contents;
		}

        static public dynamic GetObjectFromUrl(string url)
        {
            Task<string> result = GetResponseString(url + "?format=json");
            var jsonResult = result.Result;
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonResult);

            return jsonObject;
        }
    }
}
