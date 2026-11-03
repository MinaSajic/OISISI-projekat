namespace SajamKnjiga.Core.Serialization;

//  Preuzeto sa materijala sa vezbi 3
public interface ISerializable
{
    string[] ToCSV();

    void FromCSV(string[] values);
}