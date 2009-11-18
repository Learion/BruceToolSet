/****************************************************************************
Modification History:
*****************************************************************************
Date		Author		Description
*****************************************************************************
10/09/2006	brian.kuhn		Created RssEnclosure Class
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

//using SyndicationLibrary.Properties;

namespace SyndicationLibrary.RSS
{
    /// <summary>
    /// Represents a media object that can be associated to an item
    /// </summary>
    [Serializable()]
    public class RssEnclosure
    {
        //============================================================
        //	PUBLIC/PRIVATE/PROTECTED MEMBERS
        //============================================================
        #region PRIVATE/PROTECTED/PUBLIC MEMBERS
        /// <summary>
        /// Private member to hold unique identifier for enclosure
        /// </summary>
        private Guid enclosureId        = Guid.NewGuid();
        /// <summary>
        /// Private member to hold length of enclosure in bytes
        /// </summary>
        private int enclosureLength     = 0;
        /// <summary>
        /// Private member to hold standard MIME type of enclosure
        /// </summary>
        private string enclosureType    = String.Empty;
        /// <summary>
        /// Private member to hold URI that points to enclosure
        /// </summary>
        private Uri enclosureUrl        = new Uri("http://localhost", UriKind.Absolute);
        #endregion

        //============================================================
        //	CONSTRUCTORS
        //============================================================
        #region RssEnclosure()
        /// <summary>
        /// Default constructor for RssEnclosure class
        /// </summary>
        public RssEnclosure()
        {
            //------------------------------------------------------------
            //	Attempt to initialize class state
            //------------------------------------------------------------
            try
            {
                //------------------------------------------------------------
                //	
                //------------------------------------------------------------
                
            }
            catch
            {
                //------------------------------------------------------------
                //	Rethrow exception
                //------------------------------------------------------------
                throw;
            }
        }
        #endregion

        #region RssEnclosure(string url, int length, string type)
        /// <summary>
        /// Constructor for RssEnclosure class that initializes class state using supplied parameters
        /// </summary>
        /// <param name="url">URI that points to enclosure</param>
        /// <param name="length">Length of enclosure in bytes</param>
        /// <param name="type">Standard MIME type of enclosure</param>
        public RssEnclosure(string url, int length, string type)
        {
            //------------------------------------------------------------
            //	Attempt to initialize class state
            //------------------------------------------------------------
            try
            {
                //------------------------------------------------------------
                //	Initialize class state by setting properties
                //------------------------------------------------------------
                this.Length     = length;
                this.Type       = type;
                this.Url        = url;
            }
            catch
            {
                //------------------------------------------------------------
                //	Rethrow exception
                //------------------------------------------------------------
                throw;
            }
        }
        #endregion

        //============================================================
        //	PUBLIC PROPERTIES
        //============================================================
        #region Id
        /// <summary>
        /// Gets or sets unique identifier for the enclosure.
        /// </summary>
        /// <value>Unique identifier for the enclosure</value>
        [XmlAttribute(AttributeName = "id", DataType = "string")]
        public string Id
        {
            get
            {
                return enclosureId.ToString();
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Empty string", "value");
                }
                else
                {
                    enclosureId = new Guid(value.Trim());
                }
            }
        }
        #endregion

        #region Length
        /// <summary>
        /// Gets or sets length of enclosure in bytes.
        /// </summary>
        /// <value>Length of enclosure in bytes</value>
        [XmlAttribute(AttributeName = "length", DataType = "int")]
        public int Length
        {
            get
            {
                return enclosureLength;
            }

            set
            {
                enclosureLength = value;
            }
        }
        #endregion

        #region Type
        /// <summary>
        /// Gets or sets standard MIME type of enclosure.
        /// </summary>
        /// <value>Standard MIME type of enclosure</value>
        [XmlAttribute(AttributeName = "type", DataType = "string")]
        public string Type
        {
            get
            {
                return enclosureType;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Empty string", "value");
                }
                else
                {
                    enclosureType = value.Trim();
                }
            }
        }
        #endregion

        #region Url
        /// <summary>
        /// Gets or sets URI to the enclosure.
        /// </summary>
        /// <value>URI to the enclosure</value>
        [XmlAttribute(AttributeName = "url", DataType = "string")]
        public string Url
        {
            get
            {
                return enclosureUrl.AbsoluteUri;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Empty string", "value");
                }
                else
                {
                    enclosureUrl = new Uri(value.Trim(), UriKind.Absolute);
                }
            }
        }
        #endregion
    }
}
