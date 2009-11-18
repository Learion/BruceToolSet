#region Using Directives

using System;
using System.Collections.Generic;
using System.Configuration;
using SEOToolSet.Common;
using SEOToolSet.ReportsFacade.Entities;

#endregion

namespace SEOToolSet.ReportsFacade
{
    public sealed class ServiceCaller
    {
        private static readonly ObjectSerializerType pageInformationToolSerializedtype =
            (ObjectSerializerType)
            Enum.Parse(typeof (ObjectSerializerType),
                       ConfigurationManager.AppSettings["PageInformationToolObjectSerializerType"]);

        private ServiceCaller()
        {
        }

        public static string GetLinearKeywordDistributionReport(Uri uriRequested)
        {
            string applicationPath = WebHelper.WebAppRootPath;
            var linearKeywordDistributionReport = new LinearKeywordDistributionReport();
            var keywords = new List<LinearKeywordDistributionReport.Keyword>
                               {
                                   new LinearKeywordDistributionReport.Keyword
                                       {
                                           Name = "bruceclay",
                                           SrcImage = "http://chart.apis.google.com/chart?chs=435x80&cht=bvs&chd=s:KAKAAKAKAAAAAA9ooAAeyy9eeAAAAAAAAAAAKAAAAAAAAAAAAAAAAAAAAAAAAAAAAKAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA&chds=0,1&chts=3169a6&chco=991212&chf=c,lg,90,e3e5e4,0,ffffff,0.85&chbh=4,0&chxt=x,y,x&chxr=1,0,2&chxl=0:|0%|10%|20%|30%|40%|50%|60%|70%|80%|90%|100%|1:|0|1|2|3|4|5|6|2:|Low%3D2%2C%20High%3D93%2C%20Mean%3D49.86%2C%20Mode%3D60%2C%20StDev%3D27.85&chxp=0,0,10,20,30,40,50,60,70,80,90,100|2,50&chxs=0,4c4c4c,8|1,4c4c4c,8|2,4c4c4c,9"
                                       },
                                   new LinearKeywordDistributionReport.Keyword
                                       {
                                           Name = "bruce clay",
                                           SrcImage = "http://chart.apis.google.com/chart?chs=435x80&cht=bvs&chd=s:AAAAAAAA99oAUA9AAAAAAAAAAAAAAAAAAAAAAAAAAAAUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUUAU&chds=0,1&chts=3169a6&chco=991212&chf=c,lg,90,e3e5e4,0,ffffff,0.85&chbh=4,0&chxt=x,y,x&chxr=1,0,2&chxl=0:|0%|10%|20%|30%|40%|50%|60%|70%|80%|90%|100%|1:|0|1|2|3|2:|Low%3D2%2C%20High%3D93%2C%20Mean%3D49.86%2C%20Mode%3D60%2C%20StDev%3D27.85&chxp=0,0,10,20,30,40,50,60,70,80,90,100|2,50&chxs=0,4c4c4c,8|1,4c4c4c,8|2,4c4c4c,9"
                                       },
                                   new LinearKeywordDistributionReport.Keyword
                                       {
                                           Name = "search engine marketing",
                                           SrcImage = "http://chart.apis.google.com/chart?chs=435x80&cht=bvs&chd=s:A9AAAAAAAAAAAAAAAAAAAAAAA9AAAAAAAAAA9AAAAAAAAAAAAAAAAAAAAA9A9AA9AAAAAAAAAAAAAAAAAAAAAAAAAAAA9AAAAAA&chds=0,1&chts=3169a6&chco=991212&chf=c,lg,90,e3e5e4,0,ffffff,0.85&chbh=4,0&chxt=x,y,x&chxr=1,0,2&chxl=0:|0%|10%|20%|30%|40%|50%|60%|70%|80%|90%|100%|1:|0|1|2:|Low%3D2%2C%20High%3D93%2C%20Mean%3D49.86%2C%20Mode%3D60%2C%20StDev%3D27.85&chxp=0,0,10,20,30,40,50,60,70,80,90,100|2,50&chxs=0,4c4c4c,8|1,4c4c4c,8|2,4c4c4c,9"
                                       },
                                   new LinearKeywordDistributionReport.Keyword
                                       {
                                           Name = "search engine optimization",
                                           SrcImage = "http://chart.apis.google.com/chart?chs=435x80&cht=bvs&chd=s:999AAA9A9AA9AAAAAAAAAAAAAA9AAAAAAAAAAAAAAAAA9AAAAAAAAA9AAAAA9A999AAAAAAAAAAAAAAAAAAAAAAAAAAA9AAAAAA&chds=0,1&chts=3169a6&chco=991212&chf=c,lg,90,e3e5e4,0,ffffff,0.85&chbh=4,0&chxt=x,y,x&chxr=1,0,2&chxl=0:|0%|10%|20%|30%|40%|50%|60%|70%|80%|90%|100%|1:|0|1|2:|Low%3D2%2C%20High%3D93%2C%20Mean%3D49.86%2C%20Mode%3D60%2C%20StDev%3D27.85&chxp=0,0,10,20,30,40,50,60,70,80,90,100|2,50&chxs=0,4c4c4c,8|1,4c4c4c,8|2,4c4c4c,9"
                                       }
                               };
            linearKeywordDistributionReport.Keywords = keywords;
            return SerializeHelper.GetJsonResult<LinearKeywordDistributionReport>(linearKeywordDistributionReport,
                                                                  pageInformationToolSerializedtype);
        }

