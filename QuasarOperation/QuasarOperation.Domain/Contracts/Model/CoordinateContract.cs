namespace QuasarOperation.Domain.Contracts.Model
{
    /// <summary>
    /// Coordenada de ubicación utilizando eje X  e  Y en el espacio
    /// </summary>
    public class CoordinateContract
    {
        public CoordinateContract(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

    }
}