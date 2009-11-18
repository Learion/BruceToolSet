<%@ Page Language="C#" MasterPageFile="~/BC.Master" AutoEventWireup="true" CodeBehind="Project.aspx.cs"
    Inherits="SEOToolSet.WebApp.Project" Title="Projects" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="R3M.Controls" Namespace="R3M.Controls" TagPrefix="cc1" %>
<%@ Register Src="PageTitle.ascx" TagName="PageTitle" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/IncludeFile.ascx" TagName="IncludeFile" TagPrefix="uc1" %>
<%@ Register Src="ManageUsersControl.ascx" TagName="ManageUsersControl" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentArea" runat="server">
    <uc1:IncludeFile ID="IncludingTreeStylesheet" FilePath="~/scripts/Controls/Tree/Tree.css"
        TypeOfFile="CSS" runat="server" />
    <uc1:IncludeFile ID="IncludingTreeJavascript" FilePath="~/scripts/Controls/Tree/jquery.simple.tree.js"
        TypeOfFile="Javascript" runat="server" />
    <uc1:IncludeFile ID="IncludeFile1" FilePath="~/css/Project.css" TypeOfFile="CSS"
        runat="server" />
    <uc1:IncludeFile ID="IncludeFile2" FilePath="~/scripts/Controllers/TreeHelper.js"
        TypeOfFile="Javascript" runat="server" />
    <uc1:IncludeFile ID="IncludeFile3" FilePath="~/scripts/jQuery.UI/themes/flora/flora.tabs.css"
        TypeOfFile="Css" runat="server" />
    <uc1:PageTitle ID="PageTitle1" runat="server" PanelContainerVisible="false" meta:resourcekey="EditProjectPageTitle" />
    <asp:ScriptManagerProxy ID="ScriptManager1" runat="server">
    </asp:ScriptManagerProxy>
    <asp:Panel ID="ProjectPanel" runat="server">
        <cc1:RoundPanel ID="RoundPanel1" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:FormView Width="100%" ID="formViewProject" runat="server" DataSourceID="odsProject"
                        DataKeyNames="Id">
                        <EditItemTemplate>
                            <div class="FormPanel">
                                <div class="Legend">
                                    <h2>
                                        <asp:Localize ID="Localize9" runat="server" Text="<%$ Resources:CommonTerms, ProjectInformation %>"></asp:Localize></h2>
                                </div>
                                <div class="FormCSS">
                                    <asp:ValidationSummary ID="ProjectInformationValidationSummary" runat="server" ValidationGroup="ProjectInformation" />
                                    <div class="Field OneLine">
                                        <label>
                                            <asp:Localize ID="Localize10" runat="server" Text="<%$ Resources:CommonTerms, Name %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox ID="NameTextBox" Width="230px" runat="server" CssClass="FormText" Text='<%# Bind("Name") %>' />
                                        <asp:RequiredFieldValidator ID="ProjectNameRequired" runat="server" ControlToValidate="NameTextBox"
                                            Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, ProjectNameRequired %>"
                                            SetFocusOnError="True" ValidationGroup="ProjectInformation">*</asp:RequiredFieldValidator>
                                        <label class="ShortLabel" style="width: 100px !important;">
                                            <asp:Localize ID="Localize11" runat="server" Text="<%$ Resources:CommonTerms, Domain %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox ID="DomainTextBox" Width="230px" runat="server" CssClass="FormText"
                                            Text='<%# Bind("Domain") %>' />
                                        <asp:RequiredFieldValidator ID="DomainRequired" runat="server" ControlToValidate="DomainTextBox"
                                            Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, DomainRequired %>"
                                            SetFocusOnError="True" ValidationGroup="ProjectInformation">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="DomainRegexValidation" runat="server" ControlToValidate="DomainTextBox"
                                            Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, DomainNotValid %>"
                                            SetFocusOnError="True" ValidationExpression="(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w-\./?%&amp;=]*)?"
                                            ValidationGroup="ProjectInformation">*</asp:RegularExpressionValidator>
                                    </div>
                                    <div class="Field OneLine">
                                        <label>
                                            <asp:Localize ID="Localize12" runat="server" Text="<%$ Resources:CommonTerms, ClientName %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox ID="ClientNameTextBox" Width="230px" runat="server" CssClass="FormText"
                                            Text='<%# Bind("ClientName") %>' />
                                        <asp:RequiredFieldValidator ID="ClientNameRequired" runat="server" ControlToValidate="ClientNameTextBox"
                                            Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, ClientNameRequired %>"
                                            SetFocusOnError="True" ValidationGroup="ProjectInformation">*</asp:RequiredFieldValidator>
                                        <label class="ShortLabel" style="width: 100px !important;">
                                            <asp:Localize ID="Localize13" runat="server" Text="<%$ Resources:CommonTerms, ContactName %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox ID="ContactNameTextBox" Width="230px" runat="server" CssClass="FormText"
                                            Text='<%# Bind("ContactName") %>' />
                                    </div>
                                    <div class="Field OneLine">
                                        <label>
                                            <asp:Localize ID="Localize14" runat="server" Text="<%$ Resources:CommonTerms, ContactEmail %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox ID="ContactEmailTextBox" Width="230px" runat="server" CssClass="FormText"
                                            Text='<%# Bind("ContactEmail") %>' />
                                        <asp:RegularExpressionValidator ID="EmailRegexValidation" runat="server" ControlToValidate="ContactEmailTextBox"
                                            Display="Dynamic" ErrorMessage="<%$ Resources:CommonTerms, EmailNotValid %>"
                                            SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="ProjectInformation">*</asp:RegularExpressionValidator>
                                        <label class="ShortLabel" style="width: 100px !important;">
                                            <asp:Localize ID="Localize15" runat="server" Text="<%$ Resources:CommonTerms, ContactPhone %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox ID="ContactPhoneTextBox" Width="230px" runat="server" CssClass="FormText"
                                            Text='<%# Bind("ContactPhone") %>' />
                                    </div>
                                </div>
                                <div class="DoClear">
                                </div>
                                <div class="Actions">
                                    <ul>
                                        <li>
                                            <asp:LinkButton CssClass="LinkCommandRound Little" ID="ButtonSave" runat="server"
                                                CausesValidation="True" CommandName="Update" ValidationGroup="ProjectInformation">
                                                <span>
                                                    <asp:Localize ID="Localize16" runat="server" Text="<%$ Resources:CommonTerms, Save %>"></asp:Localize>
                                                </span>
                                            </asp:LinkButton>
                                        </li>
                                        <li>
                                            <asp:LinkButton CssClass="LinkCommandRound Little" ID="LinkButton1" runat="server"
                                                CausesValidation="False" CommandName="Cancel">
                                                <span>
                                                    <asp:Localize ID="Localize17" runat="server" Text="<%$ Resources:CommonTerms, Cancel %>"></asp:Localize>
                                                </span>
                                            </asp:LinkButton></li>
                                    </ul>
                                    <div class="DoClear">
                                    </div>
                                </div>
                            </div>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <div class="FormPanel">
                                <div class="Legend">
                                    <h2>
                                        <asp:Localize ID="Localize1" runat="server" Text="<%$ Resources:CommonTerms, ProjectInformation %>"></asp:Localize></h2>
                                </div>
                                <div class="FormCSS">
                                    <div class="Field OneLine">
                                        <label>
                                            <asp:Localize ID="Localize2" runat="server" Text="<%$ Resources:CommonTerms, Name %>"></asp:Localize></label>
                                        <asp:TextBox ID="NameTextBox" Width="230px" runat="server" CssClass="FormText Disabled"
                                            Text='<%# Bind("Name") %>' />
                                        <label class="ShortLabel" style="width: 100px !important;">
                                            <asp:Localize ID="Localize3" runat="server" Text="<%$ Resources:CommonTerms, Domain %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox ID="DomainTextBox" Width="230px" runat="server" CssClass="FormText Disabled"
                                            Text='<%# Bind("Domain") %>' />
                                    </div>
                                    <div class="Field OneLine">
                                        <label>
                                            <asp:Localize ID="Localize4" runat="server" Text="<%$ Resources:CommonTerms, ClientName %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox ID="ClientNameTextBox" Width="230px" runat="server" CssClass="FormText Disabled"
                                            Text='<%# Bind("ClientName") %>' />
                                        <label class="ShortLabel" style="width: 100px !important;">
                                            <asp:Localize ID="Localize5" runat="server" Text="<%$ Resources:CommonTerms, ContactName %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox ID="ContactNameTextBox" Width="230px" runat="server" CssClass="FormText Disabled"
                                            Text='<%# Bind("ContactName") %>' />
                                    </div>
                                    <div class="Field OneLine">
                                        <label>
                                            <asp:Localize ID="Localize6" runat="server" Text="<%$ Resources:CommonTerms, ContactEmail %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox ID="ContactEmailTextBox" Width="230px" runat="server" CssClass="FormText Disabled"
                                            Text='<%# Bind("ContactEmail") %>' />
                                        <label class="ShortLabel" style="width: 100px !important;">
                                            <asp:Localize ID="Localize7" runat="server" Text="<%$ Resources:CommonTerms, ContactPhone %>"></asp:Localize>
                                        </label>
                                        <asp:TextBox ID="ContactPhoneTextBox" Width="230px" runat="server" CssClass="FormText Disabled"
                                            Text='<%# Bind("ContactPhone") %>' />
                                    </div>
                                </div>
                                <div class="DoClear">
                                </div>
                                <div class="Actions">
                                    <ul>
                                        <li>
                                            <asp:LinkButton CssClass="LinkCommandRound Little" ID="ButtonSave" runat="server"
                                                CausesValidation="True" CommandName="Edit">
                                                <span>
                                                    <asp:Localize ID="Localize8" runat="server" Text="<%$ Resources:CommonTerms, Edit %>"></asp:Localize>
                                                </span>
                                            </asp:LinkButton>
                                        </li>
                                    </ul>
                                    <div class="DoClear">
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:FormView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </cc1:RoundPanel>
        <asp:ObjectDataSource TypeName="SEOToolSet.Providers.ProjectManager" SelectMethod="GetProjectById"
            ID="odsProject" runat="server" UpdateMethod="UpdateProject">
            <UpdateParameters>
                <asp:Parameter Name="id" Type="Int32" />
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="domain" Type="String" />
                <asp:Parameter Name="clientName" Type="String" />
                <asp:Parameter Name="contactEmail" Type="String" />
                <asp:Parameter Name="contactName" Type="String" />
                <asp:Parameter Name="contactPhone" Type="String" />
                <asp:Parameter Name="enabled" Type="Boolean" />
                <asp:Parameter Name="updateBy" Type="String" />
                <asp:Parameter Name="account" Type="Object" />
            </UpdateParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="__Page" Name="id" PropertyName="IdProject" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <script type="text/javascript">