        public static string GetLinkTextReport(Uri uriRequested)
        {
            var linkReport = new LinkTextReport
                                 {
                                     SummaryLinks = new LinkTextReport.Summary
                                                        {
                                                            Total = 57,
                                                            UniqueTargets = 50
                                                        }
                                 };
            var links = new List<LinkTextReport.Link>
                            {
                                new LinkTextReport.Link
                                    {
                                        LinkedTo = "/aboutus.htm",
                                        Category = "F",
                                        Rel = "",
                                        AnchorText = "About",
                                        Error = "None"
                                    },
                                new LinkTextReport.Link
                                    {
                                        LinkedTo = "/aboutus.htm",
                                        Category = "F",
                                        Rel = "",
                                        AnchorText = "About",
                                        Error = "None"
                                    },
                                new LinkTextReport.Link
                                    {
                                        LinkedTo = "/businessinfo.htm",
                                        Category = "F",
                                        Rel = "true",
                                        AnchorText = "Signup",
                                        Error = "None"
                                    },
                                new LinkTextReport.Link
                                    {
                                        LinkedTo = "/quoteform.htm",
                                        Category = "F",
                                        Rel = "true",
                                        AnchorText = "<ResultError>Quote Request Form</ResultError>",
                                        Error = "<ResultError>Broken link</ResultError>"
                                    },
                                new LinkTextReport.Link
                                    {
                                        LinkedTo = "/quoteform.htm",
                                        Category = "R",
                                        Rel = "true",
                                        AnchorText = "Want a FREE No Obligation Price Quote?",
                                        Error = "None"
                                    },
                                new LinkTextReport.Link
                                    {
                                        LinkedTo = "/quoteform.htm",
                                        Category = "F",
                                        Rel = "true",
                                        AnchorText = "comprehensive price quote",
                                        Error = "None"
                                    },
                                new LinkTextReport.Link
                                    {
                                        LinkedTo = "/quoteform.htm",
                                        Category = "F",
                                        Rel = "",
                                        AnchorText = "request notification",
                                        Error = "None"
                                    },
                                new LinkTextReport.Link
                                    {
                                        LinkedTo = "/searchenginechart.pdf",
                                        Category = "F",
                                        Rel = "",
                                        AnchorText = "FREE PDF",
                                        Error = "None"
                                    },
                                new LinkTextReport.Link
                                    {
                                        LinkedTo = "/searchenginerelationshipchart.htm",
                                        Category = "F",
                                        Rel = "",
                                        AnchorText = "<i>Image Link </i>- Click to download",
                                        Error = "None"
                                    },
                                new LinkTextReport.Link
                                    {
                                        LinkedTo = "/seo-tech-tips/techtip.htm",
                                        Category = "F",
                                        Rel = "",
                                        AnchorText = "Server Technical Tips",
                                        Error = "None"
                                    }
                            };
            linkReport.Links = links;
            return SerializeHelper.GetJsonResult<LinkTextReport>(linkReport, pageInformationToolSerializedtype);
        }

        public static string GetOptimizedKeywordsReport(Uri uriRequested)
        {
            var optimizedKeywordReport = new OptimizedKeywordsReport();
            var keywords = new List<OptimizedKeywordsReport.Keyword>
                               {
                                   new OptimizedKeywordsReport.Keyword
                                       {
                                           Name = "bruceclay",
                                           Used = "8"
                                       },
                                   new OptimizedKeywordsReport.Keyword
                                       {
                                           Name = "search engine placement",
                                           Used = "5"
                                       },
                                   new OptimizedKeywordsReport.Keyword
                                       {
                                           Name = "search engine marketing",
                                           Used = "13"
                                       },
                                   new OptimizedKeywordsReport.Keyword
                                       {
                                           Name = "search engine relationship chart",
                                           Used = "4"
                                       },
                                   new OptimizedKeywordsReport.Keyword
                                       {
                                           Name = "search engine optimization",
                                           Used = "8"
                                       },
                                   new OptimizedKeywordsReport.Keyword
                                       {
                                           Name = "website promotion",
                                           Used = "4"
                                       },
                                   new OptimizedKeywordsReport.Keyword
                                       {
                                           Name = "search engine ranking",
                                           Used = "2"
                                       }
                               };
            optimizedKeywordReport.Keywords = keywords;
            return SerializeHelper.GetJsonResult<OptimizedKeywordsReport>(optimizedKeywordReport, pageInformationToolSerializedtype);
        }

