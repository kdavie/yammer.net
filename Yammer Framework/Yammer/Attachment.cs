using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections;
using System.IO;

namespace Yammer
{
   
    public class Attachment
    {

        #region Yammer Properties

        /// <summary>
        /// Indicates the type of attachment. Current options: image and file
        /// </summary>
        [DataMember(Name = "type")]
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Numerical idenifier for this attachment.
        /// </summary>
        [DataMember(Name = "id")]
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Short text identifier which may not be unique.
        /// </summary>
        [DataMember(Name = "name")]
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Where the attachment can be viewed on the Yammer website
        /// </summary>
        [DataMember(Name = "web-url")]
        [XmlElement(ElementName = "web-url")]
        public string WebUrl { get; set; }

        /// <summary>
        /// Image attachment
        /// </summary>
        [DataMember(Name = "image")]
        [XmlElement(ElementName = "image")]
        public Image Image { get; set; }

        /// <summary>
        /// File attachment
        /// </summary>
        [DataMember(Name = "file")]
        [XmlElement(ElementName = "file")]
        public File File { get; set; }

        #endregion

        #region Client Properties

        [XmlIgnore]
        public string EncodedString { get; set; }

        [XmlIgnore]
        public AttachmentType AttachmentType { get; set; }

        [XmlIgnore]
        public string filePath;
        /// <summary>
        /// The filename of the filepart to be uploaded.
        /// </summary>
        [XmlIgnore]
        public string filename;
        /// <summary>
        /// The starting position in the stream.
        /// </summary>
        [XmlIgnore]
        public long startPos;
        /// <summary>
        /// The ending position in the stream.
        /// </summary>
        [XmlIgnore]
        public long endPos;
        /// <summary>
        /// The total file length.
        /// </summary>
        [XmlIgnore]
        public long totalFileLength;
        /// <summary>
        /// The stream of the file to be sent.
        /// </summary>
        [XmlIgnore]
        public Stream binaryStream;

        /// <summary>
        /// Gets the length of the data to be sent.
        /// </summary>
        [XmlIgnore]
        public long Length { get { return this.endPos - this.startPos + 1; } }

        #endregion

    }

}
