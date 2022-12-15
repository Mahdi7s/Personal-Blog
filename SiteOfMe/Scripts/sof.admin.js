var sof = sof || {};

sof.makeAutocomplete = function(input, searchUrl) {

    function split(val) {
        return val.split(/,\s*/);
    }

    function extractLast(term) {
        return split(term).pop();
    }

    $(input).bind("keydown", function(e) {
        if (e.keyCode === $.ui.keyCode.TAB && $(this).data("autocomplete").menu.active) {
            e.preventDefault();
        }
    }).autocomplete({
        source: function(req, res) {
            $.getJSON(searchUrl, { term: extractLast(req.term) }, res);
        },
        search: function() {
            var term = extractLast(this.value);
            if (term.length < 2) {
                return false;
            }
        },
        focus: function() {
            return false;
        },
        select: function(e, ui) {
            var terms = split(this.value);
            terms.pop();
            terms.push(ui.item.value);
            terms.push("");
            this.value = terms.join(", ");

            return false;
        }
    });
};

sof.postAutoSave = (function () {
    var lastValues = { shortMessage: "", message: "", title: "", tags: "" },
        intervalId;

    var isAnythingsChanged = function () {
        var shortMsgVal = $(".htm_editor").val(),
            msgVal = $(".htm_editor2").val(),
            titleVal = $("input#Title").val(),
            tagsVal = $("input#tagsAutocomplete").val();

        if (lastValues.shortMessage !== shortMsgVal || lastValues.message !== msgVal
            || lastValues.title !== titleVal || lastValues.tags !== tagsVal) {

            lastValues.shortMessage = shortMsgVal;
            lastValues.message = msgVal;
            lastValues.title = titleVal;
            lastValues.tags = tagsVal;

            return true;
        }
        return false;
    };
    
    return {
        start: function (saveCallback, interval) {
            isAnythingsChanged();
            intervalId = setInterval(function () {
                isAnythingsChanged() && saveCallback();
            }, interval || 360000);
        },
        stop: function () {
            clearInterval(intervalId);
        }
    };
}());