        public static string GetTagInformation(Uri uriRequested)
        {
            var tagInformationReport = new TagInformationReport();
            var tags = new List<TagInformationReport.Tag>
                           {
                               new TagInformationReport.Tag
                                   {
                                       Name = "Title",
                                       Count = "15",
                                       StopWords = "2",
                                       UsedWords = "13",
                                       Length = "122",
                                       Data = new TagInformationReport.Tag.DataColumn
                                                  {
                                                      TextData =
                                                          "<Keyword>Bruceclay</Keyword>.com - Internet <Keyword>Marketing: Search Engine Optimization</Keyword>, OOC, Analytics, Design, Email, Branding Services and tools",
                                                      ResultData =
                                                          "<ResultError>Error: Title is too long (15 words). The title should be between 6 and 12 words</ResultError>"
                                                  }
                                   },
                               new TagInformationReport.Tag
                                   {
                                       Name = "Description",
                                       Count = "31",
                                       StopWords = "6",
                                       UsedWords = "25",
                                       Length = "241",
                                       Data = new TagInformationReport.Tag.DataColumn
                                                  {
                                                      TextData =
                                                          "Award winning internet <Keyword>marketing tutorial</Keyword> and services. <Keyword>Search Engine Optimization</Keyword>, PPC, analytics, email, design and branding <Keyword>marketing site</Keyword>. Information and <Keyword>advice, SEO training, placement</Keyword>, submission, services and <Keyword>tools</Keyword> at <Keyword>bruclay.com</Keyword>",
                                                      ResultData = "<ResultSuccess>No issues found</ResultSuccess>"
                                                  }
                                   },
                               new TagInformationReport.Tag
                                   {
                                       Name = "Keywords",
                                       Count = "48",
                                       StopWords = "1",
                                       UsedWords = "47",
                                       Length = "414",
                                       Data = new TagInformationReport.Tag.DataColumn
                                                  {
                                                      TextData =
                                                          "<Keyword>Search Engine marketing</Keyword>, <Keyword>Search Engine Optimization</Keyword>, <Keyword>Search Engine Ranking</Keyword>, <Keyword>Search Engine Placemente</Keyword>, <Keyword>Web site placemente</Keyword>, Internet <Keyword>Marketing</Keyword>, Web site submission, SEO Tools, SEO Products, Award Winning, Placemente Services, Online <Keyword>Marketing</Keyword>, <Keyword>Advice</Keyword>, Regional, Submission, <Keyword>Ranking</Keyword>, <Keyword>Placement</Keyword>, <Keyword>Marketing</Keyword>, Analytics, Design, Email, PPC, Branding, Advertising, <Keyword>Training</Keyword>, Service, <Keyword>Ethics</Keyword>, Free, Information",
                                                      ResultData = "<ResultSuccess>No issues found</ResultSuccess>"
                                                  }
                                   },
                               new TagInformationReport.Tag
                                   {
                                       Name = "Headings",
                                       Count = "9",
                                       StopWords = "",
                                       UsedWords = "28",
                                       Length = "",
                                       Data = new TagInformationReport.Tag.DataColumn
                                                  {
                                                      TextData =
                                                          "&#60;h1&#62;Internet <Keyword>Marketing Website Promotion</Keyword>\n&#60;h1&#62;Internet <Keyword>Marketing</Keyword> Strategy Philosophy\n&#60;h1&#62;<Keyword>Search Engine Relationship Chart</Keyword> reg\n&#60;h1&#62;<Keyword>Search Engine Optimization</Keyword>\n&#60;h1&#62;Pay Per Click PPC\n&#60;h1&#62;<Keyword>Web</Keyword> Analytics\n&#60;h1&#62;<Keyword>SEO Web</Keyword> Design\n&#60;h1&#62;Branding",
                                                      ResultData = "<ResultSuccess>No issues found</ResultSuccess>"
                                                  }
                                   },
                               new TagInformationReport.Tag
                                   {
                                       Name = "Links",
                                       Count = "57",
                                       StopWords = "",
                                       UsedWords = "",
                                       Length = "",
                                       Data = new TagInformationReport.Tag.DataColumn
                                                  {
                                                      TextData =
                                                          "<Keyword>bruceclay</Keyword>\n<Keyword>bruceclay</Keyword> co uk\n<Keyword>bruceclay</Keyword> co za\n<Keyword>bruceclay</Keyword> sidebar\nwdfm\n<Keyword>web</Keyword>\nbruceclay search engine relationshipchart</Keyword>",
                                                      ResultData = "<ResultSuccess>No issues found</ResultSuccess>"
                                                  }
                                   },
                               new TagInformationReport.Tag
                                   {
                                       Name = "Image ALTs",
                                       Count = "27",
                                       StopWords = "",
                                       UsedWords = "84",
                                       Length = "",
                                       Data = new TagInformationReport.Tag.DataColumn
                                                  {
                                                      TextData =
                                                          "<Keyword>Bruce Clay</Keyword> Inc\n<Keyword>Bruce Clay</Keyword> Europe Ltd\n<Keyword>Bruce Clay</Keyword> Australia Pty Ltd\n<Keyword>Bruce Clay</Keyword> Africa\n<Keyword>Bruce Clay</Keyword> Japan KK\nHorizontal line\nInformation advice",
                                                      ResultData = "<ResultSuccess>No issues found</ResultSuccess>"
                                                  }
                                   }
                           };
            tagInformationReport.Tags = tags;
            return SerializeHelper.GetJsonResult<TagInformationReport>(tagInformationReport, pageInformationToolSerializedtype);
        }

        public static string GetToolSetKeywords(Uri uriRequested)
        {
            var toolSetkeywordsReport = new ToolSetKeywordsReport();
            var keywords = new List<ToolSetKeywordsReport.Keyword>
                               {
                                   new ToolSetKeywordsReport.Keyword
                                       {
                                           Name = "bruceclay",
                                           Used = "5"
                                       },
                                   new ToolSetKeywordsReport.Keyword
                                       {
                                           Name = "search engine marketing",
                                           Used = "6"
                                       },
                                   new ToolSetKeywordsReport.Keyword
                                       {
                                           Name = "search engine optimization",
                                           Used = "13"
                                       },
                                   new ToolSetKeywordsReport.Keyword
                                       {
                                           Name = "search engine placement",
                                           Used = "5"
                                       },
                                   new ToolSetKeywordsReport.Keyword
                                       {
                                           Name = "web site marketing",
                                           Used = "2"
                                       },
                                   new ToolSetKeywordsReport.Keyword
                                       {
                                           Name = "search engine relationship chart",
                                           Used = "4"
                                       },
                                   new ToolSetKeywordsReport.Keyword
                                       {
                                           Name = "website promotion",
                                           Used = "2"
                                       }
                               };
            toolSetkeywordsReport.Keywords = keywords;
            return SerializeHelper.GetJsonResult<ToolSetKeywordsReport>(toolSetkeywordsReport, pageInformationToolSerializedtype);
        }

