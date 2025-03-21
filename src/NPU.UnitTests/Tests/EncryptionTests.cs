using LockNote.Bl;

namespace LockNote.UnitTests.Tests;

public class EncryptionTests
{
    [Theory]
    [InlineData("password", "text")]
    [InlineData("Password", "text2")]
    [InlineData("12344556", "text3")]
    [InlineData("string", "text4")]
    [InlineData("password-ok-123", "text5")]
    [InlineData("hi", "text6")]
    [InlineData("password-123-123-123-hi-this-is-a-good-password", "text7")]
    [InlineData("123", "test")]
    public void Test1(string password, string text)
    {
        var encryptedText = Encryption.Encrypt(text, password);
        var decryptedText = Encryption.Decrypt(encryptedText, password);
        Assert.Equal(text, decryptedText);
    }
}