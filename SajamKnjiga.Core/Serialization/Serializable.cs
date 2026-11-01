namespace SerializationExample.Serialization;

//  Preuzeto sa materijala sa vezbi 3
public interface ISerializable
{
    string[] ToCSV();

    void FromCSV(string[] values);
}