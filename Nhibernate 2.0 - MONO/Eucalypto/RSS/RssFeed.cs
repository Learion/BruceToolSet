/****************************************************************************
Modification History:
*****************************************************************************
Date		Author		Description
*****************************************************************************
10/06/2006	brian.kuhn		Created RssFeed Class
11/02/2006  brian.kuhn      Added support for XML namespaces and trackback
12/05/2006  DavideIcardi    Modified GetFeed method to support also direct stream reading and added a WriteToStream method
14/Feb/2007 Davide.Icardi   Added the XmlIgnore to the Id Property
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

//using SyndicationLibrary.Properties;

namespace SyndicationLibrary.RSS
{
    /// <summary>
    /// Represents an RSS syndication feed
    /// </summary>
    [Serializable()]
    [XmlRoot(ElementName = "rss")]
    public class RssFeed
    {
        //============================================================
        //	PUBLIC/PRIVATE/PROTECTED MEMBERS
        //============================================================
        #region PRIVATE/PROTECTED/PUBLIC MEMBERS
        /// <summary>
        /// Private member to hold unique identifier for feed
        /// </summary>
        private Guid feedId                         = Guid.NewGuid();
        /// <summary>
        /// Private member to hold version of RSS this feed conforms to
        /// </summary>
        private string rssVersion                   = "2.0";
        /// <summary>
        /// Private member to hold channel for this feed
        /// </summary>
        private RssChannel rssChannel               = new RssChannel();
        /// <summary>
        /// Private member to hold XML namespaces associated to the feed.
        /// </summary>
        private XmlSerializerNamespaces namespaces  = new XmlSerializerNamespaces();
        #endregion

        //============================================================
        //	CONSTRUCTORS
        //============================================================
        #region RssFeed()
        /// <summary>
        /// Default constructor for RssFeed class
        /// </summary>
        public RssFeed()
        {
            //------------------------------------------------------------
            //	Attempt to initialize class state
            //------------------------------------------------------------
            try
            {
                //------------------------------------------------------------
                //	Add default XML namespaces
                //------------------------------------------------------------
                namespaces.Add("trackback", "http://madskills.com/public/xml/rss/module/trackback/");   // See http://www.rssboard.org/trackback
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

        #region RssFeed(RssChannel channel)
        /// <summary>
        /// Constructor for RssFeed class that initializes class state using supplied parameter
        /// </summary>
        /// <param name="channel">Channel for this feed</param>
        public RssFeed(RssChannel channel)
        {
            //------------------------------------------------------------
            //	Attempt to initialize class state
            //------------------------------------------------------------
            try
            {
                //------------------------------------------------------------
                //	Add default XML namespaces
                //------------------------------------------------------------
                namespaces.Add("trackback", "http://madskills.com/public/xml/rss/module/trackback/");   // See http://www.rssboard.org/trackback

                //------------------------------------------------------------
                //	Initialize class state by setting properties
                //------------------------------------------------------------
                this.Channel    = channel;
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
        #region Channel
        /// <summary>
        /// Gets or sets the channel for this feed
        /// </summary>
        [XmlElement(ElementName = "channel", Type = typeof(RssChannel))]
        public RssChannel Channel
        {
            get
            {
                return rssChannel;
            }

            set
            {
                rssChannel  = value;
            }
        }
        #endregion

        #region Id
        /// <summary>
        /// Gets or sets unique identifier for the channel.
        /// </summary>
        /// <value>Unique identifier for the channel</value>
        [XmlIgnore()]
        public string Id
        {
            get
            {
                return feedId.ToString();
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
                    feedId = new Guid(value.Trim());
                }
            }
        }
        #endregion

        #region Namespaces
        /// <summary>
        /// Gets or sets collection of XML namespaces associated to the feed.
        /// </summary>
        [XmlNamespaceDeclarations()]
        public XmlSerializerNamespaces Namespaces
        {
            get
            {
                return namespaces;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    namespaces  = value;
                }
            }
        }
        #endregion

        #region Version
        /// <summary>
        /// Gets the version of RSS this feed conforms to
        /// </summary>
        /// <value>Version of RSS this feed conforms to</value>
        [XmlAttribute(AttributeName = "version", DataType = "string")]
        public string Version
        {
            get
            {
                return rssVersion;
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
                    rssVersion = value.Trim();
                }
            }
        }
        #endregion

        //============================================================
        //	PUBLIC ROUTINES
        //============================================================
        #region GetFeed(Uri uri)
        /// <summary>
        /// Returns an RssFeed instance using reading the stream specified
        /// </summary>
        /// <param name="stream">stream of the feed to load</param>
        /// <returns>RssFeed instance</returns>
        public static RssFeed GetFeed(Stream stream)
        {
            //------------------------------------------------------------
            //	Create XmlSerializer to format RSS data
            //------------------------------------------------------------
            XmlSerializer serializer = new XmlSerializer(typeof(RssFeed));

            //------------------------------------------------------------
            //	Deserialize retrieved data
            //------------------------------------------------------------
            return serializer.Deserialize(stream) as RssFeed;
        }

        /// <summary>
        /// Returns an RssFeed instance using the feed located at the specified URI
        /// </summary>
        /// <param name="uri">URI of the feed to load</param>
        /// <returns>RssFeed instance</returns>
        public static RssFeed GetFeed(Uri uri)
        {
            //------------------------------------------------------------
            //	Create WebClient to retrieve feed information
            //------------------------------------------------------------
            using (WebClient webClient = new WebClient())
            {
                //------------------------------------------------------------
                //	Download the feed data
                //------------------------------------------------------------
                byte[] data = webClient.DownloadData(uri);

                //------------------------------------------------------------
                //	Create MemoryStream against retrieved data
                //------------------------------------------------------------
                using (MemoryStream stream = new MemoryStream(data))
                {
                    return GetFeed(stream);
                }
            }
        }
        #endregion

        #region ToString()
        /// <summary>
        /// Serializes the  current RssFeed into an XML string.
        /// </summary>
        /// <returns>An XML string representing the RssFeed, as a valid RSS feed.</returns>
        /// <remarks>Default encoding is UTF8.</remarks>
        public override string ToString()
        {
            //------------------------------------------------------------
            //	Attempt to initialize class state
            //------------------------------------------------------------
            try
            {
                //------------------------------------------------------------
                //	Return serialized result using default encoding
                //------------------------------------------------------------
                return this.ToString(Encoding.UTF8);
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

        #region ToString(Encoding encoding)
        /// <summary>
        /// Serializes the  current RssFeed into an XML string using the specified encoding.
        /// </summary>
        /// <param name="encoding">Encoding to use when serializing RssFeed.</param>
        /// <returns>An XML string representing the RssFeed, as a valid RSS feed using provided encoding.</returns>
        public string ToString(Encoding encoding)
        {
            //------------------------------------------------------------
            //	Local members
            //------------------------------------------------------------
            string xmlData              = String.Empty;

            //------------------------------------------------------------
            //	Create memory stream to hold serialized data
            //------------------------------------------------------------
            using (MemoryStream stream = new MemoryStream())
            {
                WriteToStream(stream);

                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                //------------------------------------------------------------
                //	Generate result
                //------------------------------------------------------------
                xmlData = encoding.GetString(stream.ToArray());
            }


            //------------------------------------------------------------
            //	Return result
            //------------------------------------------------------------
            return xmlData;
        }

        public void WriteToStream(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(RssFeed));

            serializer.Serialize(stream, this);
        }
        #endregion
    }
}
