using UnityEngine;

public class ColorLibrary
{
    public static Color ReadH(float hue, Color color)
    {
        float oldHue, oldSaturation, oldValue, alpha;

        Color.RGBToHSV(color, out oldHue, out oldSaturation, out oldValue);
        alpha = color.a;

        Color newColor = Color.HSVToRGB(hue, oldSaturation, oldValue);
        newColor.a = alpha;

        return newColor;


    }

    public static Color ReadSV(float saturation, float value, float alpha, Color color)
    {
        float oldHue, oldSaturation, oldValue;

        Color.RGBToHSV(color, out oldHue, out oldSaturation, out oldValue);

        Color newColor = Color.HSVToRGB(oldHue, saturation, value);
        newColor.a = alpha;

        return newColor;

    }

    public static void ReadSV(Color colorTo, Color colorFrom)
    {
        float oldHue, oldSaturation, oldValue;
        Color.RGBToHSV(colorTo, out oldHue, out oldSaturation, out oldValue);

        float newHue, newSaturation, newValue;
        Color.RGBToHSV(colorFrom, out newHue, out newSaturation, out newValue);

        Color newColor = Color.HSVToRGB(oldHue, newSaturation, newValue);
        newColor.a = colorTo.a;

        colorTo = newColor;

    }


}
