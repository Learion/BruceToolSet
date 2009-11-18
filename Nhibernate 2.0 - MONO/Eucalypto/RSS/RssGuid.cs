/****************************************************************************
Modification History:
*****************************************************************************
Date		Author		Description
*****************************************************************************
10/09/2006	brian.kuhn		Created RssGuid Class
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
    /// Represents a globally unique identifier (GUID) that uniquely identifies an item
    /// </summary>
    [Serializable()]
    public class RssGuid
    {
        //============================================================
        //	PUBLIC/PRIVATE/PROTECTED MEMBERS
        //============================================================
        #region PRIVATE/PROTECTED/PUBLIC MEMBERS
        /// <summary>
        /// Private member to hold unique identifier for guid
        /// </summary>
        private Guid guidId             = Guid.NewGuid();
        /// <summary>
        /// Private member to hold value indicating if this guid's value is a URI that points to the item it is associated to
        /// </summary>
        private bool guidIsPermaLink    = false;
        /// <summary>
        /// Private member to hold value of the guid
        /// </summary>
        private string guidValue        = String.Empty;
        #endregion

        //============================================================
        //	CONSTRUCTORS
        //============================================================
        #region RssGuid()
        /// <summary>
        /// Default constructor for RssGuid class
        /// </summary>
        public RssGuid()
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

        #region RssGuid(string value, bool isPermaLink)
        /// <summary>
        /// Constructor for RssGuid class that initializes class state using supplied parameters
        /// </summary>
        /// <param name="value">Value for Guid</param>
        /// <param name="isPermaLink">Value indicating if this guid's value is a URI that points to the item it is associated to</param>
        public RssGuid(string value, bool isPermaLink)
        {
            //------------------------------------------------------------
            //	Attempt to initialize class state
            //------------------------------------------------------------
            try
            {
                //------------------------------------------------------------
                //	Initialize class state by setting properties
                //------------------------------------------------------------
                this.IsPermaLink    = isPermaLink;
                this.Value          = value;
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
        /// Gets or sets unique identifier for the guid.
        /// </summary>
        /// <value>Unique identifier for the guid</value>
        [XmlAttribute(AttributeName = "id", DataType = "string")]
        public string Id
        {
            get
            {
                return guidId.ToString();
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
                    guidId  = new Guid(value.Trim());
                }
            }
        }
        #endregion

        #region IsPermaLink
        /// <summary>
        /// Gets a value indicating if guid's value is a URI that points to the item it is associated to.
        /// </summary>
        /// <value>If true then guid's value is a URI that points to the item it is associated to, otherwise guid value not a URI.</value>
        [XmlAttribute(AttributeName = "isPermaLink", DataType = "boolean")]
        public bool IsPermaLink
        {
            get
            {
                return guidIsPermaLink;
            }

            set
            {
                guidIsPermaLink = value;
            }
        }
        #endregion

        #region Value
        /// <summary>
        /// Gets or sets value of the guid.
        /// </summary>
        /// <value>Value of the guid</value>
        [XmlText(DataType = "string", Type = typeof(System.String))]
        public string Value
        {
            get
            {
                return guidValue;
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
                    guidValue   = value.Trim();
                }
            }
        }
        #endregion
    }
}
