namespace HelloWorld.Models
{
    public class Computer
    {
        public string Motherboard { get; set; } = "";
        public int CPUcores { get; set; }
        public bool Haswifi { get; set; }
        public bool HasLTE { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string VideoCard { get; set; } = "";
    }

}