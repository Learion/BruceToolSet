/****************************************************************************
Modification History:
*****************************************************************************
Date		Author		Description
*****************************************************************************
10/08/2006	brian.kuhn		Created RssItem Class
11/02/2006  brian.kuhn      Added support for trackback:about and trackback:ping elements
14/Feb/2007 davide.icardi   Removed the check for description, now can be empty or null
                            Added the XmlIgnore attribute to the id property
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
    /// Represents a discrete item within a channel
    /// </summary>
    [Serializable()]
    public class RssItem
    {
        //============================================================
        //	PUBLIC/PRIVATE/PROTECTED MEMBERS
        //============================================================
        #region PRIVATE/PROTECTED/PUBLIC MEMBERS
        /// <summary>
        /// Private member to hold unique identifier for item
        /// </summary>
        private Guid itemId                             = System.Guid.NewGuid();
        /// <summary>
        /// Private member to hold email address and name for author of item
        /// </summary>
        private string itemAuthor                       = String.Empty;
        /// <summary>
        /// Private member to hold collection of categories associated to channel
        /// </summary>
        private Collection<RssCategory> itemCategories  = new Collection<RssCategory>();
        /// <summary>
        /// Private member to hold URI of a page for comments relating to the item
        /// </summary>
        private Uri itemComments;
        /// <summary>
        /// Private member to hold synopsis of the item
        /// </summary>
        private string itemDescription                  = String.Empty;
        /// <summary>
        /// Private member to hold enclosure associated to this item
        /// </summary>
        private RssEnclosure itemEnclosure;
        /// <summary>
        /// Private member to hold guid associated to this item
        /// </summary>
        private RssGuid itemGuid;
        /// <summary>
        /// Private member to hold URI for the item
        /// </summary>
        private Uri itemLink                            = new Uri("http://localhost", UriKind.Absolute);
        /// <summary>
        /// Private member to hold date of when item was published
        /// </summary>
        private DateTime itemPublicationDate            = DateTime.Now;
        /// <summary>
        /// Private member to hold source associated to this item
        /// </summary>
        private RssSource itemSource;
        /// <summary>
        /// Private member to hold name of the item
        /// </summary>
        private string itemTitle                        = String.Empty;
        /// <summary>
        /// Private member to hold a trackback URL on another site that was pinged in response to the item.
        /// </summary>
        private string trackbackAbout                   = String.Empty;
        /// <summary>
        /// Private member to hold item's trackback URL.
        /// </summary>
        private string trackbackPing                    = String.Empty;
        #endregion

        //============================================================
        //	CONSTRUCTORS
        //============================================================
        #region RssItem()
        /// <summary>
        /// Default constructor for RssItem class
        /// </summary>
        public RssItem()
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

        #region RssItem(string title, string description, string link)
        /// <summary>
        /// Constructor for RssItem class that initialize class state using supplied parameters
        /// </summary>
        /// <param name="title">Name of the item.</param>
        /// <param name="description">Synopsis of the item.</param>
        /// <param name="link">The URI for the item.</param>
        public RssItem(string title, string description, string link)
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
        #region Author
        /// <summary>
        /// Gets or sets email address and name for author of item.
        /// </summary>
        /// <value>Email address and name for author of item</value>
        /// <example>john.doe@domain.com (John Doe)</example>
        [XmlElement(ElementName = "author", Type = typeof(System.String))]
        public string Author
        {
            get
            {
                return itemAuthor;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    itemAuthor = value.Trim();
                }
            }
        }
        #endregion

        #region Categories
        /// <summary>
        /// Gets or sets collection of categories associated to item.
        /// </summary>
        [XmlElement(ElementName = "category", Type = typeof(RssCategory))]
        public Collection<RssCategory> Categories
        {
            get
            {
                return itemCategories;
            }
        }
        #endregion

        #region Comments
        /// <summary>
        /// Gets or sets URI of a page for comments relating to the item.
        /// </summary>
        /// <value>URI of a page for comments relating to the item</value>
        [XmlElement(ElementName = "comments", Type = typeof(System.String))]
        public string Comments
        {
            get
            {
                if (itemComments != null)
                {
                    return itemComments.AbsoluteUri;
                }
                else
                {
                    return String.Empty;
                }
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    itemComments    = new Uri(value.Trim(), UriKind.Absolute);
                }
            }
        }
        #endregion

        #region Description
        /// <summary>
        /// Gets or sets synopsis for the item.
        /// </summary>
        /// <value>Synopsis for the item</value>
        [XmlElement(ElementName = "description", Type = typeof(System.String))]
        public string Description
        {
            get
            {
                return itemDescription;
            }

            set
            {
                itemDescription = value;
            }
        }
        #endregion

        #region Enclosure
        /// <summary>
        /// Gets or sets the enclosure associated to this item.
        /// </summary>
        /// <value>Enclosure associated to this item</value>
        /// <remarks>Can be set to a null value to indicate no <see cref="RssEnclosure"/> is associated to the item.</remarks>
        [XmlElement(ElementName = "enclosure", Type = typeof(RssEnclosure))]
        public RssEnclosure Enclosure
        {
            get
            {
                return itemEnclosure;
            }

            set
            {
                itemEnclosure   = value;
            }
        }
        #endregion

        #region Guid
        /// <summary>
        /// Gets or sets the guid associated to this item.
        /// </summary>
        /// <value>Guid associated to this item</value>
        /// <remarks>Can be set to a null value to indicate no <see cref="RssGuid"/> is associated to the item.</remarks>
        [XmlElement(ElementName = "guid", Type = typeof(RssGuid))]
        public RssGuid Guid
        {
            get
            {
                return itemGuid;
            }

            set
            {
                itemGuid    = value;
            }
        }
        #endregion

        #region HasCategories
        /// <summary>
        /// Gets a value indicating if the item has one or more categories associated to it.
        /// </summary>
        /// <value>If true then item has one or more categories, otherwise item has no associated categories.</value>
        [XmlIgnore()]
        public bool HasCategories
        {
            get
            {
                return (itemCategories.Count > 0);
            }
        }
        #endregion

        #region HasEnclosure
        /// <summary>
        /// Gets a value indicating if the item has an enclosure associated to it.
        /// </summary>
        /// <value>If true then item has an enclosure, otherwise item has no associated enclosure.</value>
        [XmlIgnore()]
        public bool HasEnclosure
        {
            get
            {
                return (itemEnclosure != null);
            }
        }
        #endregion

        #region HasGuid
        /// <summary>
        /// Gets a value indicating if the item has an guid associated to it.
        /// </summary>
        /// <value>If true then item has an guid, otherwise item has no associated guid.</value>
        [XmlIgnore()]
        public bool HasGuid
        {
            get
            {
                return (itemGuid != null);
            }
        }
        #endregion

        #region HasSource
        /// <summary>
        /// Gets a value indicating if the item has a source associated to it.
        /// </summary>
        /// <value>If true then item has a source, otherwise item has no associated source.</value>
        [XmlIgnore()]
        public bool HasSource
        {
            get
            {
                return (itemSource != null);
            }
        }
        #endregion

        #region Id
        /// <summary>
        /// Gets or sets unique identifier for the item.
        /// </summary>
        /// <value>Unique identifier for the item</value>
        [XmlIgnore()]
        public string Id
        {
            get
            {
                return itemId.ToString();
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
                    itemId = new Guid(value.Trim());
                }
            }
        }
        #endregion

        #region Link
        /// <summary>
        /// Gets or sets URI for the item.
        /// </summary>
        /// <value>URI for the item</value>
        [XmlElement(ElementName = "link", Type = typeof(System.String))]
        public string Link
        {
            get
            {
                return itemLink.AbsoluteUri;
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
                    itemLink = new Uri(value.Trim(), UriKind.Absolute);
                }
            }
        }
        #endregion

        #region PublicationDate
        /// <summary>
        /// Gets or sets publication date for when item was published.
        /// </summary>
        /// <value>Publication date for when item was published</value>
        /// <remarks>RFC822 Format</remarks>
        [XmlElement(ElementName = "pubDate", Type = typeof(System.String))]
        public string PublicationDateStr
        {
            get
            {
                return Rfc822DateTime.ToString(itemPublicationDate);
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    itemPublicationDate = Rfc822DateTime.FromString(value.Trim());
                }
            }
        }

        [XmlIgnore()]
        public DateTime PublicationDate
        {
            get { return itemPublicationDate; }
            set { itemPublicationDate = value; }
        }
        #endregion

        #region Source
        /// <summary>
        /// Gets or sets the source associated to this item.
        /// </summary>
        /// <value>Source associated to this item</value>
        /// <remarks>Can be set to a null value to indicate no <see cref="RssSource"/> is associated to the item.</remarks>
        [XmlElement(ElementName = "source", Type = typeof(RssSource))]
        public RssSource Source
        {
            get
            {
                return itemSource;
            }

            set
            {
                itemSource  = value;
            }
        }
        #endregion

        #region Title
        /// <summary>
        /// Gets or sets name of the item.
        /// </summary>
        /// <value>Name of the item</value>
        [XmlElement(ElementName = "title", Type = typeof(System.String))]
        public string Title
        {
            get
            {
                return itemTitle;
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
                    itemTitle = value.Trim();
                }
            }
        }
        #endregion

        #region TrackbackAbout
        /// <summary>
        /// Gets or sets value that identifies a trackback URL on another site that was pinged in response to the item.
        /// </summary>
        /// <value>Trackback URL on another site that was pinged in response to the item.</value>
        /// <remarks>This is an optional element.</remarks>
        [XmlElement(ElementName = "about", Type = typeof(System.String), Namespace = "http://madskills.com/public/xml/rss/module/trackback/")]
        public string TrackbackAbout
        {
            get
            {
                return trackbackAbout;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    trackbackAbout = value.Trim();
                }
            }
        }
        #endregion

        #region TrackbackPing
        /// <summary>
        /// Gets or sets value that identifies the item's trackback URL.
        /// </summary>
        /// <value>Items trackback URL.</value>
        /// <remarks>This is an optional element.</remarks>
        [XmlElement(ElementName = "ping", Type = typeof(System.String), Namespace = "http://madskills.com/public/xml/rss/module/trackback/")]
        public string TrackbackPing
        {
            get
            {
                return trackbackPing;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    trackbackPing = value.Trim();
                }
            }
        }
        #endregion
    }
}
