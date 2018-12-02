using System;
using System.Collections.Generic;
using DBLib;
using DBLib.SQLite.Mappings;

namespace CRM.Menu
{
    static public class ProgramMenu
    {
        private static Type type;

        public static void MainPage()
        {
            Console.Clear();
            About();
            NavigationBar(false);
            Console.WriteLine();
        }

        private static void PrintList<T>()
            where T : class
        {
            type = typeof(T);
            Console.WriteLine();
            using (var session = NhibernateHelper.OpenSession())
            {
                var list = new Repository<T>(session).GetList();
                foreach (var item in list)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            Console.WriteLine(" ");
        }

        private static void NavigationBar(bool editable)
        {
            Console.WriteLine(" ");
            Console.WriteLine("1. Список заявок");
            Console.WriteLine("2. Список сервисных центров");
            Console.WriteLine("3. Список сотрудников");
            Console.WriteLine("4. Список брендов");
            Console.WriteLine("5. Список моделей");
            Console.WriteLine("6. Список видов техники");
            if (editable)
            {
                Console.WriteLine("");
                Console.WriteLine("F1  Добавить запись");
                Console.WriteLine("F2  Удалить запись");
            }
            Console.WriteLine(" ");
            Console.WriteLine("0. Выход");
            Console.WriteLine(" ");
            NextPage(Console.ReadKey());
        }

        private static void NextPage(ConsoleKeyInfo callback)
        {
            switch (callback.Key)
            {
                case ConsoleKey.D1:
                    {
                        Console.Clear();
                        PrintList<CRMApplication>();
                        NavigationBar(true);
                        break;
                    }
                case ConsoleKey.D2:
                    {
                        Console.Clear();
                        PrintList<ServicePoint>();
                        NavigationBar(true);
                        break;
                    }
                case ConsoleKey.D3:
                    {
                        Console.Clear();
                        PrintList<Operator>();
                        NavigationBar(true);
                        break;
                    }
                case ConsoleKey.D4:
                    {
                        Console.Clear();
                        PrintList<Brand>();
                        NavigationBar(true);
                        break;
                    }
                case ConsoleKey.D5:
                    {
                        Console.Clear();
                        PrintList<Model>();
                        NavigationBar(true);
                        break;
                    }
                case ConsoleKey.D6:
                    {
                        Console.Clear();
                        PrintList<TechType>();
                        NavigationBar(true);
                        break;
                    }
                case ConsoleKey.D0:
                    {
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    }
                case ConsoleKey.F1:
                    {
                        Console.Clear();
                        if (type == typeof(Brand)) AddBrand();
                        if (type == typeof(Model)) AddModel();
                        if (type == typeof(TechType)) AddType();
                        if (type == typeof(Operator)) AddEmployee();
                        if (type == typeof(ServicePoint)) AddServicePoint();
                        if (type == typeof(CRMApplication)) AddApplication();
                        NavigationBar(false);
                        break;
                    }
                case ConsoleKey.F2:
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Введите ID записи для удаления");
                        bool correct = false;
                        while (!correct)
                        {
                            try
                            {
                                    DeleteEntity(Convert.ToInt32(Console.ReadLine()));
                                    correct = true;
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine(e.ToString());
                                Console.WriteLine("");
                                Console.WriteLine("Неправильный ввод, попробуйте снова");
                            }
                        }
                        NavigationBar(false);
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        About();
                        NavigationBar(false);
                        break;
                    }
            }
        }

