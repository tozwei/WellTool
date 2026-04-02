using Org.BouncyCastle.Asn1;

namespace WellTool.Crypto
{
    public static class ASN1Util
    {
        public static byte[] Encode(Asn1Encodable obj)
        {
            return obj.GetEncoded();
        }

        public static T Decode<T>(byte[] data) where T : Asn1Encodable
        {
            var asn1Object = Asn1Object.FromByteArray(data);
            return asn1Object.ToAsn1Object() as T;
        }

        public static Asn1Object Parse(byte[] data)
        {
            return Asn1Object.FromByteArray(data);
        }
    }
}