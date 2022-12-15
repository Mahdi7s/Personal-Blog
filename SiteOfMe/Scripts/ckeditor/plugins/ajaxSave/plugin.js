/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

/**
 * @fileSave plugin.
 */

(function () {
    var saveCmd =
	{
	    modes: { wysiwyg: 1, source: 1 },
	    readOnly: 1,

	    exec: function (editor) {
	        var $form = editor.element.$.form;
	        if ($form) {
	            try {
	                editor.updateElement();
	                //Here is where you put your ajax submit function. For example... if you are using
	                // jQuery and the ajaxform plugin, do something like this:
	                onCkEditorAjaxSave();
	            } catch (e) {
	                alert(e);
	            }
	        }
	    }
	};

    var pluginName = 'ajaxsave';

    // Register a plugin named "save".
    CKEDITOR.plugins.add(pluginName,
	{
	    init: function (editor) {
	        var command = editor.addCommand(pluginName, saveCmd);
	        command.modes = { wysiwyg: !!(editor.element.$.form) };

	        editor.ui.addButton('ajaxSave',
				{
				    label: editor.lang.save,
				    command: pluginName,
				    className: 'cke_button_save'
				});
	    }
	});

})();