        public static string GetWordMetrics(Uri uriRequested)
        {
            var wordMetricsReport = new WordMetricsReport
                                        {
                                            UsedWords = new WordMetricsReport.SummaryUsedWords
                                                            {
                                                                Total = "1030",
                                                                Words =
                                                                    "<Keyword>internet marketing information tips helpful hints people performing internet marketing seach engine optimization seo search engine marketing ppc running analytics project interested e mail advertising medium interested traffic through branding activities absolutely free quot quot placement ranking advice information supported products tools links tutorial then fee based services training only needed site offered those planning build optimize promote their own web site offer few secrets help</Keyword> everything implementing effective programs developing yourself internet marketing strategies plans internet marketing hard technical detailed work offer everything need succedd construct great content rich website takes planning competent designer uses online architectural design principples carefully combining information delivery intuitive navigation satisfy visitors needs why they visited while accomplishing their own business goals fame fortune why website was created having proper architecture vital online internet marketing site will attract  also satisfy visitors simply put designing award winning website not enough site needs placed front potential visitors they will not know exist integration web design development internet marketing focused traffic generation tactics vital success site without smart marketing any great website will fail bruce clay trade bruceclay focuses specifically succeed internet marketing through website design search engine placemente search engine marketing submission analytics email branding program help achieve high traffic website converts revenue quot clays site exemplifies perceived trust reliability credibility result giving away free expert advice web first glance unprepossessing text heavy site nonetheless offers comprehensive stellar information effective website design placemente search engine registration clays site must visit internet marketers both quality its information its modeling what works web quot larry chase web digest marketers developing website product risky business allows no time mistakes section discusses internet marketing concept national regional level designer must consider expert web design advice offered make sure nothing overlooked major search engine optimization opportunities identified understood even though there more loose ends than count non forgotten all these interneet information thoughts cheklists few recommendations maximize launch efforts help drive traffic site developing promoting business website far easier right tactics section also contains information reference links recommendations help promote website product not only search engines also other internet marketing channels while emphasis making website more successful focusing effective search engine placement procedures tasks site also provides insights into many other highly effective"
                                                            }
                                        };
            var wordMetrics = new List<WordMetricsReport.WordMetric>
                                  {
                                      new WordMetricsReport.WordMetric
                                          {
                                              Name = "3+ Syllable word",
                                              Value = "23",
                                              Description =
                                                  "Complex words are commonly considered words containing 3 or more syllables."
                                          },
                                      new WordMetricsReport.WordMetric
                                          {
                                              Name = "Sentence count",
                                              Value = "74",
                                              Description = "Number of sentences in the body text area."
                                          },
                                      new WordMetricsReport.WordMetric
                                          {
                                              Name = "Syllabes per word",
                                              Value = "1.83",
                                              Description = "Average number of syllabies per word in text area."
                                          },
                                      new WordMetricsReport.WordMetric
                                          {
                                              Name = "Words per sentence",
                                              Value = "13.34",
                                              Description =
                                                  "Average number of words per sentence within the body text area."
                                          },
                                      new WordMetricsReport.WordMetric
                                          {
                                              Name = "Fog Reading Grade Level",
                                              Value = "14.4",
                                              Description =
                                                  "The number of years of formal education reader of average intelligence would need to read the text once and understand the text in the body text area. Your level is ideal for most people."
                                          },
                                      new WordMetricsReport.WordMetric
                                          {
                                              Name = "Flesch Level",
                                              Value = "38.3",
                                              Description =
                                                  "The higher the score, the easier it is to undestand (40 o 60 is optimal)."
                                          },
                                      new WordMetricsReport.WordMetric
                                          {
                                              Name = "Kincaid Grade Level",
                                              Value = "11.2",
                                              Description = "U.S. grade school level (8.0 = eight grader)."
                                          }
                                  };
            wordMetricsReport.WordMetrics = wordMetrics.ToArray();
            return SerializeHelper.GetJsonResult<WordMetricsReport>(wordMetricsReport, pageInformationToolSerializedtype);
        }

