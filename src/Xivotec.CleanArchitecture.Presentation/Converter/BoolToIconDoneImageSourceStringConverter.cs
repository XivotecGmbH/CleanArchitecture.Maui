using System.Globalization;

namespace Xivotec.CleanArchitecture.Presentation.Converter;

public class BoolToIconDoneImageSourceStringConverter : IValueConverter
{
    private const string IconDoneLightTheme = "icon_done.png";
    private const string IconDoneDarkTheme = "icon_done_white.png";

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var isBoxChecked = (bool)value;
        var isLightTheme = Microsoft.Maui.Controls.Application.Current!.UserAppTheme.Equals(AppTheme.Light);

        return isBoxChecked switch
        {
            false => string.Empty,
            true when isLightTheme => IconDoneLightTheme,
            _ => IconDoneDarkTheme
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((string)value).Equals(IconDoneLightTheme) || ((string)value).Equals(IconDoneDarkTheme);
    }
}