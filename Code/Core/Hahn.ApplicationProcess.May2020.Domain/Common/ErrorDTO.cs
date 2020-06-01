namespace Hahn.ApplicationProcess.May2020.Domain.Common
{
    public class ErrorDTO
    {
        public ErrorDTO() { }
        public ErrorDTO(string error)
        {
            Error = error;
        }

        public string Error { get; set; }
        public object MoreInfo { get; set; }
    }
}
