using Microsoft.VisualStudio.TestPlatform.TestHost;
using Serilog;
using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestProjectLab1
{
    [TestFixture]
    public class TriangleTests
    {
        private const string LogFilePath = @"C:\Users\tsybi\source\repos\laba1_podderzhka\laba1_podderzhka\triangle.log";
        [SetUp]
        public void Setup()
        {
            string template = "{Timestamp:HH:mm:ss} | [{Level:u3}] | {Message:lj}{NewLine}{Exception}";
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(outputTemplate: template)
                .WriteTo.File(LogFilePath, outputTemplate: template)
                .CreateLogger();
        }

        [TearDown]
        public void Teardown()
        {
            Log.CloseAndFlush();
        }


        [Test]
        public void Log_Configuration_Successful()
        {
            // Arrange
            string expectedLogMessage = " Логгер сконфигурирован";

            // Act
            static void Action()
            {
                Log.Verbose(" Логгер сконфигурирован");
            }

            // Assert
            AssertLogMessage(expectedLogMessage, Action);
        }

        [Test]
        public void Log_ApplicationStarted_Successful()
        {
            // Arrange
            string expectedLogMessage = " Приложение запущено";

            // Act
            static void Action()
            {
                Log.Information(" Приложение запущено");
            }

            // Assert
            AssertLogMessage(expectedLogMessage, Action);
        }
        [Test]
        public void Log_GetTriangleInfo_Successful()
        {
            // Arrange
            string expectedLogMessage = "Тип треугольника: ";

            // Act
            static void Action()
            {
                var result = getTriangleIngfo("8", "8", "8");
                Log.Information($"result: '{result.Item1}' & '{GetVertexCoordinatesString(result.Item2)}'");
            }

            // Assert
            AssertLogMessage(expectedLogMessage, Action);
        }

  

        [Test]
        public void Log_InvalidInput_Unsuccessful()
        {
            // Arrange
            string expectedLogMessage = "Запись лога неуспешного запроса выполнена.";

            // Act
            static void Action()
            {
                var result = getTriangleIngfo("a", "b", "c");
            }

            // Assert
            AssertLogMessage(expectedLogMessage, Action);
        }

        [Test]
        public void Log_TriangleType_Equilateral()
        {
            // Arrange
            string expectedLogMessage = "Тип треугольника: равносторонний";

            // Act
            static void Action()
            {
                var result = getTriangleIngfo("8", "8", "8");
                Log.Information("Тип треугольника: " + result.Item1);
            }

            // Assert
            AssertLogMessage(expectedLogMessage, Action);
        }

        [Test]
        public void Log_TriangleType_Isosceles()
        {
            // Arrange
            string expectedLogMessage = "Тип треугольника: равнобедренный";

            // Act
            static void Action()
            {
                var result = getTriangleIngfo("5", "5", "8");
                Log.Information("Тип треугольника: " + result.Item1);
            }

            // Assert
            AssertLogMessage(expectedLogMessage, Action);
        }

        [Test]
        public void Log_TriangleType_Scalene()
        {
            // Arrange
            string expectedLogMessage = "Тип треугольника: разносторонний";

            // Act
            static void Action()
            {
                var result = getTriangleIngfo("3", "4", "5");
                Log.Information("Тип треугольника: " + result.Item1);
            }

            // Assert
            AssertLogMessage(expectedLogMessage, Action);
        }

        [Test]
        public void Log_TriangleType_NotATriangle()
        {
            // Arrange
            string expectedLogMessage = "Тип треугольника: не треугольник";

            // Act
            static void Action()
            {
                var result = getTriangleIngfo("1", "2", "3");
                Log.Information("Тип треугольника: " + result.Item1);
            }

            // Assert
            AssertLogMessage(expectedLogMessage, Action);
        }

      

        private static void AssertLogMessage(string expectedLogMessage, Action action)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                action.Invoke();

                // Assert
                var logOutput = sw.ToString();
                StringAssert.Contains(expectedLogMessage, logOutput);
            }
        }

        [Test]
        public void GetTriangleInfo_InvalidInput_EmptyString()
        {
            // Arrange
            string sideAInput = "";
            string sideBInput = "8";
            string sideCInput = "8";

            // Act
            var result = getTriangleIngfo(sideAInput, sideBInput, sideCInput);

            // Assert
            Assert.AreEqual("", result.Item1);
            CollectionAssert.AreEqual(new[] { new Tuple<int, int>(-2, -2), new Tuple<int, int>(-2, -2), new Tuple<int, int>(-2, -2) }, result.Item2);
        }

        [Test]
        public void GetTriangleInfo_InvalidInput_Null()
        {
            // Arrange
            string sideAInput = null;
            string sideBInput = "8";
            string sideCInput = "8";

            // Act
            var result = getTriangleIngfo(sideAInput, sideBInput, sideCInput);

            // Assert
            Assert.AreEqual("", result.Item1);
            CollectionAssert.AreEqual(new[] { new Tuple<int, int>(-2, -2), new Tuple<int, int>(-2, -2), new Tuple<int, int>(-2, -2) }, result.Item2);
        }

        [Test]
        public void GetTriangleInfo_InvalidInput_Whitespace()
        {
            // Arrange
            string sideAInput = " ";
            string sideBInput = "8";
            string sideCInput = "8";

            // Act
            var result = getTriangleIngfo(sideAInput, sideBInput, sideCInput);

            // Assert
            Assert.AreEqual("", result.Item1);
            CollectionAssert.AreEqual(new[] { new Tuple<int, int>(-2, -2), new Tuple<int, int>(-2, -2), new Tuple<int, int>(-2, -2) }, result.Item2);
        }

        [Test]
        public void GetTriangleInfo_FileExists_ReturnsTrue()
        {
            // Arrange
            string filePath = "C:\\Users\\tsybi\\source\\repos\\laba1_podderzhka\\laba1_podderzhka\\triangle.log";
            bool expected = true;

            // Act
            bool result =FileExists(filePath);

            // Assert
            Assert.AreEqual(expected, result);
        }
  
     

        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
        [Test]
        public void GetTriangleInfo_DirectoryDoesNotExist_ReturnsFalse()
        {
            // Arrange
            string directoryPath = "C:\\Users\\tsybi\\source\\repos\\laba1_podderzhka_\\laba1_podderzhka\\triangle.log";
            bool expected = false;

            // Act
            bool result = DirectoryExists(directoryPath);

            // Assert
            Assert.AreEqual(expected, result);
        }
      

        public static bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public static bool HasReadAccess(string filePath)
        {
            try
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
        }

        public static bool HasWriteAccess(string filePath)
        {
            try
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Write))
                {
                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
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





                return ("", new List<Tuple<int, int>> { new Tuple<int, int>(-2, -2), new Tuple<int, int>(-2, -2), new Tuple<int, int>(-2, -2) });

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
}
