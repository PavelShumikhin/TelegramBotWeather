using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestBot.Cities;

namespace TestBot
{
    public class Request
    {
        AllCities myCities = new AllCities();
        HttpWebRequest _request;
        string _addres;
        string _coordinates;

        public string Response { get; set; }

        public Request(string nameCity)
        {
            searchCity(nameCity);
            _addres = $"https://api.weatherbit.io/v2.0/current?{_coordinates}&key=1410135329d74feca4694bf87e711b70&include=minutely";
            Run();
            Console.WriteLine("Запрос выполнен");
        }
        //Выполнение запроса
        public void Run()
        {
            _request = (HttpWebRequest)WebRequest.Create(_addres);
            _request.Method = "GET";


            try
            {
                HttpWebResponse request = (HttpWebResponse)_request.GetResponse();
                var stream = request.GetResponseStream();
                if (stream != null)
                {
                    Response = new StreamReader(stream).ReadToEnd();
                }
            }
            catch (Exception)
            {

            }

        }
        //Поиск и определение координат города
        private void searchCity(string nameCity)
        {
            City findCity = myCities.allCities.Find(
                delegate(City city)
                {
                    return city.nameCities == nameCity;
                });
            _coordinates = findCity.coordinates;
                
        }
    }
}