var _justOne = false;
var helperUrl = '<%= Page.ResolveClientUrl("~/Handler/ProjectHelper.ashx") %>';
var IdProject = <%= IdProject %>;
var inlineMessageDiv = 'InlineMessage';
function  tree_onReady(){
    //Ensure Mozilla launch this OnReady Event properly
    if (_justOne) return;                                    	
	
	/* COMPETITORS TREE */
	/******************************************************************************************/
	//TODO: Add TimeOut to this Post Tasks, or use the AsyncTask and the AsyncManager
	
	//To Control the display of the InlineMessage
	var interval = null;
	
	/** 
	* function used to show an error message on the page, when an error happens.
	*/	
	function onError(message, noSticky) {
	   clearInterval(interval);
	   interval = setTimeout(function () {
	        $.showInlineMessage($.byId(inlineMessageDiv), message || $.getResourceString('AsyncRequestFailed', 'Oops, the async request failed, please refresh the page and try again'), 
            {
               sticky: !noSticky
            });
        }, 200);
        
	}
	
    function hideError() {  
        clearInterval(interval);  
        $.hideInLineMessage($.byId(inlineMessageDiv));
    }
	
	var opts = 
        {
            //the CSS Selector to obtain the element where the Tree must be rendered.
            selector: '#treeCompetitor.simpleTree',
            //raised on blur, args.cancel is used to 
            onEditorBlur : function (sender, args) {                           
               var val = args.value;
               
               //check if it begins with http://
               if (val.indexOf('http://') !== 0)  {
                 val = 'http://' + val;
                 //put the new value to the editor element
                 args.editorElement.value = val;                     
               }
                                             
                              
               var v = new RegExp("^http\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(/\S*)?$","gi");               
               args.cancel = !v.exec(val);                              
               if (args.cancel) {
                 onError($.getResourceString("UriNotValid", "The entry is not a valid domain. Please try again") ,true);
               }
               else {
                hideError();
               }
            },
            
            onEditorEscape : function(sender, args) {
              console.log('editor escape');
              hideError();             
            },
            onNodeElementAdded: function (sender, args) {                                
               var li = args.oNode.find('>span');
               setTimeout(function () {                                
                li.triggerHandler('dblclick');
               }, 50);               
            },
            
            onNodeElementAdding : function(sender, args) 
            {                
                var val = $.trim(sender.enconde(args.editorElement.value));                                                                                   
                $.post(helperUrl, { action: "createCompetitor", name: val, idProject : IdProject },
                     function(data, textStatus) {                        
                        if (textStatus !== 'success') onError();
                        if (data.result === 'True') {                     
                            sender.addNodeElement(
                                args.node, 
                                $.trim(args.editorElement.value), 
                                function (senderInner, argsInner) {
                                    argsInner.node.addClass('Competitor').attr('competitor_id', data.id);
                                }
                            );
                        }                        
                        
                     }
                     , "json");		
                args.cancel = true;
            },
            onNodeElementUpdating : function (sender, args) {
             args.cancel = true;
             var val = $.trim(args.editorElement.value);
             $.post(helperUrl, { action: "updateCompetitor", name: val, idCompetitor : args.node.attr('competitor_id'), idProject : IdProject  },
                function(data,textStatus) {
                    if (textStatus !== 'success') onError();
                    if (data.result === 'True')
                        sender.updateNodeText(args.node, val);                    
                }, "json");                             
            },
            
            addNewElementString : '<%= GetGlobalResourceObject("CommonTerms","AddCompetitor") %>',
			addNewElementListString : 'Add New List'                                 
        };            
    
    var treeCompetitors =  new TreeHelper(opts);        
        
    $('#deleteCompetitor').click(function () {
        var node = treeCompetitors.getCurrentNode();                
        var competitorName = node.text().replace(/\s/g,'');
        if (node && (node.length > 0) && !node.hasClass('CompetitorAdd')) {            
            $.showConfirm('<%= GetGlobalResourceObject("CommonTerms","DeleteItemConfirm") %>'.replace('{0}','<%= GetGlobalResourceObject("CommonTerms","TheCompetitor") %> "' + competitorName + '"'), {
                onOkClicked: function() {
                    $.post(helperUrl, { action: "deleteCompetitor", idCompetitor : node.attr('competitor_id'), idProject : IdProject  },
                        function(data,textStatus) {
                            if (textStatus !== 'success') onError();
                            if (data.result === 'True')
                                treeCompetitors.deleteCurrentNode();                        
                        }, "json");                                          
                },
                onCancelClicked: function() {

                }
             });
             return false;             
        }
        else {
            $.showMessage('<%= GetLocalResourceObject("SelectAtLeastOneCompetitor") %>', {icon : $.MessageIco.Alert });        
        }
        return false;
    });
    
    
    var optstreeKeywordList = { 
        selector : '#treeKeywordList.simpleTree',        
        onNodeListAdded : function (sender, args) {
           args.node.find('.AddCommandElement').addClass('KeywordAdd');           
        },
        onNodeElementAdded: function (sender, args) {                                
               var li = args.oNode.find('>span');
               setTimeout(function () {                                
                li.triggerHandler('dblclick');
               }, 200);               
        },
        onNodeListAdding : function(sender,args) {
             var val = $.trim(sender.enconde(args.editorElement.value));
                $.post(helperUrl, { action: "createKeywordList", name: val, idProject : IdProject },
                     function(data, textStatus) {
                        if (textStatus !== 'success') onError();
                        if (data.result === 'True') {                     
                            sender.addNodeList(
                                args.node, 
                                $.trim(args.editorElement.value), 
                                function (senderInner, argsInner) {
                                    argsInner.node.find('.AddCommandElement').attr('keyword_list_id', data.id);
                                }
                            );
                        }                        
                        
                     }
                     , "json");		
                args.cancel = true;
        },
        onNodeElementAdding : function(sender, args) 
            {
                var val = $.trim(sender.enconde(args.editorElement.value));
                $.post(helperUrl, { action: "createKeyword", keyword: val, idKeywordList: args.node.attr('keyword_list_id') , idProject : IdProject },
                     function(data, textStatus) {
                        if (textStatus !== 'success') onError();
                        if (data.result === 'True') {                     
                            sender.addNodeElement(
                                args.node, 
                                $.trim(args.editorElement.value), 
                                function (senderInner, argsInner) {
                                    argsInner.node.attr('keyword_id', data.id);
                                }
                            );
                        }                                                
                     }
                     , "json");		
                args.cancel = true;
            },
            onNodeElementUpdating : function (sender, args) {
                 args.cancel = true;
                 var val = $.trim(args.editorElement.value);
                 if (args.node.attr('className').indexOf('folder') >= 0 ) {
                    $.post(helperUrl, { action: "updateKeywordList", name: val, idKeywordList : args.node.find('.AddCommandElement').attr('keyword_list_id'), idProject : IdProject  },
                        function(data,textStatus) {
                            if (textStatus !== 'success') onError();
                            if (data.result === 'True')
                                sender.updateNodeText(args.node, val);                    
                        }, "json");       
                 }
                 
                 if (args.node.attr('className').indexOf('doc') >= 0 ) {
                    $.post(helperUrl, { action: "updateKeyword", keyword: val, idKeyword : args.node.attr('keyword_id'), idProject : IdProject  },
                        function(data,textStatus) {
                            if (textStatus !== 'success') onError();
                            if (data.result === 'True')
                                sender.updateNodeText(args.node, val);                    
                        }, "json");       
                 }                                                                        
            },       
            addNewElementString : '<%= GetLocalResourceObject("AddNewKeyword.Text") %>',
            addNewElementListString : '<%= GetLocalResourceObject("NewKeywordList.Text") %>'                                 
     };            
    
    var treeKeywords = new TreeHelper(optstreeKeywordList);   
    
    $('#deleteKeywordOrKeywordList').click(function() {
         var node = treeKeywords.getCurrentNode();        
        if (node && (node.length > 0) && !node.hasClass('KeywordListAdd') && !node.hasClass('KeywordAdd')) {            
           if (node.attr('className').indexOf('folder') >= 0) {            
                var keywordlist =$.trim(node.find('span.active').text().replace('\n',''));
                keywordlist == keywordlist || '';
                $.showConfirm('<%= GetGlobalResourceObject("CommonTerms","DeleteItemConfirm") %>'.replace('{0}','<%= GetGlobalResourceObject("CommonTerms","TheKeywordList") %> "' + keywordlist + '" <%= GetGlobalResourceObject("CommonTerms","AndItsKeywords") %>') + ' <%= GetGlobalResourceObject("CommonTerms","ActionCouldNotBeUndone") %>.', {
	                onOkClicked: function() {
	                     $.post(helperUrl, { action: "deleteKeywordList", idKeywordList : node.find('.AddCommandElement').attr('keyword_list_id'), idProject : IdProject  },
	                    function(data,textStatus) {
	                        if (textStatus !== 'success') onError();
	                        if (data.result === 'True')
	                            treeKeywords.deleteCurrentNode();
	                        else {
	                            onError($.getResourceString("KeywordListCannotBeDeleted", "Oops the KeywordList could not be deleted, try to delete the Keywords first"));
	                        }            
	                    }, "json"); 
	                },
	                onCancelClicked: function() {

	                }
	            });
	            return false;    
           }
           if (node.attr('className').indexOf('doc') >= 0 ) {
                var keyword = $.trim(node.find('span.active').text().replace('\n',''));
               	$.showConfirm('<%= GetGlobalResourceObject("CommonTerms","DeleteItemConfirm") %>'.replace('{0}','<%= GetGlobalResourceObject("CommonTerms","TheKeyword") %> "' + keyword + '"') , {
	                onOkClicked: function() {
	                     $.post(helperUrl, { action: "deleteKeyword", idKeyword : node.attr('keyword_id'), idProject : IdProject  },
		                    function(data,textStatus) {
		                        if (textStatus !== 'success') onError();
		                        if (data.result === 'True')
		                            treeKeywords.deleteCurrentNode();            
		                    }, "json"); 	                 
	                },
	                onCancelClicked: function() {

	                }
	             });
	             return false;                
           }
            
            
        }
        else {
            $.showMessage('<%= GetLocalResourceObject("SelectAtLeastOneKeywordOrKeywordList") %>', {icon : $.MessageIco.Alert });        
        }
        return false;
    });                       
	_justOne = true;
}
$(document).ready(tree_onReady);
        </script>

        <cc1:RoundPanel ID="RoundPanel2" runat="server">
            <div class="FormPanel">
                <div class="Legend">
                    <h2>
                        <asp:Localize ID="Localize18" runat="server" meta:resourcekey="CompetitorsAndKeywords"></asp:Localize></h2>
                </div>
                <div class="FormCSS">
                    <div class="TreeHolder">
                        <h3>
                            <asp:Localize ID="Localize19" runat="server" Text="<%$ Resources:CommonTerms, Competitors %>"></asp:Localize>
                        </h3>
                        <ul id="treeCompetitor" class="simpleTree">
                            <li class="root" id='1'>
                                <cc1:CustomRepeater DataSourceID="odsCompetitors" ID="CompetitorsRepeater" runat="server">
                                    <HeaderTemplate>
                                        <ul>
                                            <li class="AddCommandElement CompetitorAdd"><span tabindex='50'>
                                                <asp:Localize ID="Localize21" runat="server" Text="<%$ Resources:CommonTerms, AddCompetitor %>"></asp:Localize></span>
                                            </li>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li class="Competitor" competitor_id='<%# Eval("Id") %>'><span>
                                            <%# Eval("Name") ?? "Unnamed Competitor" %></span></li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </cc1:CustomRepeater>
                            </li>
                        </ul>
                        <div class="ToolbarCenter">
                            <ul>
                                <li><a id='deleteCompetitor' href='###' class='LinkCommandRound'><span>
                                    <asp:Localize ID="Localize22" runat="server" Text="<%$ Resources:CommonTerms, Delete %>"></asp:Localize>
                                </span></a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="TreeHolder" id='KeywordsLists'>
                        <div class="HolderTitle">
                            <a href="#" id="bulkLoad" class="MiniCommand"><span>
                                <%= GetLocalResourceObject("KeywordListBulkLoader") %></span></a>
                            <h3>
                                <asp:Localize ID="Localize25" runat="server" Text="<%$ Resources:CommonTerms, KeywordLists %>"></asp:Localize>
                            </h3>
                            <div class="DoClear">
                            </div>
                        </div>
                        <ul id="treeKeywordList" class="simpleTree">
                            <li class="root" id='Li1'>
                                <cc1:CustomRepeater DataSourceID="odsKeywordList" ID="CustomRepeaterKeywordList"
                                    runat="server">
                                    <HeaderTemplate>
                                        <ul>
                                            <li class="AddCommandElementList KeywordListAdd"><span tabindex='51'>
                                                <asp:Localize ID="Localize24" runat="server" meta:resourcekey="NewKeywordList"></asp:Localize>
                                            </span></li>
                                        </ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <ul>
                                            <li class='open KeywordList'><span>
                                                <%# Eval("Name") ?? "Unnamed KeywordList" %></span>
                                                <ul>
                                                    <li class='AddCommandElement KeywordAdd' keyword_list_id='<%# Eval("Id") %>'><span
                                                        tabindex='52'>
                                                        <asp:Localize ID="Localize26" runat="server" meta:resourcekey="AddNewKeyword"></asp:Localize>
                                                    </span></li>
                                                    <cc1:CustomRepeater ID="CustomRepeater1" DataSource='<%# Eval("Keyword") %>' runat="server">
                                                        <ItemTemplate>
                                                            <li class="doc" keyword_id='<%# Eval("Id") %>'><span>
                                                                <%# Eval("Keyword") ?? "Unnamed Keyword" %>
                                                            </span></li>
                                                        </ItemTemplate>
                                                    </cc1:CustomRepeater>
                                                </ul>
                                            </li>
                                        </ul>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </cc1:CustomRepeater>
                            </li>
                        </ul>
                        <div class="ToolbarCenter">
                            <ul>
                                <li><a id='deleteKeywordOrKeywordList' href='###' class='LinkCommandRound'><span>
                                    <asp:Localize ID="Localize27" runat="server" Text="<%$ Resources:CommonTerms, Delete %>"></asp:Localize>
                                </span></a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="DoClear">
                    </div>
                    <div id='InlineMessage'>
                    </div>
                </div>
                <div id="BulkLoader" class="DivForDialog">
                    <div class="wrapper-popUp">
                        <asp:FormView Width="100%" OnItemInserted="BulkLoadInserted" ID="FormView1" DataSourceID="odsKeywordsBulk"
                            DefaultMode="Insert" runat="server">
                            <InsertItemTemplate>
                                <div class="FormCSS">
                                    <div class="Field OneLine">
                                        <label>
                                            <%= Resources.CommonTerms.KeywordList %>
                                        </label>
                                        <asp:TextBox CssClass="FormText Required" ID="TextBoxKeywordListName" Text='<%# Bind("keywordListName") %>'
                                            runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="TextBoxKeywordListName" ID="RequiredFieldValidator1"
                                            ValidationGroup="ProcessBulk" runat="server" Text="*" ErrorMessage="Keyword List name is required"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="Field">
                                        <label>
                                            <%= Resources.CommonTerms.Keywords  %>
                                        </label>
                                        <asp:TextBox CssClass="FormText Required" ID="TextBoxkeywords" Rows="10" TextMode="MultiLine"
                                            runat="server" Text='<%# Bind("keywords") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="TextBoxkeywords" ID="RequiredFieldValidator2"
                                            ValidationGroup="ProcessBulk" runat="server" Text="*" ErrorMessage="Keywords required"></asp:RequiredFieldValidator>
                                        <span class="Hint">
                                            <%= GetLocalResourceObject("KeywordsSeparated")  %>
                                        </span>
                                    </div>
                                </div>
                                <div class="ButtonBar CenterWrapper">
                                    <ul>
                                        <li>
                                            <cc1:LinkButtonRound CssClass="button Save" CommandName="Insert" Text='<%# Resources.CommonTerms.Add %>'
                                                ID="LinkButtonRoundProcessBulk" ValidationGroup="ProcessBulk" runat="server">
                                            &nbsp;&nbsp;&nbsp;
                                            </cc1:LinkButtonRound></li>
                                        <li>
                                            <cc1:HyperLinkRound CssClass="button ClosePopUp" ID="HyperLinkRound1" NavigateUrl="#"
                                                Text='<%# Resources.CommonTerms.Cancel %>' runat="server"></cc1:HyperLinkRound>
                                        </li>
                                    </ul>
                                </div>
                            </InsertItemTemplate>
                        </asp:FormView>
                    </div>
                </div>
            </div>
        </cc1:RoundPanel>

        <script type="text/javascript">
        $(function() {
            $('#bulkLoad').click(function() {
                if (TreeHelper.CurrentTree) TreeHelper.CurrentTree.lostFocus();
                $.showPopUp('BulkLoader', {
                    width: 370,
                    title: " <%= GetLocalResourceObject("KeywordListBulkLoader") %>",
                    onShow: function(dlg) {                        
                        dlg 
                         .find('a.button.Save')
                            .bind('click',function() {                                                                               
                                if (Page_ClientValidate && Page_ClientValidate("ProcessBulk")) { $(this).disable(true,{ disableClass : 'disabled' }); return true; }
                            });
                        
                        dlg                                                        
                            .end()
                            .find(':input')
                            .val("") //clear the inputs
                            .end()
                            .find(':input:first')
                            .focus();
                            
                            
                    }
                });
                
                return false;
            });
        });
        </script>

        <cc1:RoundPanel ID="RoundPanel3" runat="server">
            <div class="xTable" style="min-height: 400px">
                <div id="ManageUsersTabs" class="flora">
                    <ul>
                        <li><a href="#fragment-1"><span>
                            <asp:Localize ID="Localize28" runat="server" meta:resourcekey="ManageUsers"></asp:Localize>
                        </span></a></li>
                        <li><a href="#fragment-2"><span>
                            <asp:Localize ID="Localize29" runat="server" meta:resourcekey="AdvancedManageUsers"></asp:Localize>
                        </span></a></li>
                    </ul>
                    <div id="fragment-1">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="Toolbar">
                                    <asp:LinkButton ID="LnkRefreshControlSimple" CssClass="MiniCommand" OnClick="LnkRefreshControl_OnClick"
                                        CausesValidation="false" runat="server">
                                        [
                                        <asp:Localize ID="Localize30" runat="server" Text="<%$ Resources:CommonTerms, Refresh %>"></asp:Localize>
                                        ]
                                    </asp:LinkButton>
                                </div>
                                <uc2:ManageUsersControl OnUserAddedToProject="OnUserAdded" ID="ManageUsersControl1"
                                    runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="fragment-2">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="Toolbar">
                                    <asp:LinkButton ID="LnkRefreshControlAdvanced" CssClass="MiniCommand" OnClick="LnkRefreshControl_OnClick"
                                        CausesValidation="false" runat="server">
                                        [
                                        <asp:Localize ID="Localize31" runat="server" Text="<%$ Resources:CommonTerms, Refresh %>"></asp:Localize>
                                        ]
                                    </asp:LinkButton>
                                </div>
                                <uc2:ManageUsersControl OnUserAddedToProject="OnUserAdded" ShowAdvanced="true" ID="ManageUsersControl2"
                                    runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </cc1:RoundPanel>

        <script type="text/javascript">
        $(function() {
            $('#ManageUsersTabs > ul').tabs();
                                 
        });
        $.onDomReady(function () {
            $('#ManageUsersTabs .CenterWrapper').fitToChildrenWidth();
        });
        </script>

        <asp:ObjectDataSource ID="odsCompetitors" TypeName="SEOToolSet.Providers.ProjectManager"
            SelectMethod="GetCompetitorsByProjectId" runat="server">
            <SelectParameters>
                <asp:ControlParameter ControlID="__Page" Name="idProject" PropertyName="IdProject"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsKeywordList" TypeName="SEOToolSet.Providers.ProjectManager"
            SelectMethod="GetKeywordLists" runat="server">
            <SelectParameters>
                <asp:ControlParameter ControlID="__Page" Name="idProject" PropertyName="IdProject"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource runat="server" ID="odsKeywordsBulk" SelectMethod="GetKeywordLists"
            TypeName="SEOToolSet.Providers.ProjectManager" InsertMethod="CreateKeywordListBulk">
            <SelectParameters>
                <asp:ControlParameter ControlID="__Page" Name="idProject" PropertyName="IdProject"
                    Type="Int32" />
            </SelectParameters>
            <InsertParameters>
                <asp:ControlParameter ControlID="__Page" Name="idProject" PropertyName="IdProject"
                    Type="Int32" />
                <asp:Parameter Type="Int32" Name="idKeywordList" />
                <asp:Parameter Type="String" Name="keywordListName" />
                <asp:Parameter Type="String" Name="keywords" />
            </InsertParameters>
        </asp:ObjectDataSource>
    </asp:Panel>
</asp:Content>
