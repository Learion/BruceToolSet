/**
* RRRM
* JMenu is a JQuery version of the ADxMenu, it has been modified in order to work with JQuery and to use just one CSS file for IE 6, IE 7 and Firefox
* the credits for his work could be found in the lines below
*/
/*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
ADxMenu.js - v4 (4.21) - helper script for Internet Explorer, up to version 6
http://aplus.co.yu/adxmenu/
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
(c) Copyright 2003, 2008, Aleksandar Vacic, aplus.co.yu
This work is licensed under the CC Attribution 3.0 Unported license
To view a copy of this license, visit http://creativecommons.org/licenses/by/3.0/ or
send a letter to Creative Commons, 559 Nathan Abbott Way, Stanford, California 94305, USA
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*/

function JMenu_Setup() {    
    var aUL, aA;
    var aTmp2 = $('ul.adxm li');	    
        aTmp2.each(function() {
            aUL = $(this).find("ul");
            //	if item has submenu, then make the item hoverable
            if (aUL.length) {
                this.UL = aUL[0]; //	direct submenu
                aA = $(this).find("a");
                if (aA.length) {
                    this.A = aA[0]; //	direct child link
                }
                $(this).hover(function() {
                    var jThis = $(this);
                    var UL = $(jThis[0].UL);
                    var A = $(jThis[0].A);
                    jThis.addClass("adxmhover");
                    UL.addClass("adxmhoverUL");
                    A.addClass("adxmhoverA");

                }, function() {
                    var jThis = $(this);
                    var UL = $(jThis[0].UL);
                    var A = $(jThis[0].A);

                    jThis.removeClass("adxmhover");
                    UL.removeClass("adxmhoverUL");
                    A.removeClass("adxmhoverA");
                });

            }
        });    
}


$(document).ready(function() {
    JMenu_Setup();
});
