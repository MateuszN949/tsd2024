using Newtonsoft.Json;
using System.Xml.Serialization;

namespace GoldSavings.App.Model;

public class GoldPrice
{
    [JsonProperty("Data")]
    public DateTime Date { get; set; }

    [JsonProperty("Cena")]
    public double Price { get; set; }

    public static void SaveToXml(List<GoldPrice> prices, string filePath)
{
    var serializer = new XmlSerializer(typeof(List<GoldPrice>));

        using var writer = new StreamWriter(filePath);
        serializer.Serialize(writer, prices);
    }

    public static List<GoldPrice> LoadFromXml(string filePath) => (List<GoldPrice>)new XmlSerializer(typeof(List<GoldPrice>)).Deserialize(new StreamReader(filePath));
}