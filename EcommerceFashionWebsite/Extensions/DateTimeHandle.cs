namespace EcommerceFashionWebsite.Extensions
{
    public class DateTimeHandle
    {
        public string ConvertDateTimeToDDMMYYYY(DateTime dateTime) {
            return String.Format("{0:dd/MM/yyyy}", dateTime);
        }
    }
}
