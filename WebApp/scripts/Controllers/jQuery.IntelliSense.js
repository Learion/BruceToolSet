// jQuery Intellisense Stub
// SmallSharpTools.com

// Xml Doc for JavaScript:
// http://weblogs.asp.net/bleroy/archive/2007/04/23/the-format-for-javascript-doc-comments.aspx
// http://blogs.msdn.com/webdevtools/archive/2007/03/02/jscript-intellisense-in-orcas.aspx

var jQuery = function(expression, context)
{
    /// <summary>jQuery</summary>
    /// <param name="expression" type="String">string</param>
    /// <param name="context" type="jQuery">jQuery</param>
    /// <returns type="jQuery">jQuery</returns>
};

var $ = jQuery;

jQuery.prototype = {
    each : function(callback) {
    /// <summary>Execute a function within the context of every matched element.</summary>
    /// <param name="callback" type="Function">function</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    size : function() {
    /// <summary>The number of elements in the jQuery object.</summary>
    /// <returns type="jQuery">jQuery</returns>
    },
    length : function() {
    /// <summary>The number of elements in the jQuery object.</summary>
    /// <returns type="jQuery">jQuery</returns>
    },
    eq : function(position) {
    /// <summary>Reduce the set of matched elements to a single element.</summary>
    /// <param name="position" type="String">string</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    get : function(index) {
    /// <summary>Access all matched DOM elements.</summary>
    /// <param name="index" type="int">number</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    index : function(subject) {
    /// <summary>Searches every matched element for the object and returns the index of the element, if found, starting with zero.</summary>
    /// <param name="subject">object</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    attr : function(key, value) {
    /// <summary>Set a single property to a value, on all matched elements.</summary>
    /// <param name="key" type="String">string</param>
    /// <param name="value" type="String">string</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    addClass : function(className) {
    /// <summary>Set a single property to a value, on all matched elements.</summary>
    /// <param name="className" type="String">string</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    removeClass : function(className) {
    /// <summary>Removes all or the specified class(es) from the set of matched elements.</summary>
    /// <param name="className" type="String">string</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    toggleClass : function(className) {
    /// <summary>Adds the specified class if it is not present, removes the specified class if it is present.</summary>
    /// <param name="className" type="String">string</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    html : function(val) {
    /// <summary>Set the html contents of every matched element. This property is not available on XML documents (although it will work for XHTML documents).</summary>
    /// <param name="val" type="String">string</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    text : function(val) {
    /// <summary>Set the text contents of all matched elements.</summary>
    /// <param name="val" type="String">string</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    val : function(val) {
    /// <summary>Set the value attribute of every matched element.</summary>
    /// <param name="val" type="String">string</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    ready : function(fn) {
    /// <summary>Binds a function to be executed whenever the DOM is ready to be traversed and manipulated.</summary>
    /// <param name="fn" type="Function">function</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    bind : function(type, data, fn) {
    /// <summary>Binds a handler to a particular event (like click) for each matched element. Can also bind custom events.</summary>
    /// <param name="type" type="String">string</param>
    /// <param name="data" type="String">string</param>
    /// <param name="fn" type="Function">function</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    unbind : function(type, data) {
    /// <summary>This does the opposite of bind, it removes bound events from each of the matched elements.</summary>
    /// <param name="type" type="String">string</param>
    /// <param name="data" type="String">string</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    click : function(fn) {
    /// <summary>Binds a function to the click event of each matched element.</summary>
    /// <param name="fn" type="Function">function</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    show: function(speed, callback) {
    /// <summary>Displays each of the set of matched elements if they are hidden.</summary>
    /// <param name="speed" type="int">number</param>
    /// <param name="callback" type="Function">function</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    hide: function(speed, callback) {
    /// <summary>Hide all matched elements using a graceful animation and firing an optional callback after completion.</summary>
    /// <param name="speed" type="int">number</param>
    /// <param name="callback" type="Function">function</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    toggle: function() {
    /// <summary>Toggles each of the set of matched elements.</summary>
    /// <returns type="jQuery">jQuery</returns>
    },
    slideDown: function(speed, callback) {
    /// <summary>Reveal all matched elements by adjusting their height and firing an optional callback after completion.</summary>
    /// <param name="speed" type="int">number</param>
    /// <param name="callback" type="Function">function</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    slideUp: function(speed, callback) {
    /// <summary>Hide all matched elements by adjusting their height and firing an optional callback after completion.</summary>
    /// <param name="speed" type="int">number</param>
    /// <param name="callback" type="Function">function</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    slideToggle: function(speed, callback) {
    /// <summary>Toggle the visibility of all matched elements by adjusting their height and firing an optional callback after completion.</summary>
    /// <param name="speed" type="int">number</param>
    /// <param name="callback" type="Function">function</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    fadeIn: function(speed, callback) {
    /// <summary>Fade in all matched elements by adjusting their opacity and firing an optional callback after completion.</summary>
    /// <param name="speed" type="int">number</param>
    /// <param name="callback" type="Function">function</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    fadeOut: function(speed, callback) {
    /// <summary>Fade out all matched elements by adjusting their opacity and firing an optional callback after completion.</summary>
    /// <param name="speed" type="int">number</param>
    /// <param name="callback" type="Function">function</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    fadeTo: function(speed, opacity, callback) {
    /// <summary>Fade the opacity of all matched elements to a specified opacity and firing an optional callback after completion.</summary>
    /// <param name="speed" type="int">number</param>
    /// <param name="opacity" type="int">number</param>
    /// <param name="callback" type="Function">function</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    animate: function(params, duration, easying, callback) {
    /// <summary>A function for making your own, custom animations.</summary>
    /// <param name="params">object</param>
    /// <param name="duration" type="int">number</param>
    /// <param name="easying" type="int">number</param>
    /// <param name="callback" type="Function">function</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    stop: function() {
    /// <summary>Stops all the currently running animations on all the specified elements.</summary>
    /// <returns type="jQuery">jQuery</returns>
    },
    queue: function(callback) {
    /// <summary>Adds a new function, to be executed, onto the end of the queue of all matched elements.</summary>
    /// <param name="callback" type="Function">function</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    dequeue: function() {
    /// <summary>Removes a queued function from the front of the queue and executes it.</summary>
    /// <param name="PARAM">PARAMDESC</param>
    /// <returns type="jQuery">jQuery</returns>
    },
    ajax: function(options) {
    /// <summary>Load a remote page using an HTTP request.</summary>
    /// <param name="options">object</param>
    /// <returns type="XMLHttpRequest">request</returns>
    },
    serialize: function() {
    /// <summary>Serializes a set of input elements into a string of data. This will serialize all given elements.</summary>
    /// <returns type="jQuery">jQuery</returns>
    },
    serializeArray: function() {
    /// <summary>Serializes all forms and form elements (like the .serialize() method) but returns a JSON data structure for you to work with.</summary>
    /// <returns type="jQuery">jQuery</returns>
    }
};