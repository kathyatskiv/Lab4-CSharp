using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YatskivLab4.Models;
using YatskivLab4.Tools.Managers;

namespace YatskivLab4.Tools.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        private readonly List<Person> _persons;

        internal SerializedDataStorage()
        {
            try
            {
                _persons = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _persons = new List<Person>();
                FillPersons();
            }
        }

        public void AddPerson(Person person)
        {
            _persons.Add(person);
            SaveChanges();
        }

        public void DeletePerson(Person person)
        {
            _persons.Remove(person);
            SaveChanges();
        }

        public List<Person> PersonsList => _persons.ToList();

        public void SaveChanges()
        {
            SerializationManager.Serialize(_persons, FileFolderHelper.StorageFilePath);
        }

        private void FillPersons()
        {
            Random ran = new Random();
            string[] names = { "Kateryna", "Mark", "Olga", "Vladyslav", "Alina", "Darina", "Evgenii", "Valeria", "Oleksandr", "Sofiya", "Artem", "Kyrylo", "Vladyslav", "Anna", "Yaroslava", "Yaroslava", "Anna", "Dennis", "Oleksandr", "Igor", "Tatiana", "Irina", "Konstantin", "Volodymyr", "Igor", "Serafima", "Daryna", "Anastasia", "Mariya", "Ilona", "Sofiya", "Andriy", "Dmytro", "Iliya", "Svitlana", "Vladyslav", "Vladyslav", "Vitalyi", "Daria", "Maryna", "Oleksandra", "Ivan", "Artem", "Maksym", "Dmytro", "Mykola", "Anna", "Ivan", "Taras", "Lesya" };
            string[] surnames = { "Yatskiv", "Godnyi", "Perch", "Bogolyi", "Kupchik", "Kupchik", "Borodaikevich", "Lunyakina", "Grytsyuk", "Olenyn", "Morhun", "Komonov", "Sobolyev", "Kostyuk", "Kurash", "Zavalna", "Churilova", "Kurochkin", "Myronovich", "Zhukovskyi", "Vasilyeva", "Bolyukh", "Katrich", "Rybak", "Honcharenko", "Lopukhina", "Nikolaiyuk", "Zinchenko", "Noschenko", "Voronina", "Bilogryva", "Vynnyk", "Holodov", "Yatcishin", "Lukichova", "Belkovets", "Dekret", "Kenyiz", "Garaschuk", "Zhukovskaya", "Vlasenko", "Bilash", "Sakh", "Kolenyk", "Kharchenko", "Nikorich", "Yatskiv", "Motornyi", "Shevchenko", "Ukrainka" };
            string[] emails = { "kateryna.yatskiv", "mark.godnyi", "olga.perch", "vladyslav.bogolyi", "alina.kupchik", "darina.kupchik", "evgenii.borodaikevich", "valeria.lunyakina", "oleksandr.grytsyuk", "sofiya.olenyn", "artem.morhun", "kyrylo.komonov", "vladyslav.sobolyev", "anna.kostyuk", "yaroslava.kurash", "yaroslava.zavalna", "anna.churilova", "dennis.kurochkin", "oleksandr.myronovich", "igor.zhukovskyi", "tatiana.vasilyeva", "irina.bolyukh", "konstantin.katrich", "volodymyr.rybak", "igor.honcharenko", "serafyma.lopukhina", "daryna.nikolaiyuk", "anastasia.zinchenko", "mariya.noschenko", "ilona.voronina", "sofiya.bilogryva", "andriy.vynnyk", "dmytro.holodov", "iliya.yatcishin", "svitlana.lukichova", "vladyslav.belkovets", "vladyslav.dekret", "kenyiz.vitalyi", "daria.garaschuk", "maryna.zhukovskaya", "oleksandra.vlasenko", "ivan.bilash", "artem.sakh", "maksym.kolesnyk", "dmytro.kharchenka", "mykola.nikorich", "anna.yatskiv", "ivan.motornyi", "taras.shevchenko", "lesya.ukrainka" };

            for (int i = 0; i < 50; i++)
            {
                DateTime birthday = new DateTime(ran.Next(1990, 2005), ran.Next(1, 12), ran.Next(1, 28));
                AddPerson(new Person(names[i], surnames[i], emails[i] + "@ukma.edu.ua", birthday));
            }
        }
    }
}

