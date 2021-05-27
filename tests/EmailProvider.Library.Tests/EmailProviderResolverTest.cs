using System;
using System.Threading.Tasks;
using DnsClient;
using Xunit;

namespace EmailProvider.Library.Tests
{
    public class EmailProviderResolverTest
    {
        [Theory]
        [InlineData("marko.nose@gmail.com", EmailProviderType.Google)]
        [InlineData("marko.nose@google.com", EmailProviderType.Google)]
        [InlineData("marko.nose@googlemail.com", EmailProviderType.Google)]
        [InlineData("marko.nose@cracked.com", EmailProviderType.Google)]
        [InlineData("marko.nose@hotmail.com", EmailProviderType.Microsoft)]
        [InlineData("marko.nose@microsoft.com", EmailProviderType.Microsoft)]
        [InlineData("marko.nose@onmicrosoft.com", EmailProviderType.Microsoft)]
        [InlineData("marko.nose@onmicrosoft.de", EmailProviderType.Microsoft)]
        [InlineData("marko.nose@outlook.com", EmailProviderType.Microsoft)]
        [InlineData("marko.nose@pharmalinea.com", EmailProviderType.Microsoft)]
        [InlineData("marko.nose@domenca.com", EmailProviderType.Other)]
        public async Task ValidEmailTest(string email, EmailProviderType type)
        {
            var actualType = await EmailProviderResolver.GetByEmailAsync(email);
            Assert.Equal(type, actualType);
        }

        [Fact]
        public async Task ArgumentExceptionTest()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await EmailProviderResolver.GetByEmailAsync(""));
        }

        [Fact]
        public async Task ArgumentNullExceptionTest()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await EmailProviderResolver.GetByEmailAsync(null));
        }

        [Fact]
        public async Task DnsResponseExceptionTest()
        {
            await Assert.ThrowsAsync<DnsResponseException>(async () => await EmailProviderResolver.GetByEmailAsync("totally@fakeemail123"));
        }

        [Fact]
        public async Task FormatExceptionExceptionTest()
        {
            await Assert.ThrowsAsync<FormatException>(async () => await EmailProviderResolver.GetByEmailAsync("$&*%("));
        }
    }
}
