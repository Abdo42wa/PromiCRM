using Newtonsoft.Json;
namespace PromiCore.ModelsDTO
{
    /// <summary>
    /// This is basically for catch block. so we dont have to repeat 
    /// same error messages in controllers catc blocks. SerializeObject
    /// serializes specified object(this object) to JSON string
    /// </summary>
    public class Error
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
