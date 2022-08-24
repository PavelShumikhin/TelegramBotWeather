using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot.Cities
{
    //Класс отвечающий за хранение информации о городах
    class AllCities
    {
        public List<City> allCities = new List<City>();

        //Конструктор AllCities
        public AllCities()
        {
            addCountry();
        }

        //Добавление городов в лист
        public void addCountry()
        {
            allCities.Add(new City("Москва", "lat=55.4507&lon=37.3656"));
            allCities.Add(new City("Санкт-Петербург", "lat=59.5519&lon=30.1850"));
            allCities.Add(new City("Калининград", "lat=54.4223&lon=20.3039"));
            allCities.Add(new City("Токио", "lat=39.5426&lon=116.2359"));
            allCities.Add(new City("Новгород", "lat=58.3116&lon=31.1615"));
            allCities.Add(new City("Хельсинки", "lat=60.1010&lon=24.5607"));
            allCities.Add(new City("Выборг", "lat=60.4227&lon=28.4510"));

        }
    }
}
