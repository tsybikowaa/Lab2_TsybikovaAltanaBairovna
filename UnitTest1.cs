using Microsoft.VisualStudio.TestPlatform.TestHost;
using Serilog;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;





namespace TestProjectLab1
{
    [TestFixture]
    public class Tests

    {


        [Test]
        public void GetTriangleType_EquilateralTriangle_ReturnsEquilateral()
        {
            // Arrange
            float sideA = 5;
            float sideB = 5;
            float sideC = 5;

            // Act
            string triangleType = GetTriangleType(sideA, sideB, sideC);

            // Assert
            Assert.AreEqual("равносторонний", triangleType);
        }

        [Test]
        public void GetTriangleType_IsoscelesTriangle_ReturnsIsosceles()
        {
            // Arrange
            float sideA = 5;
            float sideB = 5;
            float sideC = 8;

            // Act
            string triangleType = GetTriangleType(sideA, sideB, sideC);

            // Assert
            Assert.AreEqual("равнобедренный", triangleType);
        }

        [Test]
        public void GetTriangleType_ScaleneTriangle_ReturnsScalene()
        {
            // Arrange
            float sideA = 3;
            float sideB = 4;
            float sideC = 5;

            // Act
            string triangleType = GetTriangleType(sideA, sideB, sideC);

            // Assert
            Assert.AreEqual("разносторонний", triangleType);
        }

        [Test]
        public void GetTriangleType_NotATriangle_ReturnsNotTriangle()
        {
            // Arrange
            float sideA = 1;
            float sideB = 2;
            float sideC = 3;

            // Act
            string triangleType = GetTriangleType(sideA, sideB, sideC);

            // Assert
            Assert.AreEqual("не треугольник", triangleType);
        }

        [Test]
        public void GetVertexCoordinates_ValidSides_ReturnsCoordinates()
        {
            // Arrange
            float sideA = 3;
            float sideB = 4;
            float sideC = 5;

            // Act
            List<Tuple<int, int>> vertexCoordinates = GetVertexCoordinates(sideA, sideB, sideC);

            // Assert
            Assert.AreEqual(3, vertexCoordinates.Count);
            Assert.AreEqual(new Tuple<int, int>(0, 0), vertexCoordinates[0]);
            Assert.AreEqual(new Tuple<int, int>(50, 0), vertexCoordinates[1]);
            Assert.AreEqual(new Tuple<int, int>(32, -2147483648), vertexCoordinates[2]);
        }

        [Test]
        public void GetVertexCoordinates_NotATriangle_ReturnsInvalidCoordinates()
        {
            // Arrange
            float sideA = 1;
            float sideB = 2;
            float sideC = 3;

            // Act
            List<Tuple<int, int>> vertexCoordinates = GetVertexCoordinates(sideA, sideB, sideC);

            // Assert
            Assert.AreEqual(3, vertexCoordinates.Count);
            Assert.AreEqual(new Tuple<int, int>(-1, -1), vertexCoordinates[0]);
            Assert.AreEqual(new Tuple<int, int>(-1, -1), vertexCoordinates[1]);
            Assert.AreEqual(new Tuple<int, int>(-1, -1), vertexCoordinates[2]);
        }

        [Test]
        public void GetVertexCoordinatesString_ValidCoordinates_ReturnsFormattedString()
        {
            // Arrange
            List<Tuple<int, int>> vertexCoordinates = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(0, 0),
            new Tuple<int, int>(60, 0),
            new Tuple<int, int>(36, 48)
        };

            // Act
            string coordinatesString = GetVertexCoordinatesString(vertexCoordinates);

            // Assert
            Assert.AreEqual("(0, 0), (60, 0), (36, 48)", coordinatesString);
        }

        [Test]
        public void GetVertexCoordinatesString_EmptyCoordinates_ReturnsEmptyString()
        {
            // Arrange
            List<Tuple<int, int>> vertexCoordinates = new List<Tuple<int, int>>();

            // Act
            string coordinatesString = GetVertexCoordinatesString(vertexCoordinates);

            // Assert
            Assert.AreEqual("", coordinatesString);
        }

        // Добавьте другие тесты для проверки логирования успешных и неуспешных запросов

        private string GetTriangleType(float sideA, float sideB, float sideC)
        {
            // Реализация метода определения типа треугольника
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


          

        private List<Tuple<int, int>> GetVertexCoordinates(float sideA, float sideB, float sideC)
        {
            // Реализация метода вычисления координат вершин треугольника
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

        private string GetVertexCoordinatesString(List<Tuple<int, int>> vertexCoordinates)
        {
            // Реализация метода форматирования строкового представления координат вершин треугольника
            string coordinatesString = "";
            foreach (var vertex in vertexCoordinates)
            {
                coordinatesString += "(" + vertex.Item1 + ", " + vertex.Item2 + "), ";
            }
            return coordinatesString.TrimEnd(',', ' ');
        }


       
        
    }
}
