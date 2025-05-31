namespace Perpustakaan.Models
{
    public class KoleksiBuku
    {
        public int Id { get; set; }

        public string Judul { get; set; }

        public int PenulisId { get; set; }
        public PenulisBuku Penulis { get; set; }

        public int GenreId { get; set; }
        public GenreBuku Genre { get; set; }

        public DateOnly TahunTerbit { get; set; }
    }
}