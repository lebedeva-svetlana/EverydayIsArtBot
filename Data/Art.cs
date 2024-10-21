using System.Text.Json.Serialization;

namespace EverydayIsArtBot.Data
{
    public class Art
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("author")]
        public string[] Author { get; set; }

        [JsonPropertyName("placeOfOrigin")]
        public string[] PlaceOfOrigin { get; set; }

        [JsonPropertyName("medium")]
        public string[] Medium { get; set; }

        [JsonPropertyName("accessNumber")]
        public string AccessNumber { get; set; }

        [JsonPropertyName("wayToGet")]
        public string[] WayToGet { get; set; }

        [JsonPropertyName("description")]
        public string[] Description { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("sourceUrl")]
        public string SourceUrl { get; set; }

        [JsonPropertyName("sourceUrlText")]
        public string SourceUrlText { get; set; }
    }
}