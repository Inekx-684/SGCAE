using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGCAE.Domain.Entities.Credentials;
using SGCAE.Domain.Exceptions;
namespace SGCAE.Tests.Domain.Credentials;
[TestClass]
public sealed class CredentialTests
{
    [TestMethod]
    public void NewCredential_StartsActive()
    {
        var c = new Credential(Guid.NewGuid(), "ABC-123");
        Assert.AreEqual(CredentialStatus.Active, c.Status);
    }
    [TestMethod]
    public void Block_ActiveCredential_SetsBlocked()
    {
        var c = new Credential(Guid.NewGuid(), "ABC-123");
        c.Block();
        Assert.AreEqual(CredentialStatus.Blocked, c.Status);
        Assert.IsNotNull(c.BlockedAt);
    }
    [TestMethod]
    public void Revoke_BlockedCredential_SetsRevoked()
    {
        var c = new Credential(Guid.NewGuid(), "ABC-123");
        c.Block(); c.Revoke();
        Assert.AreEqual(CredentialStatus.Revoked, c.Status);
    }
    [TestMethod]
    public void Block_RevokedCredential_Throws()
    {
        var c = new Credential(Guid.NewGuid(), "ABC-123");
        c.Revoke();
        Assert.ThrowsExactly<BussinesRuleException>(() => c.Block());
    }
}
