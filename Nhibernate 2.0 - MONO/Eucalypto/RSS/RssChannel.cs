/****************************************************************************
Modification History:
*****************************************************************************
Date		Author		Description
*****************************************************************************
10/06/2006	brian.kuhn		Created RssChannel Class
14/Feb/2007 Davide.icardi   Added the XmlIgnore to the Id property
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
    /// Represents an RSS channel
    /// </summary>
    [Serializable()]
    public class RssChannel
    {
        //============================================================
        //	PUBLIC/PRIVATE/PROTECTED MEMBERS
        //============================================================
        #region PRIVATE/PROTECTED/PUBLIC MEMBERS
        /// <summary>
        /// Private member to hold unique identifier for channel
        /// </summary>
        private Guid channelId                              = Guid.NewGuid();
        /// <summary>
        /// Private member to hold collection of categories associated to channel
        /// </summary>
        private Collection<RssCategory> channelCategories   = new Collection<RssCategory>();
        /// <summary>
        /// Private member to hold cloud associated to channel
        /// </summary>
        private RssCloud channelCloud;
        /// <summary>
        /// Private member to hold copyright notice for channel
        /// </summary>
        private string channelCopyright                     = String.Empty;
        /// <summary>
        /// Private member to hold description of the channel
        /// </summary>
        private string channelDescription                   = String.Empty;
        /// <summary>
        /// Private member to hold URI to documentation for the RSS format this channel conforms to
        /// </summary>
        private Uri channelDocumentation                    = new Uri("http://www.rssboard.org/rss-specification", UriKind.Absolute);
        /// <summary>
        /// Private member to hold name of program used to generate the channel
        /// </summary>
        private string channelGenerator                     = "RSS Syndication Service";
        /// <summary>
        /// Private member to hold image associated to channel
        /// </summary>
        private RssImage channelImage;
        /// <summary>
        /// Private member to hold collection of items associated to channel
        /// </summary>
        private Collection<RssItem> channelItems            = new Collection<RssItem>();
        /// <summary>
        /// Private member to hold language channel is written in
        /// </summary>
        private string channelLanguage                      = String.Empty;
        /// <summary>
        /// Private member to hold time channel was last updated
        /// </summary>
        private DateTime channelLastBuildDate               = DateTime.Now;
        /// <summary>
        /// Private member to hold URI to web site corresponding to the channel
        /// </summary>
        private Uri channelLink                             = new Uri("http://localhost", UriKind.Absolute);
        /// <summary>
        /// Private member to hold email address and name of person responsible for channel content
        /// </summary>
        private string channelManagingEditor                = String.Empty;
        /// <summary>
        /// Private member to hold date channel is published
        /// </summary>
        private DateTime channelPublicationDate             = DateTime.Now;
        /// <summary>
        /// Private member to hold PICS rating for the channel
        /// </summary>
        private string channelRating                        = String.Empty;
        /// <summary>
        /// Private member to hold number of minutes channel can be cached
        /// </summary>
        private int channelTimeToLive                       = 60;
        /// <summary>
        /// Private member to hold name of the channel
        /// </summary>
        private string channelTitle                         = String.Empty;
        /// <summary>
        /// Private member to hold email address and name of person responsible for channel technical issues
        /// </summary>
        private string channelWebMaster                     = String.Empty;
        #endregion

        //============================================================
        //	CONSTRUCTORS
        //============================================================
        #region RssChannel()
        /// <summary>
        /// Default constructor for RssChannel class
        /// </summary>
        public RssChannel()
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

        #region RssChannel(string title, string description, string link)
        /// <summary>
        /// Constructor for RssChannel class that initialize class state using supplied parameters
        /// </summary>
        /// <param name="title">Name of the channel</param>
        /// <param name="description">Phrase or sentence describing the channel</param>
        /// <param name="link">The URI to the HTML website corresponding to the channel</param>
        public RssChannel(string title, string description, string link)
        {
            //------------------------------------------------------------
            //	Attempt to initialize class state
            //------------------------------------------------------------
            try
            {
                //------------------------------------------------------------
                //	Initialize class state by setting properties
                //------------------------------------------------------------
                this.Description    = description;
                this.Link           = link;
                this.Title          = title;
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
        #region Categories
        /// <summary>
        /// Gets or sets collection of categories associated to channel.
        /// </summary>
        [XmlElement(ElementName = "category", Type = typeof(RssCategory))]
        public Collection<RssCategory> Categories
        {
            get
            {
                return channelCategories;
            }
        }
        #endregion

        #region Cloud
        /// <summary>
        /// Gets or sets the cloud associated to this channel.
        /// </summary>
        /// <value>Cloud associated to this channel</value>
        /// <remarks>Can be set to a null value to indicate no <see cref="RssCloud"/> is associated to the channel.</remarks>
        [XmlElement(ElementName = "cloud", Type = typeof(RssCloud))]
        public RssCloud Cloud
        {
            get
            {
                return channelCloud;
            }

            set
            {
                channelCloud    = value;
            }
        }
        #endregion

        #region Copyright
        /// <summary>
        /// Gets or sets copyright notice for the channel.
        /// </summary>
        /// <value>Copyright notice for the channel</value>
        [XmlElement(ElementName = "copyright", Type = typeof(System.String))]
        public string Copyright
        {
            get
            {
                return channelCopyright;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    channelCopyright = value.Trim();
                }
            }
        }
        #endregion

        #region Description
        /// <summary>
        /// Gets or sets description for the channel.
        /// </summary>
        /// <value>Description for the channel</value>
        [XmlElement(ElementName = "description", Type = typeof(System.String))]
        public string Description
        {
            get
            {
                return channelDescription;
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
                    channelDescription  = value.Trim();
                }
            }
        }
        #endregion

        #region Documentation
        /// <summary>
        /// Gets or sets URI that points to the documentation for the RSS format used in the channel.
        /// </summary>
        /// <value>URI that points to the documentation for the RSS format used in the channel</value>
        [XmlElement(ElementName = "docs", Type = typeof(System.String))]
        public string Documentation
        {
            get
            {
                return channelDocumentation.AbsoluteUri;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    channelDocumentation = new Uri(value.Trim(), UriKind.Absolute);
                }
            }
        }
        #endregion

        #region Generator
        /// <summary>
        /// Gets or sets name of program used to generate the channel.
        /// </summary>
        /// <value>Name of program used to generate the channel.</value>
        [XmlElement(ElementName = "generator", Type = typeof(System.String))]
        public string Generator
        {
            get
            {
                return channelGenerator;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    channelGenerator    = value.Trim();
                }
            }
        }
        #endregion

        #region HasCategories
        /// <summary>
        /// Gets a value indicating if the channel has one or more categories associated to it.
        /// </summary>
        /// <value>If true then channel has one or more categories, otherwise channel has no associated categories.</value>
        [XmlIgnore()]
        public bool HasCategories
        {
            get
            {
                return (channelCategories.Count > 0);
            }
        }
        #endregion

        #region HasCloud
        /// <summary>
        /// Gets a value indicating if the channel has a cloud associated to it.
        /// </summary>
        /// <value>If true then channel has a cloud, otherwise channel has no associated cloud.</value>
        [XmlIgnore()]
        public bool HasCloud
        {
            get
            {
                return (channelCloud != null);
            }
        }
        #endregion

        #region HasImage
        /// <summary>
        /// Gets a value indicating if the channel has a image associated to it.
        /// </summary>
        /// <value>If true then channel has an image, otherwise channel has no associated image.</value>
        [XmlIgnore()]
        public bool HasImage
        {
            get
            {
                return (channelImage != null);
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
                return channelId.ToString();
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
                    channelId   = new Guid(value.Trim());
                }
            }
        }
        #endregion

        #region Image
        /// <summary>
        /// Gets or sets the image associated to this channel.
        /// </summary>
        /// <value>Image associated to this channel</value>
        /// <remarks>Can be set to a null value to indicate no <see cref="RssImage"/> is associated to the channel.</remarks>
        [XmlElement(ElementName = "image", Type = typeof(RssImage))]
        public RssImage Image
        {
            get
            {
                return channelImage;
            }

            set
            {
                channelImage    = value;
            }
        }
        #endregion

        #region Items
        /// <summary>
        /// Gets or sets collection of items associated to channel.
        /// </summary>
        [XmlElement(ElementName = "item", Type = typeof(RssItem))]
        public Collection<RssItem> Items
        {
            get
            {
                return channelItems;
            }
        }
        #endregion

        #region Language
        /// <summary>
        /// Gets or sets language the channel is written in.
        /// </summary>
        /// <value>Language the channel is written in</value>
        /// <example>en-US</example>
        /// <remarks>Listing of permissible language codes can be found at http://www.w3.org/TR/REC-html40/struct/dirlang.html#langcodes</remarks>
        [XmlElement(ElementName = "language", Type = typeof(System.String))]
        public string Language
        {
            get
            {
                return channelLanguage;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    channelLanguage = value.Trim();
                }
            }
        }
        #endregion

        #region LastBuildDate
        /// <summary>
        /// Gets or sets publication date for the content in the channel.
        /// </summary>
        /// <value>Publication date for the content in the channel</value>
        /// <remarks>RFC822 Format</remarks>
        [XmlElement(ElementName = "lastBuildDate", Type = typeof(System.String))]
        public string LastBuildDateStr
        {
            get
            {
                return Rfc822DateTime.ToString(channelLastBuildDate);
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    channelLastBuildDate    = Rfc822DateTime.FromString(value.Trim());
                }
            }
        }

        [XmlIgnore()]
        public DateTime LastBuildDate
        {
            get { return channelLastBuildDate; }
            set { channelLastBuildDate = value; }
        }
        #endregion

        #region Link
        /// <summary>
        /// Gets or sets URI to web site corresponding to the channel.
        /// </summary>
        /// <value>URI to web site corresponding to the channel</value>
        [XmlElement(ElementName = "link", Type = typeof(System.String))]
        public string Link
        {
            get
            {
                return channelLink.AbsoluteUri;
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
                    channelLink = new Uri(value.Trim(), UriKind.Absolute);
                }
            }
        }
        #endregion

        #region ManagingEditor
        /// <summary>
        /// Gets or sets email address and name for person responsible for channel content.
        /// </summary>
        /// <value>Email address and name for person responsible for channel content</value>
        /// <example>managing.editor@domain.com (Managing Editor)</example>
        [XmlElement(ElementName = "managingEditor", Type = typeof(System.String))]
        public string ManagingEditor
        {
            get
            {
                return channelManagingEditor;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    channelManagingEditor = value.Trim();
                }
            }
        }
        #endregion

        #region PublicationDate
        /// <summary>
        /// Gets or sets publication date for the content in the channel.
        /// </summary>
        /// <value>Publication date for the content in the channel</value>
        /// <remarks>RFC822 Format</remarks>
        [XmlElement(ElementName = "pubDate", Type = typeof(System.String))]
        public string PublicationDateStr
        {
            get
            {
                return Rfc822DateTime.ToString(channelPublicationDate);
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    channelPublicationDate = Rfc822DateTime.FromString(value.Trim());
                }
            }
        }

        [XmlIgnore()]
        public DateTime PublicationDate
        {
            get { return channelPublicationDate; }
            set { channelPublicationDate = value; }
        }
        #endregion

        #region Rating
        /// <summary>
        /// Gets or sets PICS rating for the channel.
        /// </summary>
        /// <value>PICS rating for the channel</value>
        [XmlElement(ElementName = "rating", Type = typeof(System.String))]
        public string Rating
        {
            get
            {
                return channelRating;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    channelRating = value.Trim();
                }
            }
        }
        #endregion

        #region TimeToLive
        /// <summary>
        /// Gets or sets number of minutes that indicates how long a channel can be cached before refreshing from the source.
        /// </summary>
        /// <value>Number of minutes that indicates how long a channel can be cached before refreshing from the source.</value>
        [XmlElement(ElementName = "ttl", Type = typeof(System.Int32))]
        public int TimeToLive
        {
            get
            {
                return channelTimeToLive;
            }

            set
            {
                channelTimeToLive   = value;
            }
        }
        #endregion

        #region Title
        /// <summary>
        /// Gets or sets name of the channel.
        /// </summary>
        /// <value>Name of the channel</value>
        [XmlElement(ElementName = "title", Type = typeof(System.String))]
        public string Title
        {
            get
            {
                return channelTitle;
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
                    channelTitle    = value.Trim();
                }
            }
        }
        #endregion

        #region WebMaster
        /// <summary>
        /// Gets or sets email address and name for person responsible for technical issues relating to channel.
        /// </summary>
        /// <value>Email address and name for person responsible for technical issues relating to channel</value>
        /// <example>web.master@domain.com (Web Master)</example>
        [XmlElement(ElementName = "webMaster", Type = typeof(System.String))]
        public string WebMaster
        {
            get
            {
                return channelWebMaster;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    channelWebMaster = value.Trim();
                }
            }
        }
        #endregion
    }
}
