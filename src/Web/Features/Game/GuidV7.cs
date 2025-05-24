using System.Security.Cryptography;

namespace ProjectGeoShot.Web.Features.Game;

public static class GuidV7
{
    public static Guid NewGuid()
    {
        Span<byte> bytes = stackalloc byte[16];
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        bytes[0] = (byte)(timestamp >> 40);
        bytes[1] = (byte)(timestamp >> 32);
        bytes[2] = (byte)(timestamp >> 24);
        bytes[3] = (byte)(timestamp >> 16);
        bytes[4] = (byte)(timestamp >> 8);
        bytes[5] = (byte)timestamp;
        bytes[6] = (byte)(0x70 | ((timestamp >> 56) & 0x0F));
        RandomNumberGenerator.Fill(bytes.Slice(7));
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80); // RFC 4122 variant
        return new Guid(bytes);
    }
}
