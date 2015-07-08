using System.Drawing;
using DustInTheWind.Lisimba.Egg.Enums;

namespace DustInTheWind.Lisimba.Egg
{
    public interface IZodiacSignProvider
    {
        Image GetZodiacImage(ZodiacSign zodiacSign);
    }
}