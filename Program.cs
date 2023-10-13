
using System;
using System.Collections.Generic;
using System.IO;
using Serilog;
using Serilog.Events;
using static System.Net.WebRequestMethods;

class Program
{

    //public string logFilePath = "C:\\Users\\tsybi\\source\\repos\\laba1_podderzhka\\laba1_podderzhka\\triangle.log";
    static void Main(string[] args)
    {
        string template = "{Timestamp:HH:mm:ss} | [{Level:u3}] | {Message:lj}{NewLine}{Exception}";
        Log.Logger = new LoggerConfiguration()
          .MinimumLevel.Verbose()
          .WriteTo.Console(outputTemplate: template)
          .WriteTo.File("C:\\Users\\tsybi\\source\\repos\\laba1_podderzhka\\laba1_podderzhka\\triangle.log", outputTemplate: template)
        .CreateLogger();


        Log.Verbose(" Логгер сконфигурирован");
        Log.Information(" Приложение запущено");


        // Ввод данных
        Console.WriteLine("Введите длину стороны А:");
        string sideAInput = Console.ReadLine();
        Console.WriteLine("Введите длину стороны Б:");
        string sideBInput = Console.ReadLine();
        Console.WriteLine("Введите длину стороны С:");
        string sideCInput = Console.ReadLine();

        var result = getTriangleIngfo(sideAInput, sideBInput, sideCInput);

        string r = "";
        foreach (var ver in result.Item2)
        {
            r += $"({ver.Item1}, {ver.Item2}); ";
        }


        Log.Information($"result: '{result.Item1}' & '{r}'");
        Log.Verbose("end");
        Log.CloseAndFlush();


    }

    private static (string, List<Tuple<int, int>>) getTriangleIngfo(string? sideAInput, string? sideBInput, string? sideCInput)
    {

 

        //using StreamWriter logFile = new StreamWriter(logFilePath, true);

        // Проверка и анализ входных данных
        if (float.TryParse(sideAInput, out float sideA) && float.TryParse(sideBInput, out float sideB) && float.TryParse(sideCInput, out float sideC))
        {
            // Вычисление типа треугольника
            string triangleType = GetTriangleType(sideA, sideB, sideC);

            // Вычисление координат вершин
            List<Tuple<int, int>> vertexCoordinates = GetVertexCoordinates(sideA, sideB, sideC);



            // Вывод результатов
            Console.WriteLine("Тип треугольника: " + triangleType);
            Console.WriteLine("Координаты вершин:");
            foreach (var vertex in vertexCoordinates)
            {
                Console.WriteLine("(" + vertex.Item1 + ", " + vertex.Item2 + ")");
            }

            // Логирование успешного запроса
            string template = "Успешный запрос: " + DateTime.Now.ToString() + "\n"
               + "Параметры запроса: сторона А = " + sideA + ", сторона Б = " + sideB + ", сторона С = " + sideC + "\n"
               + "Результаты запроса: тип треугольника = " + triangleType + ", координаты вершин = " + GetVertexCoordinatesString(vertexCoordinates);
            Console.WriteLine("Запись лога успешного запроса выполнена.");

         
     

            return (triangleType, vertexCoordinates);


        }
        else
        {
            // Логирование неуспешного запроса
            string template = "Неуспешный запрос: " + DateTime.Now.ToString() + "\n"
                + "Параметры запроса: сторона А = " + sideAInput + ", сторона Б = " + sideBInput + ", сторона С = " + sideCInput + "\n"
                + "Результат запроса: некорректные входные данные"; ;
            Console.WriteLine("Запись лога неуспешного запроса выполнена.");


            
    

            return ("", new List<Tuple<int, int>> { new Tuple<int, int> (-2, -2), new Tuple<int, int> ( -2, -2 ), new Tuple<int, int> ( -2, -2 ) });

        }
        
    }

    static string GetTriangleType(float sideA, float sideB, float sideC)
    {
        if (sideA + sideB > sideC && sideA + sideC > sideB && sideB + sideC > sideA)
        {
            if (sideA == sideB && sideB == sideC)
            {
                return "равносторонний";
            }
            else if (sideA == sideB || sideA == sideC || sideB == sideC)
            {
                return "равнобедренный";
            }
            else
            {
                return "разносторонний";
            }
        }
        else
        {
            return "не треугольник";
        }
    }

    static List<Tuple<int, int>> GetVertexCoordinates(float sideA, float sideB, float sideC)
    {
        const int fieldSize = 100;
        const int scalingFactor = fieldSize / 10; // Масштабирование координат для отображения в поле 100x100 px

        if (sideA + sideB > sideC && sideA + sideC > sideB && sideB + sideC > sideA)
        {
            // Вычисление координат вершин треугольника в пространстве 10x10
            int xA = 0;
            int yA = 0;
            int xB = (int)(sideC * scalingFactor);
            int yB = 0;
            int xC = (int)(sideB * scalingFactor * Math.Cos(Math.Acos((sideB * sideB + sideC * sideC - sideA * sideA) / (2 * sideB * sideC))));

            int yC = (int)(sideB * scalingFactor * Math.Sin(Math.Asin(sideB * Math.Sin(Math.Acos((sideB * sideB + sideC * sideC - sideA * sideA) / (2 * sideB * sideC))))));

            return new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(xA, yA),
                new Tuple<int, int>(xB, yB),
                new Tuple<int, int>(xC, yC)
            };
        }
        else
        {
            // Для некорректных входных данных возвращаем (-1, -1) в качестве координат всех вершин
            return new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(-1, -1),
                new Tuple<int, int>(-1, -1),
                new Tuple<int, int>(-1, -1)
            };
        }
    }

    static string GetVertexCoordinatesString(List<Tuple<int, int>> vertexCoordinates)
    {
        string coordinatesString = "";
        foreach (var vertex in vertexCoordinates)
        {
            coordinatesString += "(" + vertex.Item1 + ", " + vertex.Item2 + "), ";
        }
        return coordinatesString.TrimEnd(',', ' ');
    }

  
}
