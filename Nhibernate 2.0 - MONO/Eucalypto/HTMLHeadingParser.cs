using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto
{
  /// <summary>
  /// Class used to parse an XHTML snippet and construct a TOC based on the heading tags
  /// </summary>
  class HTMLHeadingParser
  {
    /// <summary>
    /// Automatically create the table of contents for the specified document.
    /// Insert an ID in the document if the heading doesn't have it.
    /// Returns the XHTML for the TOC
    /// </summary>
    public static string GenerateTOC(System.Xml.XmlDocument doc)
    {
      System.Xml.XmlNodeList headings = doc.SelectNodes("//*");

      Heading root = new Heading("ROOT", null, 0);
      int index = 0;
      GenerateHeadings(headings, root, ref index);


      using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
      {
        System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(stream, System.Text.Encoding.UTF8);
        writer.WriteStartElement("div");
        root.WriteChildrenToXml(writer);
        writer.WriteEndElement();
        writer.Flush();

        stream.Seek(0, System.IO.SeekOrigin.Begin);
        System.IO.StreamReader reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

        return reader.ReadToEnd();
      }
    }

    private static void GenerateHeadings(System.Xml.XmlNodeList headings, Heading parent, ref int index)
    {
      while (index < headings.Count)
      {
        int headingLevel = GetHeadingLevel(headings[index].Name);

        if (headingLevel == 0)
        {
          //not an heading
          index++; // Skip
        }
        else if (headingLevel <= parent.Level)
        {
          return;
        }
        else if (headingLevel > parent.Level)
        {
          //Generate the header only for heading with ID attribute
          string id = CheckForId(parent, (System.Xml.XmlElement)headings[index]);
          Heading subHead = new Heading(headings[index].InnerText, id, headingLevel);
          parent.AddChild(subHead);

          index++; // Read next

          GenerateHeadings(headings, subHead, ref index);
        }
        else
          throw new ArgumentOutOfRangeException("index");
      }
    }

    private static string CheckForId(Heading parent, System.Xml.XmlElement element)
    {
      string id = element.GetAttribute("id");
      if (id == null || id.Length == 0)
      {
        StringBuilder builder = new StringBuilder();
        foreach (char c in element.InnerText)
        {
          if (char.IsLetterOrDigit(c))
            builder.Append(c);
          else if (char.IsWhiteSpace(c))
          {
            //don't consider whitespace
          }
          else
            builder.Append('_');

          if (builder.Length >= 100)
            break;
        }

        id = builder.ToString();

        //Add the parent id
        if (parent.IsRoot() == false)
          id = parent.Id + "_" + id;

        element.SetAttribute("id", id);
      }

      return id;
    }

    private static string[] TAG_HEADINGS = new string[] { "h1", "h2", "h3", "h4", "h5", "h6" };
    private static int GetHeadingLevel(string element)
    {
      int index = Array.BinarySearch(TAG_HEADINGS, element.ToLowerInvariant());
      if (index < 0)
        return 0;
      else
        return index + 1;
    }

    class Heading
    {
      public static Heading ROOT = new Heading(null, null, 0);

      public Heading(string pText, string pId, int pLevel)
      {
        Text = pText;
        Level = pLevel;
        Id = pId;
      }

      public string Id;
      public string Text;
      public int Level;

      private List<Heading> mChildren = new List<Heading>();
      public void AddChild(Heading child)
      {
        mChildren.Add(child);
      }

      public void WriteToXml(System.Xml.XmlTextWriter writer)
      {
        writer.WriteStartElement("li");

        writer.WriteStartElement("a");
        writer.WriteAttributeString("href", "#" + Id);
        writer.WriteString(Text);
        writer.WriteEndElement();

        if (mChildren.Count > 0)
        {
          WriteChildrenToXml(writer);
        }

        writer.WriteEndElement();
      }

      public void WriteChildrenToXml(System.Xml.XmlTextWriter writer)
      {
        writer.WriteStartElement("ul");

        foreach (Heading subHead in mChildren)
        {
          subHead.WriteToXml(writer);
        }

        writer.WriteEndElement();
      }

      public bool IsRoot()
      {
        return Level == 0;
      }
    }
  }
}
