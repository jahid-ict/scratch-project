namespace ScratchProject.Api.DataTransferObjects
{
    public class CarModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }

        public override string ToString()
        {
           var type = GetType();
           var properties = type.GetProperties();
            var result = "";
           foreach ( var property in properties )
            {
                result += $"{property.Name}: {property.GetValue(this)}";
            }
           return result;
        }

    }
}
