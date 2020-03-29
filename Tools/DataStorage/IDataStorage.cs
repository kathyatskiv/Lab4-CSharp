using System.Collections.Generic;
using YatskivLab4.Models;

namespace YatskivLab4.Tools.DataStorage
{
    internal interface IDataStorage
    {
        List<Person> PersonsList { get; }

        void AddPerson(Person person);

        void DeletePerson(Person person);

        void SaveChanges();
    }
}
