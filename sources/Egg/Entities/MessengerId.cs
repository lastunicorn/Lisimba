using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DustInTheWind.Lisimba.Egg
{
    /// <summary>
    /// Class containing information about a messenger id.
    /// </summary>
    [Serializable()]
    [XmlRoot("MessengerId")]
    public class MessengerId
    {
        #region Fields

		private string id;
		private string description;

		#endregion Fields

		#region Properties

		/// <summary>
        /// The messenger id.
        /// </summary>
		[XmlAttribute("Id")]
		public string Id
		{
			get { return this.id; }
            set { this.id = value; this.OnIdChanged(new IdChangedEventArgs(value)); }
		}

        /// <summary>
        /// A short description of the e-mail address.
        /// </summary>
        [XmlAttribute("Description")]
		public string Description
		{
			get { return this.description; }
            set { this.description = value; this.OnDescriptionChanged(new DescriptionChangedEventArgs(value)); }
		}

		#endregion Properties

        #region Events

        #region Event IdChanged

        public event IdChangedHandler IdChanged;
        public delegate void IdChangedHandler(object sender, IdChangedEventArgs e);

        public class IdChangedEventArgs : EventArgs
        {
            private string newValue;

            public string NewValue
            {
                get { return newValue; }
            }
	
            public IdChangedEventArgs(string newValue)
            {
                this.newValue = newValue;
            }
        }

        protected void OnIdChanged(IdChangedEventArgs e)
        {
            if (IdChanged != null)
            {
                IdChanged(this, e);
            }
        }

        #endregion Event IdChanged

        #region Event DescriptionChanged

        public event DescriptionChangedHandler DescriptionChanged;
        public delegate void DescriptionChangedHandler(object sender, DescriptionChangedEventArgs e);

        public class DescriptionChangedEventArgs : EventArgs
        {
            private string newValue;

            public string NewValue
            {
                get { return newValue; }
            }

            public DescriptionChangedEventArgs(string newValue)
            {
                this.newValue = newValue;
            }
        }

        protected void OnDescriptionChanged(DescriptionChangedEventArgs e)
        {
            if (DescriptionChanged != null)
            {
                DescriptionChanged(this, e);
            }
        }

        #endregion Event DescriptionChanged

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new empty MessengerId object.
        /// </summary>
		public MessengerId()
            : this(string.Empty, string.Empty)
		{
		}

        /// <summary>
        /// Creates a new MessengerId object with the id specified. The description text is an empty string.
        /// </summary>
        /// <param name="address"></param>
		public MessengerId(string id)
			:this(id, string.Empty)
		{
		}

        /// <summary>
        /// Creates a new MessengerId object with the id and description specified.
        /// </summary>
        /// <param name="address">The e-mail address</param>
        /// <param name="description">A short description of the email address.</param>
		public MessengerId(string id, string description)
		{
			this.id = id;
			this.description = description;
		}

        /// <summary>
        /// Creates a new MessengerId object with the data copied from the one passed as parameter.
        /// </summary>
        /// <param name="email"></param>
        public MessengerId(MessengerId messenger)
		{
			this.CopyFrom(messenger);
		}

		#endregion Constructors

        #region public void Clear()

        /// <summary>
        /// Removes the data from all the fields
        /// </summary>
		public void Clear()
		{
            this.id = string.Empty;
            this.description = string.Empty;
        }

        #endregion

        #region public void CopyFrom(MessengerId messenger)

        /// <summary>
        /// Copy the data from the MessengerId object passed as parameter into the current object.
        /// </summary>
        /// <param name="email"></param>
        public void CopyFrom(MessengerId messenger)
		{
			this.id = messenger.id;
			this.description = messenger.description;
		}

        #endregion

        #region public override bool Equals(object obj)

        public override bool Equals(object obj)
        {
            if (!(obj is MessengerId)) return false;

            MessengerId messengerId = (MessengerId)obj;

            if (!id.Equals(messengerId.id)) return false;
            if (!description.Equals(messengerId.description)) return false;

            return true;
        }

        #endregion

        #region public override string ToString()

        public override string ToString()
        {
            return this.Id + (this.description.Length > 0 ? " - " + this.description : string.Empty);
        }

        #endregion
    }
}
