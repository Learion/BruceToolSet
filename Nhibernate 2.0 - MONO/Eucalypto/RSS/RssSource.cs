/****************************************************************************
Modification History:
*****************************************************************************
Date		Author		Description
*****************************************************************************
10/09/2006	brian.kuhn		Created RssSource Class
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
    /// Represents the source channel that an item came from
    /// </summary>
    [Serializable()]
    public class RssSource
    {
        //============================================================
        //	PUBLIC/PRIVATE/PROTECTED MEMBERS
        //============================================================
        #region PRIVATE/PROTECTED/PUBLIC MEMBERS
        /// <summary>
        /// Private member to hold unique identifier for source
        /// </summary>
        private Guid sourceId           = Guid.NewGuid();
        /// <summary>
        /// Private member to hold title of the source
        /// </summary>
        private string sourceTitle      = String.Empty;
        /// <summary>
        /// Private member to hold URI to source
        /// </summary>
        private Uri sourceUrl           = new Uri("http://localhost", UriKind.Absolute);
        #endregion

        //============================================================
        //	CONSTRUCTORS
        //============================================================
        #region RssSource()
        /// <summary>
        /// Default constructor for RssSource class
        /// </summary>
        public RssSource()
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

        #region RssSource(string title, string url)
        /// <summary>
        /// Constructor for RssSource class that initializes class state using supplied parameters
        /// </summary>
        /// <param name="title">Name of the source</param>
        /// <param name="url">URI that points to the source</param>
        public RssSource(string title, string url)
        {
            //------------------------------------------------------------
            //	Attempt to initialize class state
            //------------------------------------------------------------
            try
            {
                //------------------------------------------------------------
                //	Initialize class state by setting properties
                //------------------------------------------------------------
                this.Title  = title;
                this.Url    = url;
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
        /// Gets or sets unique identifier for the source.
        /// </summary>
        /// <value>Unique identifier for the source</value>
        [XmlAttribute(AttributeName = "id", DataType = "string")]
        public string Id
        {
            get
            {
                return sourceId.ToString();
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
                    sourceId = new Guid(value.Trim());
                }
            }
        }
        #endregion

        #region Title
        /// <summary>
        /// Gets or sets title of the source.
        /// </summary>
        /// <value>Title of the source</value>
        [XmlText(DataType = "string", Type = typeof(System.String))]
        public string Title
        {
            get
            {
                return sourceTitle;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Emtpy string", "value");
                }
                else
                {
                    sourceTitle = value.Trim();
                }
            }
        }
        #endregion

        #region Url
        /// <summary>
        /// Gets or sets URI to source.
        /// </summary>
        /// <value>URI to source</value>
        [XmlAttribute(AttributeName = "url", DataType = "string")]
        public string Url
        {
            get
            {
                return sourceUrl.AbsoluteUri;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Emtpy string", "value");
                }
                else
                {
                    sourceUrl = new Uri(value.Trim(), UriKind.Absolute);
                }
            }
        }
        #endregion
    }
}
