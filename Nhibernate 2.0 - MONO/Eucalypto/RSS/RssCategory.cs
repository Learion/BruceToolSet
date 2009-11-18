/****************************************************************************
Modification History:
*****************************************************************************
Date		Author		Description
*****************************************************************************
10/06/2006	brian.kuhn		Created RssCategory Class
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
    /// Represents a common category that can be associated to channels or items
    /// </summary>
    [Serializable()]
    public class RssCategory
    {
        //============================================================
        //	PUBLIC/PRIVATE/PROTECTED MEMBERS
        //============================================================
        #region PRIVATE/PROTECTED/PUBLIC MEMBERS
        /// <summary>
        /// Private member to hold unique identifier for category
        /// </summary>
        private Guid categoryId         = Guid.NewGuid();
        /// <summary>
        /// Private member to hold unique name of category
        /// </summary>
        private string categoryTitle    = String.Empty;
        /// <summary>
        /// Private member to hold domain category belongs to
        /// </summary>
        private string categoryDomain   = String.Empty;
        #endregion

        //============================================================
        //	CONSTRUCTORS
        //============================================================
        #region RssCategory()
        /// <summary>
        /// Default constructor for RssCategory class
        /// </summary>
        public RssCategory()
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

        #region RssCategory(string domain, string title)
        /// <summary>
        /// Constructor for RssCategory class that initializes class state using supplied parameters
        /// </summary>
        /// <param name="domain">Domain designator for category.</param>
        /// <param name="title">Name of category.</param>
        public RssCategory(string domain, string title)
        {
            //------------------------------------------------------------
            //	Attempt to initialize class state
            //------------------------------------------------------------
            try
            {
                //------------------------------------------------------------
                //	Initialize class state by setting properties
                //------------------------------------------------------------
                this.Domain     = domain;
                this.Title      = title;
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
        #region Domain
        /// <summary>
        /// Gets or sets domain category belongs to.
        /// </summary>
        /// <value>Name of domain category belongs to</value>
        [XmlAttribute(AttributeName = "domain", DataType = "string")]
        public string Domain
        {
            get
            {
                return categoryDomain;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    categoryDomain  = value.Trim();
                }
            }
        }
        #endregion

        #region Id
        /// <summary>
        /// Gets or sets unique identifier for the category.
        /// </summary>
        /// <value>Unique identifier for the category</value>
        [XmlAttribute(AttributeName = "id", DataType = "string")]
        public string Id
        {
            get
            {
                return categoryId.ToString();
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
                    categoryId  = new Guid(value.Trim());
                }
            }
        }
        #endregion

        #region Title
        /// <summary>
        /// Gets or sets name of the category.
        /// </summary>
        /// <value>Name of the category</value>
        [XmlText(DataType = "string", Type = typeof(System.String))]
        public string Title
        {
            get
            {
                return categoryTitle;
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
                    categoryTitle   = value.Trim();
                }
            }
        }
        #endregion
    }
}
