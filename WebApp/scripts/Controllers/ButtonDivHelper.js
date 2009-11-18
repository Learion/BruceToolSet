///<reference path="jQuery.IntelliSense.js" />
function SetButtonEnable(){
   $("#divProject").show()
   $("#divProjectDefault").hide();
   $("#afirst").hide();
   $("#spanp").show();
}

function ClearManageForm(txt){
   $(txt).val("");
   return false;
}

function ResetAddProjectlbtn(){
   $("#afirst").show();
   $("#spanp").hide();
}