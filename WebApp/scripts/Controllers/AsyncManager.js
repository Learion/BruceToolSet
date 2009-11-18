/**
 * @author rriojas
 */
$.Namespace('$.AsyncManager');

$.AsyncManager.Manager = function(options)
{
    var _opts = 
        {
            maxSimultaneousAllowed: 3
        };
    

    $.extend(_opts, options);
    
    var _me = this;
    var _tasks = [];
	var _queuedTasks = [];
	
	var _errorHappens = false;
	
	_me.onAsyncWorkComplete = null;
	_me.onAsyncError = null;
    
    var _currentCalls = 0;
    _me.stopTasks = function() {
		_currentCalls = 0;
		while (_queuedTasks.length > 0) {
		 	var task = _queuedTasks.shift();
			task.removeEventsOfType("onSuccess");
			task.removeEventsOfType("onError");
			task.removeEventsOfType("onAbort");
			task.abort();
			task = null;
		}
	};
    _me.clearTasks = function()
    {
        _tasks = [];
    };
    _me.addTask = function(task)
    {    		
		task.addEventListener('onSuccess', function()
        {
            _me.taskFinished();
        });	

        task.addEventListener('onError',function()
        {
			if (!_errorHappens) {
				if (typeof _me.onAsyncError == 'function') {
					_me.onAsyncError();
				}
				_errorHappens = true;
			}
            _me.taskFinished();
        });
        
        _tasks.push(task);
    };
    
    _me.runTasks = function()
    {
		_me.stopTasks();
		
		_errorHappens = false;

        if (_opts.maxSimultaneousAllowed > 0) 
        {
        	_launchPendingTasks();
        }
        else 
        {
            for (var ix = 0; ix < _tasks.length; ix++) 
            {
				var task = _tasks[ix];
				_queuedTasks.push(task);				
				task.run();				
                _currentCalls++;
            }
			_onTasksFinished();
        }
    };
    
    _me.taskFinished = function(task)
    {
        console.log('Task has finished');
        _currentCalls--;
		
		if (_opts.maxSimultaneousAllowed > 0) 
		{
			if (_currentCalls === 0) 
			{
				_onTasksFinished();
			}
		}
		
        _launchPendingTasks();
		
    };	
	
	function _onTasksFinished() {
		if (typeof _me.onAsyncWorkComplete == 'function') {
			_me.onAsyncWorkComplete();
		}
	}
    
    function _launchPendingTasks()
    {
        if (_opts.maxSimultaneousAllowed > 0) 
        {
            while ((_currentCalls < _opts.maxSimultaneousAllowed) && (_tasks.length > 0)) 
            {				
                var task = _tasks.shift();
             	_queuedTasks.push(task);
                task.run();
                _currentCalls++;
            }
        }
    }
    
};



$.AsyncManager.AsyncTask = function() {
    var _me = this;   
    _me.parent.constructor.call(this);

    _me.Id = "";
    _me.serviceUrl = '';
	_me.method = null;
	_me.data = null;
    var _req = null;

    function _onResponse(data) {
        console.log('trying to dispatch the events registered for data');
        _me.dispatchEvent({ type: "onSuccess", result: data });
    }

    function _onError(XMLHttpRequest, textStatus, errorThrown) {
        console.log('trying to dispatch the events registered here for error');
        _me.dispatchEvent({ type: "onError", result: { XHR: XMLHttpRequest, status: textStatus, error: errorThrown} });
    }

    _me.run = function() {
        var evt = { type: "onBeforeCall", url: _me.serviceUrl, cancelRequest : false, timeout: 20000, data: _me.data, method : _me.method || "GET" };
        _me.dispatchEvent(evt);
        if (!evt.cancelRequest) {			
            _req = $.getJsonResponse(evt.url, _onResponse, _onError, evt.timeout, evt.method, evt.data);
        }
        else {
            _onError(null, 'request canceled by client', null);
        }

    };

    _me.abort = function() {
        if (_req) {
            _req.abort();
        }
        _me.dispatchEvent({ type: "onAbort", XHR: _req });
    };

};

$.AsyncManager.AsyncTask.inherits($.EventDispatcher);