using OneInc.Common.Model;

namespace OneInc.DAL.Repository
{
    public interface IEncodedStringsRepo
    {
        void CreatedEncodeString(EncodedString encodedString);
        EncodedString? GetEncodedStringById(string id);
    }
}
