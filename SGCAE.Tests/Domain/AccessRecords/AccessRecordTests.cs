using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGCAE.Domain.Entities.AccessRecords;
namespace SGCAE.Tests.Domain.AccessRecords;
[TestClass]
public class AccessRecordTests
{
    [TestMethod]
    public void RejectedRecord_StoresReasonAndTime()
    {
        var record = new AccessRecord(null, null, null, "NO-REGISTRADA", AccessMovement.Entry,
            AccessDecision.Rejected, "Credencial no registrada.");
        Assert.AreEqual(AccessDecision.Rejected, record.Decision);
        Assert.AreEqual("Credencial no registrada.", record.Reason);
        Assert.IsTrue(record.OccurredAt <= DateTime.UtcNow);
    }
    [TestMethod]
    public void ApprovedRecord_StoresReferences()
    {
        var employee = Guid.NewGuid(); var credential = Guid.NewGuid(); var point = Guid.NewGuid();
        var record = new AccessRecord(employee, credential, point, "CARD-001", AccessMovement.Exit,
            AccessDecision.Approved, "Acceso autorizado.");
        Assert.AreEqual(employee, record.EmployeeId);
        Assert.AreEqual(credential, record.CredentialId);
        Assert.AreEqual(point, record.AccessPointId);
    }
}
