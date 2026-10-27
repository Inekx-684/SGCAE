
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SGCAE.Tests;

[TestClass]
public class Test1
{
    [TestMethod]
    public void PrimeraPrueba_DebeSerExitosa()
    {
        // Arrange: preparar los datos
        int numero1 = 2;
        int numero2 = 3;

        // Act: ejecutar la operación
        int resultado = numero1 + numero2;

        // Assert: comprobar el resultado
        Assert.AreEqual(5, resultado);
    }
}
