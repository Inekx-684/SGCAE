using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGCAE.Domain.Entities.AccessPoints;
using SGCAE.Domain.Exceptions;
namespace SGCAE.Tests.Domain.AccessPoints;
[TestClass]
public sealed class AccessPointTests
{
    [TestMethod]
    public void NewPoint_IsActive() { var p = new AccessPoint("Recepción", "Edificio A"); Assert.IsTrue(p.IsActive); }
    [TestMethod]
    public void Deactivate_ActivePoint_IsInactive() { var p = new AccessPoint("Recepción", "Edificio A"); p.Deactivate(); Assert.IsFalse(p.IsActive); }
    [TestMethod]
    public void Activate_InactivePoint_IsActive() { var p = new AccessPoint("Recepción", "Edificio A"); p.Deactivate(); p.Activate(); Assert.IsTrue(p.IsActive); }
    [TestMethod]
    public void Deactivate_Twice_Throws() { var p = new AccessPoint("Recepción", "Edificio A"); p.Deactivate(); Assert.ThrowsExactly<BussinesRuleException>(() => p.Deactivate()); }
    [TestMethod]
    public void EmptyName_Throws() => Assert.ThrowsExactly<BussinesRuleException>(() => new AccessPoint(" ", "Edificio A"));
}
