/*global $, jQuery, document */


/**
 * @author rriojas
 */
jQuery.extend({
    logResult: function(name, result, b_pass){
        var aConsole = $('#logDiv');
        if (!aConsole.length) {
            $('<div id="logDiv"><ul></ul></div>').appendTo('body');
            aConsole = $('#logDiv');
        }
        var className = b_pass ? 'Pass' : 'Fail';
        aConsole.find('ul').append($.stringFormat('<li class="{0}"><p>Test {1} : {2}</p></li>', className, name, result));
    },
    UNDEFINED_VALUE: undefined,
    _innerTypeOf: function(someConstructor){
        var result = null;
        switch (someConstructor) {
            case String:
                result = 'String';
                break;
            case Boolean:
                result = 'Boolean';
                break;
            case Number:
                result = 'Number';
                break;
            case Array:
                result = 'Array';
                break;
            case RegExp:
                result = 'RegExp';
                break;
            case Function:
                result = 'Function';
                break;
            default:
                var m = someConstructor.toString().match(/function\s*([^( ]+)\(/);
                if (m) {
                    result = m[1];
                }
                break;
        }
        return result;
    },
    _trueTypeOf: function(something){
        var result = typeof(something);
        try {
            switch (result) {
                case 'string':
                case 'boolean':
                case 'number':
                    break;
                case 'object':
                case 'function':
                    result = jQuery._innerTypeOf(something.constructor);
                    break;
            }
        }
        finally {
            if (result) 
			{
				result = result.substr(0, 1).toUpperCase() + result.substr(1);
			}
            return result;
        }
    },
    _displayStringForValue: function(aVar){
        var result = '<' + aVar + '>';
        if (!(aVar === null || aVar === jQuery.UNDEFINED_VALUE)) {
            result += ' (' + jQuery._trueTypeOf(aVar) + ')';
        }
        return result;
    },
    fail: function(failureMessage){
        throw new jQuery.JsUnitException("Call to fail()", failureMessage);
    },
    JsUnitException: function(comment, message){
        this.isJsUnitException = true;
        this.comment = comment;
		this.message = message;
        this.jsUnitMessage = message;
        this.stackTrace = jQuery.getStackTrace();
    },
    
    
    getFunctionName: function(aFunction){
        var regexpResult = aFunction.toString().match(/function(\s*)(\w*)/);
        if (regexpResult && regexpResult.length >= 2 && regexpResult[2]) {
            return regexpResult[2];
        }
        return 'anonymous';
    },
    
    getStackTrace: function(){
        var result = '';
        
        if (typeof arguments.caller != 'undefined') { // IE, not ECMA
            for (var a = arguments.caller; a !== null; a = a.caller) {
                result += '> ' + jQuery.getFunctionName(a.callee) + '\n';
                if (a.caller == a) {
                    result += '*';
                    break;
                }
            }
        }
        else { // Mozilla, not ECMA
            // fake an exception so we can get Mozilla's error stack            
            try {
				var foo = null;
                foo.bar = '';
            } 
            catch (testExcp) {
                var stack = jQuery.parseErrorStack(testExcp);
                for (var i = 1; i < stack.length; i++) {
                    result += '> ' + stack[i] + '\n';
                }
            }
        }
        
        return result;
    },
    
    parseErrorStack: function(excp){
        var stack = [];
        var name;
        
        if (!excp || !excp.stack) {
            return stack;
        }
        
        var stacklist = excp.stack.split('\n');
        
        for (var i = 0; i < stacklist.length - 1; i++) {
            var framedata = stacklist[i];
            
            name = framedata.match(/^(\w*)/)[1];
            if (!name) {
                name = 'anonymous';
            }
            
            stack[stack.length] = name;
        }
        // remove top level anonymous functions to match IE
        
        while (stack.length && stack[stack.length - 1] == 'anonymous') {
            stack.length = stack.length - 1;
        }
        return stack;
    },
    
    error: function(errorMessage){
        var errorObject = {};
        errorObject.description = errorMessage;
        errorObject.stackTrace = jQuery.getStackTrace();
        throw errorObject;
    },
    argumentsIncludeComments: function(expectedNumberOfNonCommentArgs, args){
        return args.length == expectedNumberOfNonCommentArgs + 1;
    },
    commentArg: function(expectedNumberOfNonCommentArgs, args){
        if (jQuery.argumentsIncludeComments(expectedNumberOfNonCommentArgs, args)) 
		{ return args[0]; }
        return null;
    },
    nonCommentArg: function(desiredNonCommentArgIndex, expectedNumberOfNonCommentArgs, args){
        return jQuery.argumentsIncludeComments(expectedNumberOfNonCommentArgs, args) ? args[desiredNonCommentArgIndex] : args[desiredNonCommentArgIndex - 1];
    },
    _validateArguments: function(expectedNumberOfNonCommentArgs, args){
        if (!(args.length == expectedNumberOfNonCommentArgs ||
		(args.length == expectedNumberOfNonCommentArgs + 1 && (typeof args[0] == 'string')))) 
		{
			jQuery.error('Incorrect arguments passed to assert function');
		}
    },
    _assert: function(comment, booleanValue, failureMessage){
        if (!booleanValue) 
		{  throw new jQuery.JsUnitException(comment, failureMessage); }
        
    },
    assert: function(){
        jQuery._validateArguments(1, arguments);
        var booleanValue = jQuery.nonCommentArg(1, 1, arguments);
        
        if (typeof booleanValue != 'boolean') 
		{
			jQuery.error('Bad argument to assert(boolean)');
		}
        
        jQuery._assert(jQuery.commentArg(1, arguments), booleanValue === true, 'Call to assert(boolean) with false');
    },
    assertTrue: function(){
        jQuery._validateArguments(1, arguments);
        var booleanValue = jQuery.nonCommentArg(1, 1, arguments);
        
        if (typeof booleanValue != 'boolean') 
		{
			jQuery.error('Bad argument to assertTrue(boolean)');
		}
        
        jQuery._assert(jQuery.commentArg(1, arguments), booleanValue === true, 'Call to assertTrue(boolean) with false');
    },
    assertFalse: function(){
        jQuery._validateArguments(1, arguments);
        var booleanValue = jQuery.nonCommentArg(1, 1, arguments);
        
        if (typeof booleanValue != 'boolean') 
		{
			jQuery.error('Bad argument to assertFalse(boolean)');
		}
        
        jQuery._assert(jQuery.commentArg(1, arguments), booleanValue === false, 'Call to assertFalse(boolean) with true');
    },
    assertEquals: function(){
        jQuery._validateArguments(2, arguments);
        var var1 = jQuery.nonCommentArg(1, 2, arguments);
        var var2 = jQuery.nonCommentArg(2, 2, arguments);
        jQuery._assert(jQuery.commentArg(2, arguments), var1 === var2, 'Expected ' + jQuery._displayStringForValue(var1) + ' but was ' + jQuery._displayStringForValue(var2));
    },
    assertNotEquals: function(){
        jQuery._validateArguments(2, arguments);
        var var1 = jQuery.nonCommentArg(1, 2, arguments);
        var var2 = jQuery.nonCommentArg(2, 2, arguments);
        jQuery._assert(jQuery.commentArg(2, arguments), var1 !== var2, 'Expected not to be ' + jQuery._displayStringForValue(var2));
    },
    assertNull: function(){
        jQuery._validateArguments(1, arguments);
        var aVar = jQuery.nonCommentArg(1, 1, arguments);
        jQuery._assert(jQuery.commentArg(1, arguments), aVar === null, 'Expected ' + jQuery._displayStringForValue(null) + ' but was ' + jQuery._displayStringForValue(aVar));
    },
    assertNotNull: function(){
        jQuery._validateArguments(1, arguments);
        var aVar = jQuery.nonCommentArg(1, 1, arguments);
        jQuery._assert(jQuery.commentArg(1, arguments), aVar !== null, 'Expected not to be ' + jQuery._displayStringForValue(null));
    },
    assertUndefined: function(){
        jQuery._validateArguments(1, arguments);
        var aVar = jQuery.nonCommentArg(1, 1, arguments);
        jQuery._assert(jQuery.commentArg(1, arguments), aVar === jQuery.UNDEFINED_VALUE, 'Expected ' + jQuery._displayStringForValue(jQuery.UNDEFINED_VALUE) + ' but was ' + jQuery._displayStringForValue(aVar));
    },
    assertNotUndefined: function(){
        jQuery._validateArguments(1, arguments);
        var aVar = jQuery.nonCommentArg(1, 1, arguments);
        jQuery._assert(jQuery.commentArg(1, arguments), aVar !== jQuery.UNDEFINED_VALUE, 'Expected not to be ' + jQuery._displayStringForValue(jQuery.UNDEFINED_VALUE));
    },
    assertNaN: function(){
        jQuery._validateArguments(1, arguments);
        var aVar = jQuery.nonCommentArg(1, 1, arguments);
        jQuery._assert(jQuery.commentArg(1, arguments), isNaN(aVar), 'Expected NaN');
    },
    assertNotNaN: function(){
        jQuery._validateArguments(1, arguments);
        var aVar = jQuery.nonCommentArg(1, 1, arguments);
        jQuery._assert(jQuery.commentArg(1, arguments), !isNaN(aVar), 'Expected not NaN');
    },
    assertObjectEquals: function(){
        jQuery._validateArguments(2, arguments);
        var var1 = jQuery.nonCommentArg(1, 2, arguments);
        var var2 = jQuery.nonCommentArg(2, 2, arguments);
        var type;
        var msg = jQuery.commentArg(2, arguments) ? jQuery.commentArg(2, arguments) : '';
        var isSame = (var1 === var2);
        //shortpath for references to same object
        var isEqual = ((type = jQuery._trueTypeOf(var1)) == jQuery._trueTypeOf(var2));
        if (isEqual && !isSame) {
            switch (type) {
                case 'String':
                case 'Number':
                    isEqual = (var1 == var2);
                    break;
                case 'Boolean':
                case 'Date':
                    isEqual = (var1 === var2);
                    break;
                case 'RegExp':
                case 'Function':
                    isEqual = (var1.toString() === var2.toString());
                    break;
                default: //Object | Array
                    var i;
					isEqual = (var1.length === var2.length);
                    if (isEqual) {
                        for (i in var1) {
							if (var1[i]) {
	                            jQuery.assertObjectEquals(msg + ' found nested ' + type + '@' + i + '\n', var1[i], var2[i]);
							}
                        }
                    }
            }
            jQuery._assert(msg, isEqual, 'Expected ' + jQuery._displayStringForValue(var1) + ' but was ' + jQuery._displayStringForValue(var2));
        }
    },
    assertArrayEquals: jQuery.assertObjectEquals,
    
    assertEvaluatesToTrue: function(){
        jQuery._validateArguments(1, arguments);
        var value = jQuery.nonCommentArg(1, 1, arguments);
        if (!value) 
		{
			jQuery.fail(jQuery.commentArg(1, arguments));
		}
    },
    assertEvaluatesToFalse: function(){
        jQuery._validateArguments(1, arguments);
        var value = jQuery.nonCommentArg(1, 1, arguments);
        if (value) 
		{
			jQuery.fail(jQuery.commentArg(1, arguments));
		}
    },
    assertHTMLEquals: function(){
        jQuery._validateArguments(2, arguments);
        var var1 = jQuery.nonCommentArg(1, 2, arguments);
        var var2 = jQuery.nonCommentArg(2, 2, arguments);
        var var1Standardized = jQuery.standardizeHTML(var1);
        var var2Standardized = jQuery.standardizeHTML(var2);
        
        jQuery._assert(jQuery.commentArg(2, arguments), var1Standardized === var2Standardized, 'Expected ' + jQuery._displayStringForValue(var1Standardized) + ' but was ' + jQuery._displayStringForValue(var2Standardized));
    },
    assertHashEquals: function(){
        jQuery._validateArguments(2, arguments);
        var var1 = jQuery.nonCommentArg(1, 2, arguments);
        var var2 = jQuery.nonCommentArg(2, 2, arguments);
		var key = null;
        for (key in var1) {
			if (var1[key]) 
			{
				jQuery.assertNotUndefined("Expected hash had key " + key + " that was not found", var2[key]);
				jQuery.assertEquals("Value for key " + key + " mismatch - expected = " + var1[key] + ", actual = " + var2[key], var1[key], var2[key]);
			}
        }
        for (key in var2) {
			if (var2[key]) 
			{
				jQuery.assertNotUndefined("Actual hash had key " + key + " that was not expected", var1[key]);
			}
        }
    },
    assertRoughlyEquals: function(){
        jQuery._validateArguments(3, arguments);
        var expected = jQuery.nonCommentArg(1, 3, arguments);
        var actual = jQuery.nonCommentArg(2, 3, arguments);
        var tolerance = jQuery.nonCommentArg(3, 3, arguments);
        jQuery.assertTrue("Expected " + expected + ", but got " + actual + " which was more than " + tolerance + " away", Math.abs(expected - actual) < tolerance);
    },
    assertContains: function(){
        jQuery._validateArguments(2, arguments);
        var contained = jQuery.nonCommentArg(1, 2, arguments);
        var container = jQuery.nonCommentArg(2, 2, arguments);
        jQuery.assertTrue("Expected '" + container + "' to contain '" + contained + "'", container.indexOf(contained) != -1);
    },
    standardizeHTML: function(html){
        var translator = document.createElement("DIV");
        translator.innerHTML = html;
        return translator.innerHTML;
    }
    
});