        private static void AddBrand()
        {
            Console.WriteLine("Регистрация бренда");
            Console.WriteLine("");
            Console.WriteLine("Название бренда");
            Brand brand = new Brand();
            brand.Name = Console.ReadLine();
            using (var session = NhibernateHelper.OpenSession())
            {
                new Repository<Brand>(session).Save(brand);
            }
        }
        private static void AddModel()
        {
            Console.WriteLine("Регистрация модели");
            Console.WriteLine("");
            Model model = new Model();

            Console.WriteLine("Введите название модели");
            model.Name = Console.ReadLine();
            
            using (var session = NhibernateHelper.OpenSession())
            {
                Console.WriteLine("Введите название бренда");
                var brand = new Repository<Brand>(session).GetListByValue("Name",Console.ReadLine())[0];
                Console.WriteLine("Введите тип техники");
                var ttype = new Repository<TechType>(session).GetListByValue("Name", Console.ReadLine())[0];
                model.Brand = brand;
                model.TechType = ttype;
                new Repository<Model>(session).Save(model);
            }
        }
        private static void AddType()
        {
            Console.WriteLine("Регистрация типа");
            Console.WriteLine("");
            Console.WriteLine("Название типа");
            TechType ttype = new TechType();
            ttype.Name = Console.ReadLine();
            using (var session = NhibernateHelper.OpenSession())
            {
                new Repository<TechType>(session).Save(ttype);
            }
        }
        private static void AddEmployee()
        {
            Console.WriteLine("Регистрация сотрудника");
            Console.WriteLine("");
            Console.WriteLine("Имя");
            Operator oper = new Operator();
            oper.Name = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Фамилия");
            oper.SecondName = Console.ReadLine();
            Console.WriteLine("");
            
            using (var session = NhibernateHelper.OpenSession())
            {
                Console.WriteLine("Место работы");
                var servicePoint = new Repository<ServicePoint>(session).GetListByValue("Title", Console.ReadLine())[0];
                oper.ServicePoint = servicePoint;
                new Repository<Operator>(session).Save(oper);
            }
        }
        private static void AddServicePoint()
        {
            Console.WriteLine("Регистрация Сервисного Центра");
            Console.WriteLine("");
            Console.WriteLine("Название СЦ");
            ServicePoint sp = new ServicePoint();
            sp.Title = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Адрес СЦ");
            sp.Adress = Console.ReadLine();
            using (var session = NhibernateHelper.OpenSession())
            {
                new Repository<ServicePoint>(session).Save(sp);
            }
        }
        private static void AddApplication()
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                CRMApplication apl = new CRMApplication();
                apl.AcceptingDate = DateTime.Now;
                Console.WriteLine("");
                Console.WriteLine(" Регистрация новой заявки ");
                Console.WriteLine("");
                Console.WriteLine("Имя обратившегося");
                apl.Applicator = Console.ReadLine();
                Console.WriteLine("");
                Console.WriteLine("Номер телефона клиента");
                apl.PhoneNumber = Console.ReadLine();
                Console.WriteLine("");
                Console.WriteLine("Причина обращения");
                apl.Reason = Console.ReadLine();
                Console.WriteLine("Тип оборудования (введите ID)");
                Console.WriteLine("-------------------");
                PrintList<TechType>();
                Console.WriteLine("-------------------");

                bool correct = false;
                while (!correct)
                {
                    try
                    {
                        apl.Type = new Repository<TechType>(session).GetEntity(Convert.ToInt32(Console.ReadLine()));
                        if (apl.Type == null) Console.WriteLine("Неправильный ввод, попробуйте еще раз"); else correct = true;
                    }
                    catch
                    {
                        Console.WriteLine("Неправильный ввод, попробуйте еще раз");
                    }
                }

                Console.WriteLine("Бренд оборудования (введите ID)");
                Console.WriteLine("-------------------");
                PrintList<Brand>();
                Console.WriteLine("-------------------");

                correct = false;
                while (!correct)
                {
                    try
                    {
                        apl.Brand = new Repository<Brand>(session).GetEntity(Convert.ToInt32(Console.ReadLine()));
                        if (apl.Brand == null) Console.WriteLine("Неправильный ввод, попробуйте еще раз"); else correct = true;
                    }
                    catch
                    {
                        Console.WriteLine("Неправильный ввод, попробуйте еще раз");
                    }
                }

                Console.WriteLine("Модель оборудования (введите ID)");
                Console.WriteLine("-------------------");
                PrintList<Model>();
                Console.WriteLine("-------------------");

                correct = false;
                while (!correct)
                {
                    try
                    {
                        apl.Model = new Repository<Model>(session).GetEntity(Convert.ToInt32(Console.ReadLine()));
                        if (apl.Model == null) Console.WriteLine("Неправильный ввод, попробуйте еще раз"); else correct = true;
                    }
                    catch
                    {
                        Console.WriteLine("Неправильный ввод, попробуйте еще раз");
                    }
                }

                apl.ServicePoint = new Repository<ServicePoint>(session).GetEntity(1);
                apl.Operator = new Repository<Operator>(session).GetEntity(2);
                apl.Status = "Обработан";
                new Repository<CRMApplication>(session).Save(apl);
            }
        }
        private static void DeleteEntity(int ID)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                if (type == typeof(Brand)) new Repository<Brand>(session).Delete(ID);
                if (type == typeof(Model)) new Repository<Model>(session).Delete(ID);
                if (type == typeof(TechType)) new Repository<TechType>(session).Delete(ID);
                if (type == typeof(Operator)) new Repository<Operator>(session).Delete(ID);
                if (type == typeof(ServicePoint)) new Repository<ServicePoint>(session).Delete(ID);
                if (type == typeof(CRMApplication)) new Repository<CRMApplication>(session).Delete(ID);
            }
        }
        private static void About()
        {
            Console.Clear();
            Console.WriteLine("Консольная версия, с большим функционалом чем MVC");
            Console.WriteLine("ФДДО МИЭТ 2018 год. Шевцов Е.Р.");
        }
    }
}
