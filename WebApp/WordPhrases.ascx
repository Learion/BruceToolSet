<%@ Control Language="C#" AutoEventWireup="True" 
CodeBehind="WordPhrases.ascx.cs" Inherits="SEOToolSet.WebApp.WordPhrases" %>
<%@ Register Src="ArrayRenderer.ascx" TagName="ArrayRenderer" TagPrefix="uc4" %>
<div class='xTable Phrases'>
    <h3>
        <asp:Literal ID="literalTitle" runat="server"></asp:Literal>
    </h3>
    <uc4:ArrayRenderer ID="ArrayRenderer9" runat="server" HeaderCssClass="RowHeader"
        ItemCssClass="RowItem" AlternatingCssClass="AlternatingRowItem">
        <headertemplate>
            <table>
                <tbody>
                    <tr class='[CLASS_NAME]' style="width: 100%;">
                        <th style="width: 200px; text-align: left;" class="Sortable" field_name='Name' data_type="String">
                            <asp:Label ID="keywordHeading" runat="server" Text="Keyword" meta:resourcekey="keywordHeading"></asp:Label>
                        </th>
                        <th class="Sortable" field_name='MetaTitle.Percentage' data_type="Number">
                            <asp:Label ID="metaTitleHeading" runat="server" Text="Meta Title" meta:resourcekey="metaTitleHeading"></asp:Label>
                        </th>
                        <th class="Sortable" field_name='MetaDesc.Percentage' data_type="Number">
                            <asp:Label ID="metaDescHeading" runat="server" Text="Meta Desc" meta:resourcekey="metaDescHeading"></asp:Label>
                        </th>
                        <th class="Sortable" field_name='MetaKeywords.Percentage' data_type="Number">
                            <asp:Label ID="metaKeywordsHeading" runat="server" Text="Meta Keywords" meta:resourcekey="metaKeywordsHeading"></asp:Label>
                        </th>
                        <th class="Sortable" field_name='Heads.Percentage' data_type="Number">
                            <asp:Label ID="headsHeading" runat="server" Text="Heads" meta:resourcekey="headsHeading"></asp:Label>
                        </th>
                        <th class="Sortable" field_name='AltTags.Percentage' data_type="Number">
                            <asp:Label ID="altTagsHeading" runat="server" Text="ALT Tags" meta:resourcekey="altTagsHeading"></asp:Label>
                        </th>
                        <th class="Sortable" field_name='FirstWords.Percentage' data_type="Number">
                            <asp:Label ID="firstWordsHeading" runat="server" Text="First Words" meta:resourcekey="firstWordsHeading"></asp:Label>
                        </th>
                        <th class="Sortable" field_name='BodyWords.Percentage' data_type="Number">
                            <asp:Label ID="bodyWordsHeading" runat="server" Text="Body Words" meta:resourcekey="bodyWordsHeading"></asp:Label>
                        </th>
                        <th class="Sortable" field_name='AllWords.Percentage' data_type="Number">
                            <asp:Label ID="allWordsHeading" runat="server" Text="All Words" meta:resourcekey="allWordsHeading"></asp:Label>
                        </th>
                    </tr>
                    <tr class='CountHeader'>
                        <td valign="top" class="Countable" style="text-align:right;">
                            {-Name-}
                        </td>
                        <td valign="top" class="{-MetaTitle.Style|default:''-}">
                            {-MetaTitle.Counter-}
                        </td>
                        <td valign="top" class="{-MetaDesc.Style|default:''-}">
                            {-MetaDesc.Counter-}
                        </td>
                        <td valign="top" class="{-MetaKeywords.Style|default:''-}">
                            {-MetaKeywords.Counter-}
                        </td>
                        <td valign="top" class="{-Heads.Style|default:''-}">
                            {-Heads.Counter-}
                        </td>
                        <td valign="top" class="{-AltTags.Style|default:''-}">
                            {-AltTags.Counter-}
                        </td>
                        <td valign="top" class="{-FirstWords.Style|default:''-}">
                            {-FirstWords.Counter-}
                        </td>
                        <td valign="top" class="{-BodyWords.Style|default:''-}">
                            {-BodyWords.Counter-}
                        </td>
                        <td valign="top" class="{-AllWords.Style|default:''-}">
                            {-AllWords.Counter-}
                        </td>
                    </tr>
                </tbody>
            </table>
        </headertemplate>
        <itemtemplate>
            <table>
                <tbody>
                    <tr class="[CLASS_NAME] {Style|default:''}">
                        <td class="Keyword" valign="top" style="text-align:left;">
                            {Name}
                        </td>
                        <td valign="top" class="{MetaTitle.Style|default:''}">
                            {MetaTitle.Percentage|default:'-'|format:'[0] %'} 
                            {MetaTitle.Counter|default:'&nbsp;'|format:'([0])'}
                        </td>
                        <td valign="top" class="{MetaDesc.Style|default:''}">
                            {MetaDesc.Percentage|default:'-'|format:'[0] %'} 
                            {MetaDesc.Counter|default:'&nbsp;'|format:'([0])'}
                        </td>
                        <td valign="top" class="{MetaKeywords.Style|default:''}">
                            {MetaKeywords.Percentage|default:'-'|format:'[0] %'} 
                            {MetaKeywords.Counter|default:'&nbsp;'|format:'([0])'}
                        </td>
                        <td valign="top" class="{Heads.Style|default:''}">
                            {Heads.Percentage|default:'-'|format:'[0] %'} 
                            {Heads.Counter|default:'&nbsp;'|format:'([0])'}
                        </td>
                        <td valign="top" class="{AltTags.Style|default:''}">
                            {AltTags.Percentage|default:'-'|format:'[0] %'} 
                            {AltTags.Counter|default:'&nbsp;'|format:'([0])'}
                        </td>
                        <td valign="top" class="{FirstWords.Style|default:''}">
                            {FirstWords.Percentage|default:'-'|format:'[0] %'} 
                            {FirstWords.Counter|default:'&nbsp;'|format:'([0])'}
                        </td>
                        <td valign="top" class="{BodyWords.Style|default:''}">
                            {BodyWords.Percentage|default:'-'|format:'[0] %'} 
                            {BodyWords.Counter|default:'&nbsp;'|format:'([0])'}
                        </td>
                        <td valign="top" class="{AllWords.Style|default:''}">
                            {AllWords.Percentage|default:'-'|format:'[0] %'} 
                            {AllWords.Counter|default:'&nbsp;'|format:'([0])'}
                        </td>
                    </tr>
                </tbody>
            </table>
        </itemtemplate>
    </uc4:ArrayRenderer>
</div>
