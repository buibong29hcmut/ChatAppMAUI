using System.Text.Json;

namespace ChatApp.API.Middlewares
{
    public class ErrorDetailModel
    {
        public int StatusCode { get; set; }
        public List<string> Messages { get; set; }
        public ErrorDetailModel(int statuscode, List<string> messages)
        {
            StatusCode = statuscode;
            Messages = messages;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
