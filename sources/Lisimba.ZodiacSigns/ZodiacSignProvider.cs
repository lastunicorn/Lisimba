using System.Drawing;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Enums;
using Lisimba.ZodiacSigns.Properties;

namespace Lisimba.ZodiacSigns
{
    public class ZodiacSignProvider : IZodiacSignProvider
    {
        public Image GetZodiacImage(ZodiacSign zodiacSign)
        {
            switch (zodiacSign)
            {
                case ZodiacSign.Aquarius:
                    return Resources.Aquarius;

                case ZodiacSign.Pisces:
                    return Resources.Pisces;

                case ZodiacSign.Aries:
                    return Resources.Aries;

                case ZodiacSign.Taurus:
                    return Resources.Taurus;

                case ZodiacSign.Gemini:
                    return Resources.Gemini;

                case ZodiacSign.Cancer:
                    return Resources.Cancer;

                case ZodiacSign.Leo:
                    return Resources.Leo;

                case ZodiacSign.Virgo:
                    return Resources.Virgo;

                case ZodiacSign.Libra:
                    return Resources.Libra;

                case ZodiacSign.Scorpio:
                    return Resources.Scorpio;

                case ZodiacSign.Sagittarius:
                    return Resources.Sagittarius;

                case ZodiacSign.Capricorn:
                    return Resources.Capricorn;

                default:
                    return null;
            }
        }
    }
}
