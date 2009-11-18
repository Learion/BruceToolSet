
/**
 * TestSuite allows the Test to be registered inside a testsuite
 */
function TestSuite(){
	var _objTest = new Object();
	var _me = this;
	
	    this.findTestInPage = function() {
		$('.Test').each(function (ix) {
			var ref = $.byId(this.id);
			var testName = ref.attr('test_name');
			var testClass = ref.attr('test_class');
			try {
				var ObjTest = eval($.stringFormat("new {0}()",testClass )); 			
				_me.registerTest(testName,  ObjTest);				
			}
			catch(ex) {
				console.log("[Error] : can't instantiate the Test" + ex.message)
			}
		});
	}
	
	this.geTestsNames = function () {
		var arr = new Array();
		for (var key in _objTest) {
			arr.push(key);
		}
		
		return arr;
	}
	
	this.registerTest = function(name, ObjTest) {
		_objTest[name] = ObjTest;
	}
	this.setup = function () {
		for (var key in _objTest) {
			if (_objTest[key] != null) {
				console.log('calling setup for ' + key);
				_objTest[key].setup();
			}
		}
	}
	this.run = function () {
		for (var key in _objTest) {
			if (_objTest[key] != null) {
				console.log('calling run for ' + key);				
				_objTest[key].run();
			}
		}
	}
	this.tearDown = function() {
		for (var key in _objTest) {
			if (_objTest[key] != null) {
				console.log('calling tearDown for ' + key);
				_objTest[key].tearDown();
			}
		}
	}
}


function Test () {
	var _me = this;	
	var _passed = 0;
	var _failed = 0;
	
	this.evaluate = function (func,key) {
		if (typeof(func) == "function") {
			try {
				func();
				$.logResult(key, 'Pass Assync', true);
			}
			catch(ex) {
				$.logResult(key,'Fail Assync --> ' + ex.message,false);
			}
		}
	}
	
	this.run = function () {	
		if ( typeof(_me["setup"]) == 'function' ) _me["setup"]();
		for (var key in _me) {
			if (key != 'run' && key != 'setup' && key != 'tearDown' && key != 'evaluate') {
				try  {										
					if (typeof(_me[key]) == 'function') {
						_me[key]();

						if (key.indexOf('async_') == -1)  //async_ methods will be evaluated using an inner callback, so the result will appear after a short delay
							$.logResult(key, 'Pass', true);
					}
					else {
						continue;
					}			
				}
				catch(ex) {
					$.logResult(key,'Fail --> ' + ex.message,false);
				}
			}
		}
		if ( typeof(_me["tearDown"]) == 'function' ) _me["tearDown"]();
	}
}