        public static string GetWordPhrases(Uri uriRequested)
        {
            var wordPhrasesReport = new WordPhrasesReport();
            var phrases = new List<WordPhrasesReport.Phrase>
                              {
                                  new WordPhrasesReport.Phrase
                                      {
                                          Count = new WordPhrasesReport.Keyword
                                                      {
                                                          Name = "Count",
                                                          MetaTitle = new WordPhrasesReport.KeywordItem
                                                                          {
                                                                              Counter = 13
                                                                          },
                                                          MetaDesc = new WordPhrasesReport.KeywordItem
                                                                         {
                                                                             Counter = 25,
                                                                             Style = "High"
                                                                         },
                                                          MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                             {
                                                                                 Counter = 47
                                                                             },
                                                          Heads = new WordPhrasesReport.KeywordItem
                                                                      {
                                                                          Counter = 28
                                                                      },
                                                          AltTags = new WordPhrasesReport.KeywordItem
                                                                        {
                                                                            Counter = 18,
                                                                            Style = "Low"
                                                                        },
                                                          FirstWords = new WordPhrasesReport.KeywordItem
                                                                           {
                                                                               Counter = 160
                                                                           },
                                                          BodyWords = new WordPhrasesReport.KeywordItem
                                                                          {
                                                                              Counter = 950
                                                                          },
                                                          AllWords = new WordPhrasesReport.KeywordItem
                                                                         {
                                                                             Counter = 1227
                                                                         }
                                                      },
                                          Keywords = new List<WordPhrasesReport.Keyword>
                                                         {
                                                             new WordPhrasesReport.Keyword
                                                                 {
                                                                     Name = "search",
                                                                     MetaTitle = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 7.7,
                                                                                         Counter = 1
                                                                                     },
                                                                     MetaDesc = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 4.0,
                                                                                        Counter = 1
                                                                                    },
                                                                     MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                                        {
                                                                                            Percentage = 8.5,
                                                                                            Counter = 4
                                                                                        },
                                                                     Heads = new WordPhrasesReport.KeywordItem
                                                                                 {
                                                                                     Percentage = 7.1,
                                                                                     Counter = 2
                                                                                 },
                                                                     AltTags = new WordPhrasesReport.KeywordItem
                                                                                   {
                                                                                       Percentage = 22,
                                                                                       Counter = 2
                                                                                   },
                                                                     FirstWords = new WordPhrasesReport.KeywordItem
                                                                                      {
                                                                                          Percentage = 2.7,
                                                                                          Counter = 4
                                                                                      },
                                                                     BodyWords = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 2.4,
                                                                                         Counter = 23
                                                                                     },
                                                                     AllWords = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 3.1,
                                                                                        Counter = 38
                                                                                    }
                                                                 },
                                                             new WordPhrasesReport.Keyword
                                                                 {
                                                                     Name = "marketing",
                                                                     MetaTitle = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 7.7,
                                                                                         Counter = 1
                                                                                     },
                                                                     MetaDesc = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 8,
                                                                                        Counter = 2
                                                                                    },
                                                                     MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                                        {
                                                                                            Percentage = 8.5,
                                                                                            Counter = 4
                                                                                        },
                                                                     Heads = new WordPhrasesReport.KeywordItem
                                                                                 {
                                                                                     Percentage = 7.1,
                                                                                     Counter = 2
                                                                                 },
                                                                     AltTags = new WordPhrasesReport.KeywordItem
                                                                                   {
                                                                                       Percentage = 11,
                                                                                       Counter = 1
                                                                                   },
                                                                     FirstWords = new WordPhrasesReport.KeywordItem
                                                                                      {
                                                                                          Percentage = 2.7,
                                                                                          Counter = 4
                                                                                      },
                                                                     BodyWords = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 1.2,
                                                                                         Counter = 12
                                                                                     },
                                                                     AllWords = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 3.1,
                                                                                        Counter = 1
                                                                                    }
                                                                 },
                                                             new WordPhrasesReport.Keyword
                                                                 {
                                                                     Name = "engine",
                                                                     MetaTitle = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 7.7,
                                                                                         Counter = 1
                                                                                     },
                                                                     MetaDesc = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 4.0,
                                                                                        Counter = 1
                                                                                    },
                                                                     MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                                        {
                                                                                            Percentage = 8.5,
                                                                                            Counter = 4
                                                                                        },
                                                                     Heads = new WordPhrasesReport.KeywordItem
                                                                                 {
                                                                                     Percentage = 7.1,
                                                                                     Counter = 2
                                                                                 },
                                                                     AltTags = new WordPhrasesReport.KeywordItem
                                                                                   {
                                                                                       Percentage = 22,
                                                                                       Counter = 2
                                                                                   },
                                                                     FirstWords = new WordPhrasesReport.KeywordItem
                                                                                      {
                                                                                          Percentage = 1.4,
                                                                                          Counter = 2
                                                                                      },
                                                                     BodyWords = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 2.4,
                                                                                         Counter = 23
                                                                                     },
                                                                     AllWords = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 2.6,
                                                                                        Counter = 31
                                                                                    }
                                                                 },
                                                             new WordPhrasesReport.Keyword
                                                                 {
                                                                     Name = "internet",
                                                                     MetaTitle = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 7.7,
                                                                                         Counter = 1
                                                                                     },
                                                                     MetaDesc = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 4,
                                                                                        Counter = 1
                                                                                    },
                                                                     MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                                        {
                                                                                            Percentage = 7.7,
                                                                                            Counter = 3
                                                                                        },
                                                                     Heads = new WordPhrasesReport.KeywordItem
                                                                                 {
                                                                                     Percentage = 7.1,
                                                                                     Counter = 2
                                                                                 },
                                                                     AltTags = new WordPhrasesReport.KeywordItem
                                                                                   {
                                                                                       Percentage = 11,
                                                                                       Counter = 1
                                                                                   },
                                                                     FirstWords = new WordPhrasesReport.KeywordItem
                                                                                      {
                                                                                          Percentage = 0.7,
                                                                                          Counter = 1
                                                                                      },
                                                                     BodyWords = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 1.2,
                                                                                         Counter = 11
                                                                                     },
                                                                     AllWords = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 2,
                                                                                        Counter = 25
                                                                                    }
                                                                 },
                                                             new WordPhrasesReport.Keyword
                                                                 {
                                                                     Name = "<OptimizedWord>bruce</OptimizedWord>",
                                                                     Style = "High",
                                                                     MetaTitle = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 0,
                                                                                         Counter = 0
                                                                                     },
                                                                     MetaDesc = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 0,
                                                                                        Counter = 0
                                                                                    },
                                                                     MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                                        {
                                                                                            Percentage = 2.1,
                                                                                            Counter = 1
                                                                                        },
                                                                     Heads = new WordPhrasesReport.KeywordItem
                                                                                 {
                                                                                     Percentage = 7.1,
                                                                                     Counter = 2
                                                                                 },
                                                                     AltTags = new WordPhrasesReport.KeywordItem
                                                                                   {
                                                                                       Percentage = 11,
                                                                                       Counter = 1
                                                                                   },
                                                                     FirstWords = new WordPhrasesReport.KeywordItem
                                                                                      {
                                                                                          Percentage = 5.3,
                                                                                          Counter = 8
                                                                                      },
                                                                     BodyWords = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 2.4,
                                                                                         Counter = 23
                                                                                     },
                                                                     AllWords = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 3,
                                                                                        Counter = 37
                                                                                    }
                                                                 }
                                                         }
                                      },
                                  new WordPhrasesReport.Phrase
                                      {
                                          Count = new WordPhrasesReport.Keyword
                                                      {
                                                          Name = "Count",
                                                          MetaTitle = new WordPhrasesReport.KeywordItem
                                                                          {
                                                                              Counter = 13
                                                                          },
                                                          MetaDesc = new WordPhrasesReport.KeywordItem
                                                                         {
                                                                             Counter = 25,
                                                                             Style = "High"
                                                                         },
                                                          MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                             {
                                                                                 Counter = 47
                                                                             },
                                                          Heads = new WordPhrasesReport.KeywordItem
                                                                      {
                                                                          Counter = 28
                                                                      },
                                                          AltTags = new WordPhrasesReport.KeywordItem
                                                                        {
                                                                            Counter = 9,
                                                                            Style = "Low"
                                                                        },
                                                          FirstWords = new WordPhrasesReport.KeywordItem
                                                                           {
                                                                               Counter = 160
                                                                           },
                                                          BodyWords = new WordPhrasesReport.KeywordItem
                                                                          {
                                                                              Counter = 950
                                                                          },
                                                          AllWords = new WordPhrasesReport.KeywordItem
                                                                         {
                                                                             Counter = 1227
                                                                         }
                                                      },
                                          Keywords = new List<WordPhrasesReport.Keyword>
                                                         {
                                                             new WordPhrasesReport.Keyword
                                                                 {
                                                                     Name = "marketing tutorial",
                                                                     MetaTitle = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 0,
                                                                                         Counter = 0
                                                                                     },
                                                                     MetaDesc = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 4,
                                                                                        Counter = 1
                                                                                    },
                                                                     MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                                        {
                                                                                            Percentage = 0,
                                                                                            Counter = 0
                                                                                        },
                                                                     Heads = new WordPhrasesReport.KeywordItem
                                                                                 {
                                                                                     Percentage = 0,
                                                                                     Counter = 0
                                                                                 },
                                                                     AltTags = new WordPhrasesReport.KeywordItem
                                                                                   {
                                                                                       Percentage = 0,
                                                                                       Counter = 0
                                                                                   },
                                                                     FirstWords = new WordPhrasesReport.KeywordItem
                                                                                      {
                                                                                          Percentage = 0,
                                                                                          Counter = 0
                                                                                      },
                                                                     BodyWords = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 2.4,
                                                                                         Counter = 23,
                                                                                         Style = "High"
                                                                                     },
                                                                     AllWords = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 2.5,
                                                                                        Counter = 31
                                                                                    }
                                                                 },
                                                             new WordPhrasesReport.Keyword
                                                                 {
                                                                     Name = "SEO training",
                                                                     MetaTitle = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 7.7,
                                                                                         Counter = 1
                                                                                     },
                                                                     MetaDesc = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 0.2,
                                                                                        Counter = 1
                                                                                    },
                                                                     MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                                        {
                                                                                            Percentage = 0.5,
                                                                                            Counter = 1
                                                                                        },
                                                                     Heads = new WordPhrasesReport.KeywordItem
                                                                                 {
                                                                                     Percentage = 0.2,
                                                                                     Counter = 1
                                                                                 },
                                                                     AltTags = new WordPhrasesReport.KeywordItem
                                                                                   {
                                                                                       Percentage = 0.2,
                                                                                       Counter = 1
                                                                                   },
                                                                     FirstWords = new WordPhrasesReport.KeywordItem
                                                                                      {
                                                                                          Percentage = 0,
                                                                                          Counter = 0
                                                                                      },
                                                                     BodyWords = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 0.5,
                                                                                         Counter = 1
                                                                                     },
                                                                     AllWords = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 0.2,
                                                                                        Counter = 1
                                                                                    }
                                                                 },
                                                             new WordPhrasesReport.Keyword
                                                                 {
                                                                     Name = "marketing site",
                                                                     Style = "Low",
                                                                     MetaTitle = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 7.7,
                                                                                         Counter = 1
                                                                                     },
                                                                     MetaDesc = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 8,
                                                                                        Counter = 1
                                                                                    },
                                                                     MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                                        {
                                                                                            Percentage = 8.5,
                                                                                            Counter = 4,
                                                                                            Style = "Low"
                                                                                        },
                                                                     Heads = new WordPhrasesReport.KeywordItem
                                                                                 {
                                                                                     Percentage = 7.1,
                                                                                     Counter = 2
                                                                                 },
                                                                     AltTags = new WordPhrasesReport.KeywordItem
                                                                                   {
                                                                                       Percentage = 5,
                                                                                       Counter = 1
                                                                                   },
                                                                     FirstWords = new WordPhrasesReport.KeywordItem
                                                                                      {
                                                                                          Percentage = 2.4,
                                                                                          Counter = 0
                                                                                      },
                                                                     BodyWords = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 0.2,
                                                                                         Counter = 8
                                                                                     },
                                                                     AllWords = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 0.1,
                                                                                        Counter = 11
                                                                                    }
                                                                 }
                                                         }
                                      },
                                  new WordPhrasesReport.Phrase
                                      {
                                          Count = new WordPhrasesReport.Keyword
                                                      {
                                                          Name = "Count",
                                                          MetaTitle = new WordPhrasesReport.KeywordItem
                                                                          {
                                                                              Counter = 13
                                                                          },
                                                          MetaDesc = new WordPhrasesReport.KeywordItem
                                                                         {
                                                                             Counter = 25,
                                                                             Style = "High"
                                                                         },
                                                          MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                             {
                                                                                 Counter = 47
                                                                             },
                                                          Heads = new WordPhrasesReport.KeywordItem
                                                                      {
                                                                          Counter = 28
                                                                      },
                                                          AltTags = new WordPhrasesReport.KeywordItem
                                                                        {
                                                                            Counter = 9,
                                                                            Style = "Low"
                                                                        },
                                                          FirstWords = new WordPhrasesReport.KeywordItem
                                                                           {
                                                                               Counter = 160
                                                                           },
                                                          BodyWords = new WordPhrasesReport.KeywordItem
                                                                          {
                                                                              Counter = 950
                                                                          },
                                                          AllWords = new WordPhrasesReport.KeywordItem
                                                                         {
                                                                             Counter = 1227
                                                                         }
                                                      },
                                          Keywords = new List<WordPhrasesReport.Keyword>
                                                         {
                                                             new WordPhrasesReport.Keyword
                                                                 {
                                                                     Name =
                                                                         "<OptimizedWord>search engine optimization</OptimizedWord>",
                                                                     Style = "Low",
                                                                     MetaTitle = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 0,
                                                                                         Counter = 0
                                                                                     },
                                                                     MetaDesc = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 0,
                                                                                        Counter = 0
                                                                                    },
                                                                     MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                                        {
                                                                                            Percentage = 0,
                                                                                            Counter = 0,
                                                                                            Style = "Low"
                                                                                        },
                                                                     Heads = new WordPhrasesReport.KeywordItem
                                                                                 {
                                                                                     Percentage = 7.1,
                                                                                     Counter = 2
                                                                                 },
                                                                     AltTags = new WordPhrasesReport.KeywordItem
                                                                                   {
                                                                                       Percentage = 0.5,
                                                                                       Counter = 1
                                                                                   },
                                                                     FirstWords = new WordPhrasesReport.KeywordItem
                                                                                      {
                                                                                          Percentage = 1.2,
                                                                                          Counter = 2
                                                                                      },
                                                                     BodyWords = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 2.4,
                                                                                         Counter = 12
                                                                                     },
                                                                     AllWords = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 1.5,
                                                                                        Counter = 17
                                                                                    }
                                                                 },
                                                             new WordPhrasesReport.Keyword
                                                                 {
                                                                     Name = "web site placement",
                                                                     MetaTitle = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 7.7,
                                                                                         Counter = 1
                                                                                     },
                                                                     MetaDesc = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 4,
                                                                                        Counter = 1
                                                                                    },
                                                                     MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                                        {
                                                                                            Percentage = 8.5,
                                                                                            Counter = 1
                                                                                        },
                                                                     Heads = new WordPhrasesReport.KeywordItem
                                                                                 {
                                                                                     Percentage = 7.1,
                                                                                     Counter = 3
                                                                                 },
                                                                     AltTags = new WordPhrasesReport.KeywordItem
                                                                                   {
                                                                                       Percentage = 4.2,
                                                                                       Counter = 2
                                                                                   },
                                                                     FirstWords = new WordPhrasesReport.KeywordItem
                                                                                      {
                                                                                          Percentage = 2.7,
                                                                                          Counter = 3
                                                                                      },
                                                                     BodyWords = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 2.4,
                                                                                         Counter = 5
                                                                                     },
                                                                     AllWords = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 2,
                                                                                        Counter = 6
                                                                                    }
                                                                 },
                                                             new WordPhrasesReport.Keyword
                                                                 {
                                                                     Name = "marketing website promotion",
                                                                     MetaTitle = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 7.7,
                                                                                         Counter = 1
                                                                                     },
                                                                     MetaDesc = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 8,
                                                                                        Counter = 3
                                                                                    },
                                                                     MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                                        {
                                                                                            Percentage = 0,
                                                                                            Counter = 0
                                                                                        },
                                                                     Heads = new WordPhrasesReport.KeywordItem
                                                                                 {
                                                                                     Percentage = 7.1,
                                                                                     Counter = 3
                                                                                 },
                                                                     AltTags = new WordPhrasesReport.KeywordItem
                                                                                   {
                                                                                       Percentage = 5.0,
                                                                                       Counter = 2
                                                                                   },
                                                                     FirstWords = new WordPhrasesReport.KeywordItem
                                                                                      {
                                                                                          Percentage = 1.4,
                                                                                          Counter = 1
                                                                                      },
                                                                     BodyWords = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 1.2,
                                                                                         Counter = 4
                                                                                     },
                                                                     AllWords = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 1,
                                                                                        Counter = 5
                                                                                    }
                                                                 }
                                                         }
                                      },
                                  new WordPhrasesReport.Phrase
                                      {
                                          Count = new WordPhrasesReport.Keyword
                                                      {
                                                          Name = "Count",
                                                          MetaTitle = new WordPhrasesReport.KeywordItem
                                                                          {
                                                                              Counter = 13
                                                                          },
                                                          MetaDesc = new WordPhrasesReport.KeywordItem
                                                                         {
                                                                             Counter = 25,
                                                                             Style = "High"
                                                                         },
                                                          MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                             {
                                                                                 Counter = 47
                                                                             },
                                                          Heads = new WordPhrasesReport.KeywordItem
                                                                      {
                                                                          Counter = 28
                                                                      },
                                                          AltTags = new WordPhrasesReport.KeywordItem
                                                                        {
                                                                            Counter = 9,
                                                                            Style = "Low"
                                                                        },
                                                          FirstWords = new WordPhrasesReport.KeywordItem
                                                                           {
                                                                               Counter = 160
                                                                           },
                                                          BodyWords = new WordPhrasesReport.KeywordItem
                                                                          {
                                                                              Counter = 950
                                                                          },
                                                          AllWords = new WordPhrasesReport.KeywordItem
                                                                         {
                                                                             Counter = 1227
                                                                         }
                                                      },
                                          Keywords = new List<WordPhrasesReport.Keyword>
                                                         {
                                                             new WordPhrasesReport.Keyword
                                                                 {
                                                                     Name = "search engine relationship chart",
                                                                     MetaTitle = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 0,
                                                                                         Counter = 0
                                                                                     },
                                                                     MetaDesc = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 0,
                                                                                        Counter = 0
                                                                                    },
                                                                     MetaKeywords = new WordPhrasesReport.KeywordItem
                                                                                        {
                                                                                            Percentage = 0,
                                                                                            Counter = 0
                                                                                        },
                                                                     Heads = new WordPhrasesReport.KeywordItem
                                                                                 {
                                                                                     Percentage = 3.1,
                                                                                     Counter = 1
                                                                                 },
                                                                     AltTags = new WordPhrasesReport.KeywordItem
                                                                                   {
                                                                                       Percentage = 0.5,
                                                                                       Counter = 1
                                                                                   },
                                                                     FirstWords = new WordPhrasesReport.KeywordItem
                                                                                      {
                                                                                          Percentage = 0,
                                                                                          Counter = 0
                                                                                      },
                                                                     BodyWords = new WordPhrasesReport.KeywordItem
                                                                                     {
                                                                                         Percentage = 0.3,
                                                                                         Counter = 2
                                                                                     },
                                                                     AllWords = new WordPhrasesReport.KeywordItem
                                                                                    {
                                                                                        Percentage = 0.2,
                                                                                        Counter = 3
                                                                                    }
                                                                 }
                                                         }
                                      }
                              };
            wordPhrasesReport.Phrases = phrases;
            return SerializeHelper.GetJsonResult<WordPhrasesReport>(wordPhrasesReport, pageInformationToolSerializedtype);
        }

        public static string GetSiteInfo(Uri uriRequested)
        {
            var siteCheckerReport = new SiteCheckerReport();
            var criteria = new List<SiteCheckerReport.SiteCheckerCriterion>
                               {
                                   new SiteCheckerReport.SiteCheckerCriterion
                                       {
                                           CheckedCriterion = "Original URL",
                                           Description = "http://www.bruceclay.com"
                                       },
                                   new SiteCheckerReport.SiteCheckerCriterion
                                       {
                                           CheckedCriterion = "Domain",
                                           Description = "bruceclay.com"
                                       },
                                   new SiteCheckerReport.SiteCheckerCriterion
                                       {
                                           CheckedCriterion = "File",
                                           Description = "/"
                                       },
                                   new SiteCheckerReport.SiteCheckerCriterion
                                       {
                                           CheckedCriterion = "DNS IP",
                                           Description =
                                               "<strong>Address:</strong> 65.124.219.55 /<strong>Sites Hosted:</strong>5"
                                       },
                                   new SiteCheckerReport.SiteCheckerCriterion
                                       {
                                           CheckedCriterion = "Ping",
                                           Description = "Successful (Average Ping Time: <strong>129ms</strong>)"
                                       },
                                   new SiteCheckerReport.SiteCheckerCriterion
                                       {
                                           CheckedCriterion = "Robots",
                                           Description =
                                               "User-Agent: *\nDisallow: /cgi-bin/\nDisallow: /LinkMaps/\nDisallow: /dsm/\nDisallow: /whitepapers/*.pdf\nDisallow: /whitepapers/.htm\nSitemap: http://www.bruceclay.com/bruceclaycom.sitemap.xml"
                                       },
                                   new SiteCheckerReport.SiteCheckerCriterion
                                       {
                                           CheckedCriterion = "IP Blocklist",
                                           Description = "<ResultSuccess>No issues found</ResultSuccess>"
                                       }
                               };
            siteCheckerReport.Criteria = criteria;
            return SerializeHelper.GetJsonResult<SiteCheckerReport>(siteCheckerReport, pageInformationToolSerializedtype);
        }

        public static string GetHeaderInfo(Uri uriRequested)
        {
            var siteCheckerReport = new SiteCheckerReport();
            var criteria = new List<SiteCheckerReport.SiteCheckerCriterion>
                               {
                                   new SiteCheckerReport.SiteCheckerCriterion
                                       {
                                           CheckedCriterion = "Status",
                                           Description = "200 - OK"
                                       },
                                   new SiteCheckerReport.SiteCheckerCriterion
                                       {
                                           CheckedCriterion = "Headers",
                                           Description =
                                               "Date: Fri, 30 May 2008 20:16:06 GMT\nServer: Apache/1.3.39 (Unix) PHP/5.2.5 mod_throttle/3.1.2 mod_psoft_traffic/0.2 mod_ssl/2.8.29\nContent-Type: text/html: charset=iso-8859-1\nClient-Transfer-Encoding: chunked\nP3P: CP='ALL DSP COR CUR ADMo CONo PUBI BUS UNI"
                                       }
                               };
            siteCheckerReport.Criteria = criteria;
            return SerializeHelper.GetJsonResult<SiteCheckerReport>(siteCheckerReport, pageInformationToolSerializedtype);
        }

        public static string GetCloakCheckInfo(Uri uriRequested)
        {
            string applicationPath = WebHelper.WebAppRootPath;
            var siteCheckerReport = new SiteCheckerReport();
            var criteria = new List<SiteCheckerReport.SiteCheckerCriterion>
                               {
                                   new SiteCheckerReport.SiteCheckerCriterion
                                       {
                                           CheckedCriterion =
                                               string.Concat(applicationPath, "images/google_logo_small.jpg"),
                                           Description = "<ResultSuccess>No issues found</ResultSuccess>"
                                       },
                                   new SiteCheckerReport.SiteCheckerCriterion
                                       {
                                           CheckedCriterion =
                                               string.Concat(applicationPath, "images/livesearch-logo.jpg"),
                                           Description = "<ResultSuccess>No issues found</ResultSuccess>"
                                       },
                                   new SiteCheckerReport.SiteCheckerCriterion
                                       {
                                           CheckedCriterion = string.Concat(applicationPath, "images/Yahoo-logo.jpg"),
                                           Description = "<ResultError>1 error found</ResultError>"
                                       }
                               };
            siteCheckerReport.Criteria = criteria;
            return SerializeHelper.GetJsonResult<SiteCheckerReport>(siteCheckerReport, pageInformationToolSerializedtype);
        }

        public static string GetMonitorReport(Uri uriRequested)
        {
            return SerializeHelper.GetJsonResult<MonitorReport>(new MonitorReport(), ObjectSerializerType.Object);
        }

        public static string GetRankingReport(Uri uriRequested)
        {
            return SerializeHelper.GetJsonResult<RankingReport>(new RankingReport(), ObjectSerializerType.Object);
        }

    }
}
