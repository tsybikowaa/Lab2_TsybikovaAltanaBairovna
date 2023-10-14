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
      string triangleType =Program.GetTriangleType(sideA, sideB, sideC);

      // Assert
      Assert.AreEqual("равносторонний", triangleType);
  }

  [TestCase(5, 5, 8)]
  [TestCase(8, 5, 5)]
  [TestCase(5, 8, 5)]
  public void GetTriangleType_IsoscelesTriangle_ReturnsIsosceles(float a, float b, float c)
  {
      // Arrange

      // Act
      string triangleType = Program.GetTriangleType(a, b, c);

      // Assert
      Assert.AreEqual("равнобедренный", triangleType);
  }


  [TestCase(3, 4, 5)]
  [TestCase(4, 5, 3)]
  [TestCase(5, 4, 3)]
  [TestCase(4, 3, 5)]
  
  public void GetTriangleType_ScaleneTriangle_ReturnsScalene(float a, float b, float c)
  {
      // Arrange
   

      // Act
      string triangleType = Program.GetTriangleType(a,b , c);

      // Assert
      Assert.AreEqual("разносторонний", triangleType);
  }

  [TestCase(1, 2, 3)]
  [TestCase(2, 1, 3)]
  [TestCase(1, 3, 2)]
  [TestCase(3, 2, 1)]

  public void GetTriangleType_NotATriangle_ReturnsNotTriangle(float a, float b, float c)
  {
      // Arrange
   

      // Act
      string triangleType = Program.GetTriangleType(a, b, c);

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
      List<Tuple<int, int>> vertexCoordinates = Program.GetVertexCoordinates(sideA, sideB, sideC);

      // Assert
      Assert.AreEqual(3, vertexCoordinates.Count);
      Assert.AreEqual(new Tuple<int, int>(0, 0), vertexCoordinates[0]);
      Assert.AreEqual(new Tuple<int, int>(50, 0), vertexCoordinates[1]);
      Assert.AreEqual(new Tuple<int, int>(32, -2147483648), vertexCoordinates[2]);
  }

  [TestCase(1, 2, 3)]
  [TestCase(2, 1, 3)]
  [TestCase(1, 3, 2)]
  [TestCase(3, 2, 1)]
  public void GetVertexCoordinates_NotATriangle_ReturnsInvalidCoordinates(float a,float b,float c)
  {
      // Arrange
   

      // Act
      List<Tuple<int, int>> vertexCoordinates = Program.GetVertexCoordinates(a, b, c);

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
      string coordinatesString = Program.GetVertexCoordinatesString(vertexCoordinates);

      // Assert
      Assert.AreEqual("(0, 0), (60, 0), (36, 48)", coordinatesString);
  }

  [Test]
  public void GetVertexCoordinatesString_EmptyCoordinates_ReturnsEmptyString()
  {
      // Arrange
      List<Tuple<int, int>> vertexCoordinates = new List<Tuple<int, int>>();

      // Act
      string coordinatesString = Program.GetVertexCoordinatesString(vertexCoordinates);

      // Assert
      Assert.AreEqual("", coordinatesString);
  }



       
        
    }
}
