public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; } // Nueva propiedad para almacenar la cantidad
    public byte[]? Image { get; set; } // Nueva propiedad para almacenar la imagen
}