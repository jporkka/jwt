using System;
using System.Security.Cryptography;
using AutoFixture;
using FluentAssertions;
using JWT.Algorithms;
using Xunit;

namespace JWT.Tests.Common
{
    public class RS256AlgorithmTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void Ctor_Should_Throw_Exception_When_PublicKey_Is_Null()
        {
            var privateKey = _fixture.Create<RSACryptoServiceProvider>();

            Action newWithoutPublicKey =
                () => new RS256Algorithm(null, privateKey);

            newWithoutPublicKey.Should()
                               .Throw<ArgumentNullException>("because asymmetric algorithm cannot be constructed without public key");
        }

        [Fact]
        public void Ctor_Should_Throw_Exception_When_PrivateKey_Is_Null()
        {
            var publicKey = _fixture.Create<RSACryptoServiceProvider>();

            Action newWithoutPrivateKey =
                () => new RS256Algorithm(publicKey, null);

            newWithoutPrivateKey.Should()
                                .Throw<ArgumentNullException>("because asymmetric algorithm cannot be constructed without private key");
        }

        [Fact]
        public void Sign_Should_Throw_Exception_When_PrivateKey_Is_Null()
        {
            var publicKey = _fixture.Create<RSACryptoServiceProvider>();
            var alg = new RS256Algorithm(publicKey);

            var bytesToSign = Array.Empty<byte>();

            Action signWithoutPrivateKey =
                () => alg.Sign(null, bytesToSign);

            signWithoutPrivateKey.Should()
                                 .Throw<InvalidOperationException>("because asymmetric algorithm cannot sign data without private key");
        }
    }
}
