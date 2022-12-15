var sof = {
    initQuote: function () {
        var $quote = $("div.quote").data("expanded", true),
        $nav = $("div#navigator", $quote),
        $con = $("div#content", $quote),
        $syn = $("div#syndication", $quote),
        height = Math.max($nav.height(), $con.height());

        $nav.height(height);
        $syn.height(height).css('margin-top', -height + 'px');
        $con.height(height);

        $quote.click(function () {
            $quote.data("expanded") ?
                $quote.animate({ "margin-top": -height }, 200) : $quote.animate({ "margin-top": 0 }, 200);
            $quote.data("expanded", !$quote.data("expanded"));
        });

        $.each([$nav, $syn], function (idx, $elem) {
            $elem.click(function (evt) {
                evt.stopPropagation();
            });
        });

        return this;
    },
    enableSyntaxHighlighter: function (hilighterScriptsPath) {
        function path() {
            var args = arguments,
                result = [];

            for (var i = 0; i != (args.length - 1) ; i++)
                result.push(
                    args[i].replace('^', hilighterScriptsPath));

            return result;
        }

        SyntaxHighlighter.autoloader.apply(null, path(
            'applescript ^shBrushAppleScript.js',
            'actionscript3 as3 ^shBrushAS3.js',
            'bash shell ^shBrushBash.js',
            'coldfusion cf ^shBrushColdFusion.js',
            'cpp c ^shBrushCpp.js',
            'c# c-sharp csharp ^shBrushCSharp.js',
            'css ^shBrushCss.js',
            'delphi pascal ^shBrushDelphi.js',
            'diff patch pas ^shBrushDiff.js',
            'erl erlang ^shBrushErlang.js',
            'groovy ^shBrushGroovy.js',
            'java ^shBrushJava.js',
            'jfx javafx ^shBrushJavaFX.js',
            'js jscript javascript ^shBrushJScript.js',
            'perl pl ^shBrushPerl.js',
            'php ^shBrushPhp.js',
            'text plain ^shBrushPlain.js',
            'py python ^shBrushPython.js',
            'ruby rails ror rb ^shBrushRuby.js',
            'sass scss ^shBrushSass.js',
            'scala ^shBrushScala.js',
            'sql ^shBrushSql.js',
            'vb vbnet ^shBrushVb.js',
            'xml xhtml xslt html ^shBrushXml.js',
            'xml ^shBrushXml.js'
        ));
        SyntaxHighlighter.all();
        SyntaxHighlighter.config.bloggerMode = true;

        return sof;
    },
    getGPluseOne: function () {
        $.getScript("http://apis.google.com/js/plusone.js", null, true);
        return sof;
    },
    collapsible: {
        changeTitle: function (button, title, isCollapsed) {
            title = (title ? title + " " : "") + (isCollapsed ? "v" : "^");
            $(button).children("p").html(title);
        },
        showTopButton: function (btn) {
            $(btn).animate({ opacity: 1 }, 100);
        },
        hideTopButton: function (btn) {
            $(btn).animate({ opacity: 0 }, 100);
        },
        enable: function () {
            var self = this;

            $(".collapsible").each(function (index) {
                var collapsible = $(this),
                    isCollapsed = collapsible.hasClass("collapsed"),
                    title = $(this).attr("title"),
                    content = $('<div class="content" >');

                var collapseButton = $('<div class="collapsibleButton" ><p></p></div>').click(function () {
                    isCollapsed = collapsible.hasClass("collapsed");
                    if (!isCollapsed) {
                        $(this).next(".content").fadeOut("slow");
                        collapsible.addClass("collapsed");
                        self.changeTitle(collapseButton, title, true);
                    } else {
                        $(this).next(".content").fadeIn("slow");
                        collapsible.removeClass("collapsed");
                        self.changeTitle(collapseButton, title, false);
                    }
                });
                self.changeTitle(collapseButton, title, isCollapsed);

                $(this).empty().append(collapseButton).append(content);
                isCollapsed && content.css({ "opacity": 0, "display": "none" });
            });
        }
    },

    initScrolling: function () {
        $('#st_down').click(function () {
            $('html, body').animate({ scrollTop: $(document).height() }, { duration: 600, easing: 'linear' });
        });

        $('#st_up').click(function () {
            $('html, body').animate({ scrollTop: 0 }, { duration: 600, easing: 'linear' });
        });

        return this;
    }
}