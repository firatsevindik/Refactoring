using Refactoring.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Refactoring
{
    class Program
    {
        static IDictionary<string, Course> courses;
        static Invoice invoice;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Refactoring Example");
            courses = new Dictionary<string, Course>();
            courses.Add("dpattern", new Course() { Name = "Design Pattern", Type = Types.Software });
            courses.Add("hface", new Course() { Name = "Human Face", Type = Types.Art });
            courses.Add("redis", new Course() { Name = "Redis", Type = Types.Software });

            invoice = new Invoice();
            invoice.CustomerName = "FS Team";
            invoice.Registers = new Register[]
            {
                new Register() {CourseID = "dpattern", Student = 20},
                new Register() {CourseID = "hface", Student = 5},
                new Register() {CourseID = "redis", Student = 5},
            };


            var result = $"{invoice.CustomerName} için Fatura Detayı: \n";

            foreach (Register reg in invoice.Registers)
            {

                //her bir şiparişin fiyatı
                result += $"{FindCourse(reg).Name}: {Tr(GetAmounth(reg) / 100)} ({reg.Student} kişi)\n";
            }

            result += $"Toplam borç {Tr(GetTotalAmount() / 100)}\n";
            result += $"Kazancınız { Tr(TotalVolumeCredits()) } \n";

            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static decimal TotalVolumeCredits()
        {
            decimal result = 0;
            foreach (Register reg in invoice.Registers)
            {
                //kazanılan para puan
                result += CalculateVolumeCredit(reg);
            }

            return result;
        }

        public static int GetAmounth(Register register)
        {
            var result = 0;

            switch (FindCourse(register).Type)
            {
                case Types.Art:
                    {
                        result = 3000;
                        if (register.Student > 15)
                        {
                            result += 1000 * (register.Student - 10);
                        }
                        break;
                    }
                case Types.Software:
                    {
                        result = 30000;
                        if (register.Student > 10)
                        {
                            result += 10000 + 500 * (register.Student - 5);
                        }
                        result += 300 * register.Student;
                        break;
                    }
            }

            return result;
        }

        public static Course FindCourse(Register register)
        {
            return courses[register.CourseID];
        }

        public static decimal CalculateVolumeCredit(Register register)
        {
            decimal result = 0;
            //kazanılan para puan
            result += Math.Max(register.Student - 15, 0);

            //extra bonus para puan her 5 yazılım öğrencisi için
            decimal fiveStudentGroup = register.Student / 5;
            if (Types.Software == FindCourse(register).Type) result += Math.Floor(fiveStudentGroup);
            return result;
        }

        public static string Tr(decimal value)
        {
            CultureInfo trFormat = new CultureInfo("tr-TR", false);
            trFormat.NumberFormat.CurrencySymbol = "TL";
            trFormat.NumberFormat.NumberDecimalDigits = 2;
            return value.ToString("C", trFormat);
        }

        public static decimal GetTotalAmount()
        {
            decimal result = 0;
            foreach (Register reg in invoice.Registers)
            {
                result += GetAmounth(reg);
            }
            return result;
        }
    }


    #region BeforeRefactoring

        //class Program
        //{
        //    static void Main(string[] args)
        //    {
        //        Console.WriteLine("Welcome to Refactoring Example");
        //        IDictionary<string, Course> courses = new Dictionary<string, Course>();
        //        courses.Add("dpattern", new Course() { Name = "Design Pattern", Type = Types.Software });
        //        courses.Add("hface", new Course() { Name = "Human Face", Type = Types.Art });
        //        courses.Add("redis", new Course() { Name = "Redis", Type = Types.Software });

        //        Invoice invoice = new Invoice();
        //        invoice.CustomerName = "FS Team";
        //        invoice.registers = new Register[]{
        //        new Register(){CourseID="dpattern",Student=20},
        //        new Register() { CourseID = "hface", Student = 15 },
        //        new Register() { CourseID = "redis", Student = 5 },
        //    };

        //        decimal totalAmount = 0;
        //        decimal volumeCredits = 0;
        //        var result = $"{invoice.CustomerName} için Fatura Detayı: \n";

        //        CultureInfo trFormat = new CultureInfo("tr-TR", false);
        //        trFormat.NumberFormat.CurrencySymbol = "TL";
        //        trFormat.NumberFormat.NumberDecimalDigits = 2;

        //        foreach (Register reg in invoice.registers)
        //        {
        //            Course lesson = courses[reg.CourseID];
        //            var thisAmount = 0;

        //            switch (lesson.Type)
        //            {
        //                case Types.Art:
        //                    {
        //                        thisAmount = 3000;
        //                        if (reg.Student > 15)
        //                        {
        //                            thisAmount += 1000 * (reg.Student - 10);
        //                        }
        //                        break;
        //                    }
        //                case Types.Software:
        //                    {
        //                        thisAmount = 30000;
        //                        if (reg.Student > 10)
        //                        {
        //                            thisAmount += 10000 + 500 * (reg.Student - 5);
        //                        }
        //                        thisAmount += 300 * reg.Student;
        //                        break;
        //                    }
        //            }
        //            //kazanılan para puan
        //            volumeCredits += Math.Max(reg.Student - 15, 0);

        //            // extra bonus para puan her 5 yazılım öğrencisi için
        //            decimal fiveStudentGroup = reg.Student / 5;
        //            if (Types.Software == lesson.Type) volumeCredits += Math.Floor(fiveStudentGroup);

        //            // her bir şiparişin fiyatı
        //            result += $"{lesson.Name}: {(thisAmount / 100).ToString("C", trFormat)} ({reg.Student} kişi)\n";
        //            totalAmount += thisAmount;
        //        }
        //        result += $"Toplam borç { (totalAmount / 100).ToString("C", trFormat)}\n";
        //        result += $"Kazancınız { volumeCredits.ToString("C", trFormat) } \n";
        //        Console.WriteLine(result);
        //        Console.ReadLine();
        //    }
        //}
    

    #endregion

}
