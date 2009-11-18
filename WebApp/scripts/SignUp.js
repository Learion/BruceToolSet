function signUpLoad() {
    addClearAddress("#UserNewAddressLink", "ctl00_contentArea_CreateAccountWizard_CreateUserFormView_");
    addClearAddress("#BillingInformationNewAddressLink", "ctl00_contentArea_CreateAccountWizard_BillingInformationFormView_");
    addConfirm("#ctl00_contentArea_CreateAccountWizard_CancelAccountStepCommand");
    addConfirm("#ctl00_contentArea_CreateAccountWizard_CancelBillingInformationStepCommand");
    addConfirm("#ctl00_contentArea_CreateAccountWizard_CancelUserStepCommand");
    addConfirm("#ctl00_contentArea_CreateAccountWizard_BillingConfirmationFormView_CancelCommand");
    passwordMirroring();
    prepopulateCardholder();
    togglePayForm();
    prepareChangePassword();
    addAjaxValidation();
    
    $('a,input,select').fixTabIndex();
    
  /*  $().ajaxSend(function(r, s){
        $("#ContentLoadingDiv").show();
    });
    $().ajaxStop(function(r, s){
        $("#ContentLoadingDiv").fadeOut("fast");  
    });*/
}
function togglePayForm() {
    $("#CreditCardTabs li").click(
                function() {
                    $("#CreditCardTabs li").removeClass("Active");
                    $(this).addClass("Active");
                    $(".CreditCardPanel").toggle();
                }
            );
}
function addClearAddress(commandName, clearAt) {
    var clearCommand = $(commandName);
    if (clearCommand) {
        clearCommand.click(
                    function() {
                        $('#' + clearAt + 'CountryDropDownList').val(-1);
                        $('#' + clearAt + 'Address1TextBox').val('');
                        $('#' + clearAt + 'Address2TextBox').val('');
                        $('#' + clearAt + 'CityTextBox').val('');
                        $('#' + clearAt + 'StateTextBox').val('');
                        $('#' + clearAt + 'ZipTextBox').val('');
                        if ($('#' + clearAt + 'PhoneTextBox')) $('#' + clearAt + 'PhoneTextBox').val('');
                    }
                );
    }
}
function copyAddress(copyFrom, copyTo) {
    $('#' + copyTo + 'CountryDropDownList').val($('#' + copyFrom + 'CountryAddressHidden').val());
    $('#' + copyTo + 'Address1TextBox').val($('#' + copyFrom + 'AddressAddress1Hidden').val());
    $('#' + copyTo + 'Address2TextBox').val($('#' + copyFrom + 'AddressAddress2Hidden').val());
    $('#' + copyTo + 'CityTextBox').val($('#' + copyFrom + 'CityAddressHidden').val());
    $('#' + copyTo + 'StateTextBox').val($('#' + copyFrom + 'StateAddressHidden').val());
    $('#' + copyTo + 'ZipTextBox').val($('#' + copyFrom + 'ZipAddressHidden').val());
    if ($('#' + copyTo + 'PhoneTextBox')) $('#' + copyTo + 'PhoneTextBox').val($('#' + copyFrom + 'PhoneAddressHidden').val());
}
function passwordMirroring() {
    var passwordInput = $("#ctl00_contentArea_CreateAccountWizard_CreateUserFormView_PwTextBox");
    var retypePasswordInput = $("#ctl00_contentArea_CreateAccountWizard_CreateUserFormView_RetypePasswordTextBox");
    var passwordHidden = $("#ctl00_contentArea_UserCopyFormView_PasswordHidden");
    if (passwordInput && passwordInput.val() == "") {
        passwordInput.val(passwordHidden.val());
        retypePasswordInput.val(passwordHidden.val());
    }
}
function prepopulateCardholder() {
    var userFirstName = $("#ctl00_contentArea_UserCopyFormView_FirstNameHidden").val();
    var userLastName = $("#ctl00_contentArea_UserCopyFormView_LastNameHidden").val();
    var cardholderInput = $("#ctl00_contentArea_CreateAccountWizard_BillingInformationFormView_CreditCardCardholderTextBox");
    if (cardholderInput && cardholderInput.val() == "") {
        cardholderInput.val(userFirstName + " " + userLastName);
    }
}
function addConfirm(commandName) {
    var cancelCommand = $(commandName);
    var commandPostback = commandName.replace(/_/g, "$").substring(1);
    if (cancelCommand) {
        cancelCommand.click(
                    function() {
                        $.showConfirm($.getResourceString("AreSureQuestion", "Are you sure?"), {
                            onOkClicked: function() {
                                __doPostBack(commandPostback, '');
                            },
                            onCancelClicked: function() { }
                        });
                        return false;
                    }
                );
    }
}
function prepareChangePassword() {
    var changePasswordCommand = $("#ctl00_contentArea_CreateAccountWizard_CreateUserFormView_ChangePasswordCommand");
    if (changePasswordCommand && changePasswordCommand.length > 0) {
        $(".Password").css("display", "none");
        changePasswordCommand.click(function() {
            $(".Password").toggle("slow");
        });
    }
}

function addAjaxValidation() {
    firstTime = false;
    var inputAccount = $.byId("ctl00_contentArea_CreateAccountWizard_CreateAccountFormView_NameCreateAccountInput");
    var inputLogin = $.byId("ctl00_contentArea_CreateAccountWizard_CreateUserFormView_LgTexBox");
    var inputEmail = $.byId("ctl00_contentArea_CreateAccountWizard_CreateUserFormView_EmailTextBox");
    var accountExistsMessage = $.getResourceString("AccountNameNotUnique", "Choose another account name. The account name is used by another account");
    var loginExistsMessage = $.getResourceString("LoginNameNotUnique", "Choose another login name. The login name is used by another user");
    var emailExistsMessage = $.getResourceString("EmailNotUnique", "Choose another email. The email is used by another user");
    inputAccount.change(function() {
        $.post("Handler/AjaxValidations.ashx",
            { action: "CheckAccountName", accountName: inputAccount.val() },
            function(data) {
                if (data.result == true) { //if the account name is available
                    inputAccount.removeClass("InputError");
                    $.hideInLineMessage($("#AccountInlineMessage"));
                } else {
                    inputAccount.focus();
                    inputAccount.select();
                    inputAccount.addClass("InputError");
                    $.showInlineMessage($("#AccountInlineMessage"), accountExistsMessage, { sticky: true });
                }
            },
            "json"
        );
    });
    inputLogin.change(function() {
        $.post("Handler/AjaxValidations.ashx",
            { action: "CheckLoginName", loginName: inputLogin.val() },
            function(data) {
                if (data.result == true) { //if the login name is available
                    inputLogin.removeClass("InputError");
                    $.hideInLineMessage($("#UserInlineMessage"));
                } else {
                    inputLogin.focus();
                    inputLogin.select();
                    inputLogin.addClass("InputError");
                    $.showInlineMessage($("#UserInlineMessage"), loginExistsMessage, { sticky: true });
                }
            },
            "json"
        );
    });

    inputEmail.change(function() {
        $.post("Handler/AjaxValidations.ashx",
            { action: "CheckEmail", email: inputEmail.val() },
            function(data) {
                if (data.result == true) { //if the email is available
                    inputEmail.removeClass("InputError");
                    $.hideInLineMessage($("#UserInlineMessage"));
                } else {
                    inputEmail.focus();
                    inputEmail.select();
                    inputEmail.addClass("InputError");
                    $.showInlineMessage($("#UserInlineMessage"), emailExistsMessage, { sticky: true });
                }
            },
            "json"
        );
    });
}