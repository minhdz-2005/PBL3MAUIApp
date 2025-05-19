using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Converters;

public class StatusToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is string status)
        {
            switch (status)
            {
                case "Đang mở":
                    return Color.FromArgb("#90EE90"); // Xanh lá nhạt
                case "Đã đóng":
                    return Color.FromArgb("#FFB6C1"); // Hồng nhạt
                case "Sắp mở":
                    return Color.FromArgb("#ADD8E6"); // Xanh dương nhạt
                default:
                    return Color.FromArgb("#E6E6E6"); // Xám nhạt
            }
        }
        return Color.FromArgb("#E6E6E6");
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}