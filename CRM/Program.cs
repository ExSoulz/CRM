using System;
using DBLib;
using DBLib.SQLite.Mappings;

namespace CRM
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                Repository<CRMApplication> repository = new Repository<CRMApplication>(session);
                CRMApplication apl = new CRMApplication();
                apl.AcceptingDate = DateTime.Now;
                apl.Applicator = "Вазген";
                apl.PhoneNumber = "+7-961-407-41-38";

                var list = repository.GetList();

                foreach (var x in list)
                {
                    Console.WriteLine($"{x.ID} {x.AcceptingDate} {x.Applicator} {x.PhoneNumber}");
                }

                repository.Save(apl);
            }
        }
    }
}
