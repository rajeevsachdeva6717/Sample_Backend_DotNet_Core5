using Sample.DataContract.Enums;

namespace Sample.DataContract.Models
{
    public class APIResponse
    {
        public APIResponse(StatusCodesEnum status, object result, string fileType = null)
        {
            StatusCode = status;
            Result = result;
            FileType = fileType;
        }

        public APIResponse(StatusCodesEnum status)
        {
            StatusCode = status;
        }

        public object Result { get; set; }
        public StatusCodesEnum StatusCode { get; set; }
        public string FileType { get; set; }
    }
}
