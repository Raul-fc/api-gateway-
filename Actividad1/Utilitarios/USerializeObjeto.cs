using Newtonsoft.Json;

namespace Actividad1.Utilitarios
{
    public class USerializeObjeto
    {
        public static List<T> deserializeJsom_ObjetoList<T>(String dataJsom)
        {
            List<T> deserializedProduct = JsonConvert.DeserializeObject<List<T>>(dataJsom);

            return deserializedProduct;
        }
    }
}
