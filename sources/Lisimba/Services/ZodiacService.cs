using System.Drawing;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Properties;

namespace DustInTheWind.Lisimba.Services
{
    public class ZodiacService
    {
        public Image GetZodiacImage(ZodiacSign zodiacSign)
        {
            Image img = null;

            switch (zodiacSign)
            {
                case ZodiacSign.Aquarius:
                    img = Resources.Aquarius;
                    break;

                case ZodiacSign.Pisces:
                    img = Resources.Pisces;
                    break;

                case ZodiacSign.Aries:
                    img = Resources.Aries;
                    break;

                case ZodiacSign.Taurus:
                    img = Resources.Taurus;
                    break;

                case ZodiacSign.Gemini:
                    img = Resources.Gemini;
                    break;

                case ZodiacSign.Cancer:
                    img = Resources.Cancer;
                    break;

                case ZodiacSign.Leo:
                    img = Resources.Leo;
                    break;

                case ZodiacSign.Virgo:
                    img = Resources.Virgo;
                    break;

                case ZodiacSign.Libra:
                    img = Resources.Libra;
                    break;

                case ZodiacSign.Scorpio:
                    img = Resources.Scorpio;
                    break;

                case ZodiacSign.Sagittarius:
                    img = Resources.Sagittarius;
                    break;

                case ZodiacSign.Capricorn:
                    img = Resources.Capricorn;
                    break;
            }

            return img;
        }

        public string GetZodiacSignName(ZodiacSign zodiacSign)
        {
            return zodiacSign.ToString();
        }
    }
}
