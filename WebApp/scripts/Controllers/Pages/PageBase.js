$.Namespace('$.SEOToolSet');
$.Namespace('$.CurrentPage');

$.SEOToolSet.PageBase = function() {

    this.idPreviousProject = -1;

    this.init = function() {
        var me = this;
        var cbx = $.CurrentPage.cbxProjects;
        if (cbx !== null) {
            cbx.addEventListener('Changed', function(args) {
                me.dispatchEvent(
                    {
                        type: "ProjectChanged",
                        IdProject: cbx.getDropDown().val()
                    });
            });

            //Storing the previous project 
            cbx.addEventListener('Changing', function(args) {
                //Avoid reloading when the project selected is the current
                if (args.itemJNode.attr('cbx_value') == cbx.getDropDown().val()) {
                    args.cancel = true;
                    return false;
                }
                me.idPreviousProject = cbx.getDropDown().val();
            });
        }
        else {
            me.dispatchEvent(
                {
                    type: 'Error',
                    errorMessage: 'The Projects Combobox was not found on the page',
                    errorCode: 'ProjectComboNotFound'
                });
        }
    };

    this.raiseProjectChanged = function() {
        var cbx = $.CurrentPage.cbxProjects;
        this.dispatchEvent(
            {
                type: "ProjectChanged",
                IdProject: cbx.getDropDown().val()
            });
    };

    /**
    * Return the id from the previous selected project 
    **/
    this.getPreviousIdProject = function() {
        return this.idPreviousProject;
    };

    /**
    * Return the id from the current selected project
    **/
    this.getCurrentIdProject = function() {
        if ($.CurrentPage.cbxProjects) {
            return $.CurrentPage.cbxProjects.getDropDown().val();
        }
        return -1;
    };

    this.getCurrentProjectName = function() {
        if ($.CurrentPage.cbxProjects) {
            return $.CurrentPage.cbxProjects.getSelectedItemText();
        }
        return "--";
    };

    this.CheckValidProject = function() {
        var me = this;
        if (me.getCurrentIdProject() == "-1") {
            me.dispatchEvent(
                {
                    type: 'NoActiveProjects',
                    idPreviousProject: me.getPreviousIdProject()
                });
            return false;
        }
        return true;
    };
};

$.SEOToolSet.PageBase.inherits($.EventDispatcher);
