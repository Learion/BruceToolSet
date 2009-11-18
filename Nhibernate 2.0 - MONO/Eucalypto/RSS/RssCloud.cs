/****************************************************************************
Modification History:
*****************************************************************************
Date		Author		Description
*****************************************************************************
10/06/2006	brian.kuhn		Created RssCloud Class
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
    /// Represents a web service that supports the rssCloud interface which can be implemented in HTTP-POST, XML-RPC or SOAP 1.1.
    /// </summary>
    [Serializable()]
    public class RssCloud
    {
        //============================================================
        //	PUBLIC/PRIVATE/PROTECTED MEMBERS
        //============================================================
        #region PRIVATE/PROTECTED/PUBLIC MEMBERS
        /// <summary>
        /// Private member to hold unique identifier for cloud
        /// </summary>
        private Guid cloudId            = Guid.NewGuid();
        /// <summary>
        /// Private member to hold domain URI of cloud
        /// </summary>
        private string cloudDomain      = String.Empty;
        /// <summary>
        /// Private member to hold port for cloud
        /// </summary>
        private int cloudPort           = 80;
        /// <summary>
        /// Private member to hold path of cloud
        /// </summary>
        private string cloudPath        = String.Empty;
        /// <summary>
        /// Private member to hold protocol used by the cloud
        /// </summary>
        private string cloudProtocol    = String.Empty;
        /// <summary>
        /// Private member to hold procedure cloud calls
        /// </summary>
        private string cloudProcedure   = String.Empty;
        #endregion

        //============================================================
        //	CONSTRUCTORS
        //============================================================
        #region RssCloud()
        /// <summary>
        /// Default constructor for RssCloud class
        /// </summary>
        public RssCloud()
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

        #region RssCloud(string domain, int port, string path, string protocol, string procedure)
        /// <summary>
        /// Constructor for RssCloud class that initializes class state using supplied parameters
        /// </summary>
        /// <param name="domain">Domain for the cloud.</param>
        /// <param name="port">Port number for cloud.</param>
        /// <param name="path">Path for cloud.</param>
        /// <param name="protocol">Protocol for cloud.</param>
        /// <param name="procedure">Register procedure for cloud.</param>
        public RssCloud(string domain, int port, string path, string protocol, string procedure)
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
                this.Path       = path;
                this.Port       = port;
                this.Procedure  = procedure;
                this.Protocol   = protocol;
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
        /// Gets or sets domain for cloud.
        /// </summary>
        /// <value>Name of domain for cloud</value>
        [XmlAttribute(AttributeName = "domain", DataType = "string")]
        public string Domain
        {
            get
            {
                return cloudDomain;
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
                    cloudDomain = value.Trim();
                }
            }
        }
        #endregion

        #region Id
        /// <summary>
        /// Gets or sets unique identifier for the cloud.
        /// </summary>
        /// <value>Unique identifier for the cloud</value>
        [XmlAttribute(AttributeName = "id", DataType = "string")]
        public string Id
        {
            get
            {
                return cloudId.ToString();
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
                    cloudId = new Guid(value.Trim());
                }
            }
        }
        #endregion

        #region Path
        /// <summary>
        /// Gets or sets path for cloud.
        /// </summary>
        /// <value>Path for cloud</value>
        [XmlAttribute(AttributeName = "path", DataType = "string")]
        public string Path
        {
            get
            {
                return cloudPath;
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
                    cloudPath = value.Trim();
                }
            }
        }
        #endregion

        #region Port
        /// <summary>
        /// Gets or sets port number for cloud.
        /// </summary>
        /// <value>Port number for cloud.</value>
        [XmlAttribute(AttributeName = "port", DataType = "int")]
        public int Port
        {
            get
            {
                return cloudPort;
            }

            set
            {
                cloudPort = value;
            }
        }
        #endregion

        #region Procedure
        /// <summary>
        /// Gets or sets register procedure for cloud.
        /// </summary>
        /// <value>Register procedure for cloud</value>
        [XmlAttribute(AttributeName = "registerProcedure", DataType = "string")]
        public string Procedure
        {
            get
            {
                return cloudProcedure;
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
                    cloudProcedure = value.Trim();
                }
            }
        }
        #endregion

        #region Protocol
        /// <summary>
        /// Gets or sets protocol for cloud.
        /// </summary>
        /// <value>Protocol for cloud</value>
        [XmlAttribute(AttributeName = "protocol", DataType = "string")]
        public string Protocol
        {
            get
            {
                return cloudProtocol;
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
                    cloudProtocol = value.Trim();
                }
            }
        }
        #endregion
    }
}
