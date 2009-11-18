/****************************************************************************
Modification History:
*****************************************************************************
Date		Author		Description
*****************************************************************************
10/06/2006	brian.kuhn		Created RssImage Class
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
    /// Represents an image that can be associated to a channel
    /// </summary>
    [Serializable()]
    public class RssImage
    {
        //============================================================
        //	PUBLIC/PRIVATE/PROTECTED MEMBERS
        //============================================================
        #region PRIVATE/PROTECTED/PUBLIC MEMBERS
        /// <summary>
        /// Private member to hold unique identifier for image
        /// </summary>
        private Guid imageId            = Guid.NewGuid();
        /// <summary>
        /// Private member to hold URI to a GIF, JPEG or PNG image 
        /// </summary>
        private Uri imageUrl;
        /// <summary>
        /// Private member to hold URI for the site image is associated with
        /// </summary>
        private Uri imageLink;
        /// <summary>
        /// Private member to hold title of image
        /// </summary>
        private string imageTitle       = String.Empty;
        /// <summary>
        /// Private member to hold width of image in pixels
        /// </summary>
        private int imageWidth          = 88;
        /// <summary>
        /// Private member to hold height of image in pixels
        /// </summary>
        private int imageHeight         = 31;
        /// <summary>
        /// Private member to hold description of image
        /// </summary>
        private string imageDescription = String.Empty;
        #endregion

        //============================================================
        //	CONSTRUCTORS
        //============================================================
        #region RssImage()
        /// <summary>
        /// Default constructor for RssImage class
        /// </summary>
        public RssImage()
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

        #region RssImage(string url, string title, string link)
        /// <summary>
        /// Constructor for RssImage class that initialize class state using supplied parameters
        /// </summary>
        /// <param name="url">The URL of a GIF, JPEG or PNG image for the image</param>
        /// <param name="title">Describes the image.</param>
        /// <param name="link">The URL of a site that image links to.</param>
        public RssImage(string url, string title, string link)
        {
            //------------------------------------------------------------
            //	Attempt to initialize class state
            //------------------------------------------------------------
            try
            {
                //------------------------------------------------------------
                //	Initialize class state by setting properties
                //------------------------------------------------------------
                this.Url            = url;
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
        #region Description
        /// <summary>
        /// Gets or sets description for the image.
        /// </summary>
        /// <value>Description for the image</value>
        [XmlElement(ElementName = "description", Type = typeof(System.String))]
        public string Description
        {
            get
            {
                return imageDescription;
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
                    imageDescription = value.Trim();
                }
            }
        }
        #endregion

        #region Height
        /// <summary>
        /// Gets or sets height of image in pixels.
        /// </summary>
        /// <value>Height of image in pixels</value>
        [XmlElement(ElementName = "height", Type = typeof(System.Int32))]
        public int Height
        {
            get
            {
                return imageHeight;
            }

            set
            {
                if (value > 400)
                {
                    throw new ArgumentException("Image height cannot exceed 400 pixels.", "value");
                }
                else
                {
                    imageHeight = value;
                }
            }
        }
        #endregion

        #region Id
        /// <summary>
        /// Gets or sets unique identifier for the image.
        /// </summary>
        /// <value>Unique identifier for the image</value>
        [XmlAttribute(AttributeName = "id", DataType = "string")]
        public string Id
        {
            get
            {
                return imageId.ToString();
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
                    imageId = new Guid(value.Trim());
                }
            }
        }
        #endregion

        #region Link
        /// <summary>
        /// Gets or sets URI for the site image is associated with.
        /// </summary>
        /// <value>URI for the site image is associated with</value>
        [XmlElement(ElementName = "link", Type = typeof(System.String))]
        public string Link
        {
            get
            {
                if (imageLink != null)
                {
                    return imageLink.AbsoluteUri;
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
                else if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Empty string", "value");
                }
                else
                {
                    imageLink = new Uri(value.Trim(), UriKind.Absolute);
                }
            }
        }
        #endregion

        #region Title
        /// <summary>
        /// Gets or sets title of the image.
        /// </summary>
        /// <value>Title of the image</value>
        [XmlElement(ElementName = "title", Type = typeof(System.String))]
        public string Title
        {
            get
            {
                return imageTitle;
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
                    imageTitle = value.Trim();
                }
            }
        }
        #endregion

        #region Url
        /// <summary>
        /// Gets or sets URI to a GIF, JPEG or PNG image.
        /// </summary>
        /// <value>URI to a GIF, JPEG or PNG image</value>
        [XmlElement(ElementName = "url", Type = typeof(System.String))]
        public string Url
        {
            get
            {
                if (imageUrl != null)
                {
                    return imageUrl.AbsoluteUri;
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
                else if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Empty string", "value");
                }
                else
                {
                    imageUrl    = new Uri(value.Trim(), UriKind.Absolute);
                }
            }
        }
        #endregion

        #region Width
        /// <summary>
        /// Gets or sets width of image in pixels.
        /// </summary>
        /// <value>Width of image in pixels</value>
        [XmlElement(ElementName = "width", Type = typeof(System.Int32))]
        public int Width
        {
            get
            {
                return imageWidth;
            }

            set
            {
                if (value > 144)
                {
                    throw new ArgumentException("Image width cannot exceed 144 pixels.", "value");
                }
                else
                {
                    imageWidth  = value;
                }
            }
        }
        #endregion
    }
}
