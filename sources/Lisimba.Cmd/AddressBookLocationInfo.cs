using System;
using System.Text;

namespace Lisimba.Cmd
{
    internal class AddressBookLocationInfo
    {
        public string FileName { get; set; }
        public string GateId { get; set; }

        public AddressBookLocationInfo()
        {
        }

        public AddressBookLocationInfo(string text)
        {
            if (text == null) throw new ArgumentNullException("text");

            Parse(text);
        }

        private void Parse(string text)
        {
            var parts = text.Split(';');
            if (parts.Length == 0)
                throw new ApplicationException("Invalid 'LastAddressBook' value in configuration file.");

            if (parts.Length >= 1)
            {
                FileName = parts[0].Trim();

                if (FileName.Length == 0)
                    throw new ApplicationException("Invalid 'LastAddressBook' value in configuration file.");
            }

            if (parts.Length >= 2)
            {
                GateId = parts[1].Trim();

                if (GateId.Length == 0)
                    throw new ApplicationException("Invalid 'LastAddressBook' value in configuration file.");
            }
        }

        public override string ToString()
        {
            if (FileName == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            sb.Append(FileName);

            if (GateId != null)
                sb.Append(";").Append(GateId);

            return sb.ToString();
        }
    }
}