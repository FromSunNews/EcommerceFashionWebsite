namespace EcommerceFashionWebsite.Extensions
{
    public class FormatHandle
    {
        public string ConvertDateTimeToDDMMYYYY(DateTime dateTime) {
            return String.Format("{0:dd/MM/yyyy}", dateTime);
        }

        public string FormatNumber(int number)
        {
            return string.Format("{0:N0}", number); // "N0" định dạng số nguyên với dấu chấm làm dấu ngăn cách
        }
    }
}
