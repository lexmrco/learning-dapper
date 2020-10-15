namespace Tekus.Entidades
{
    public class Servicio: EntidadBase
    {
        public string Nombre { get; set; }
        public decimal ValorHora { get; set; }
        public int ClienteID { get; set; }
        public Cliente Cliente { get; set; }
        public int PaisID { get; set; }
        public Pais Pais { get; set; }
    }
}
